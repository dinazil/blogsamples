using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Ionic.Zip;
using Ionic.Zlib;
using ManagedProcessing;
using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Parsers.Clr;
using Microsoft.Diagnostics.Tracing.Session;
using WrappedProcessing;

namespace SelfProfiling
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private CancellationTokenSource m_processingCts;
		private CancellationToken m_processingToken;

		private CancellationTokenSource m_collectionCts;
		private CancellationToken m_collectionToken;

		private void StartProfilingButton_OnClick(object sender, RoutedEventArgs e)
		{
			StartProfilingButton.IsEnabled = false;

			m_collectionCts = new CancellationTokenSource();
			m_collectionToken = m_collectionCts.Token;
			var bufferSize = Int32.Parse(BufferSizeTextBox.Text);

			Task.Factory.StartNew(
				() =>
				{
					var fileName = string.Format("{0}_cpuStacks_{1}.etl", Dns.GetHostName(), DateTime.Now.ToString("yyyy_MM_d-h_mm_ss"));
					collectCpuStacks(fileName, bufferSize);
				}, m_collectionToken);

			var sw = Stopwatch.StartNew();
			m_profilingTimer = new DispatcherTimer();
			m_profilingTimer.Tick += (s, ee) =>
			{
				ProfilingTimeElapsedLabel.Content = sw.Elapsed;
			};
			m_profilingTimer.Interval = TimeSpan.FromSeconds(1);
			m_profilingTimer.Start();
		}

		private void StopProfilingButton_OnClick(object sender, RoutedEventArgs e)
		{
			m_collectionCts.Cancel();
			m_profilingTimer.Stop();

			StartProfilingButton.IsEnabled = true;
		}

		private ConcurrentBag<int> m_primes = new ConcurrentBag<int>();
		private Stopwatch m_stopwatch = new Stopwatch();
		private DispatcherTimer m_processingTimer;
		private DispatcherTimer m_profilingTimer;
		private int m_managedCalls;
		private int m_nativeCalls;

		private void StartGrindingButton_OnClick(object sender, RoutedEventArgs e)
		{
			StartGrindingButton.IsEnabled = false;

			m_processingCts = new CancellationTokenSource();
			m_processingToken = m_processingCts.Token;

			m_primes = new ConcurrentBag<int>();

			Task.Factory.StartNew(
				() =>
				{
					m_stopwatch = Stopwatch.StartNew();
					m_managedCalls = 0;
					m_nativeCalls = 0;
					Parallel.For(
						0,
						Int32.MaxValue,
						new ParallelOptions { CancellationToken = m_processingToken },
						n =>
						{
							if (ThreadSafeRandom.Next() % 2 == 0)
							{
								Interlocked.Increment(ref m_managedCalls);
								if (ManagedProcessor.IsPrime(n))
								{
									m_primes.Add(n);
								}
							}
							else
							{
								Interlocked.Increment(ref m_nativeCalls);
								if (WrapperProcessor.IsPrime(n))
								{
									m_primes.Add(n);
								}
							}
						});
				}, m_processingToken);
			m_stopwatch.Stop();
		}

		private void StopGrindingButton_OnClick(object sender, RoutedEventArgs e)
		{
			m_processingCts.Cancel();

			NumberOfPrimesLabel.Content = m_primes.Count;
			TimeElapsedLabel.Content = m_stopwatch.Elapsed;
			StartGrindingButton.IsEnabled = true;
			NumberOfManagedCallsLabel.Content = m_managedCalls;
			NumberOfNativeCallsLabel.Content = m_nativeCalls;
		}

		private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
		{
			var cpuCounter = new PerformanceCounter
			{
				CategoryName = "Processor",
				CounterName = "% Processor Time",
				InstanceName = "_Total"
			};

			m_processingTimer = new DispatcherTimer();
			m_processingTimer.Tick += (s, ee) =>
			{
				CpuUsageLabel.Content = cpuCounter.NextValue().ToString("N2");
			};
			m_processingTimer.Interval = TimeSpan.FromSeconds(1);
			m_processingTimer.Start();
		}

		private void MainWindow_OnClosing(object sender, CancelEventArgs e)
		{
			m_processingTimer.Stop();
		}

		private void collectCpuStacks(string dataFileName, int bufferSizeMb)
		{
			const KernelTraceEventParser.Keywords kernelKeywords =
				KernelTraceEventParser.Keywords.ImageLoad |
				KernelTraceEventParser.Keywords.Process |
				KernelTraceEventParser.Keywords.Thread |
				KernelTraceEventParser.Keywords.Profile;

			// To support Windows 7 and earlier, we need a separate session for kernel and CLR events
			string sessionName = "CPUStacksSession";
			string kernelDataFileName = Path.ChangeExtension(dataFileName, ".kernel.etl");
			using (var session = new TraceEventSession(sessionName, dataFileName) { BufferSizeMB = bufferSizeMb })
			{
				using (var kernelSession = new TraceEventSession(KernelTraceEventParser.KernelSessionName, kernelDataFileName))
				{
					kernelSession.EnableKernelProvider(kernelKeywords, KernelTraceEventParser.Keywords.Profile);
					session.EnableProvider(ClrTraceEventParser.ProviderGuid, TraceEventLevel.Verbose,
						(ulong)(ClrTraceEventParser.Keywords.Default | ClrTraceEventParser.Keywords.StartEnumeration));

					m_collectionToken.WaitHandle.WaitOne();

					generateJitRundownEvents(dataFileName, sessionName, bufferSizeMb);

					var kernelEventsLost = kernelSession.EventsLost;
					Dispatcher.BeginInvoke(
						new Action(() =>
						{
							EventsLostKernelLabel.Content = kernelEventsLost;
						}));
				}

				var sessionEventsLost = session.EventsLost;
				Dispatcher.BeginInvoke(
					new Action(() =>
					{
						EventsLostTraceLabel.Content = sessionEventsLost;
					}));
			}

			var p = new Process
			{
				StartInfo = new ProcessStartInfo("xperf")
				{
					Arguments = string.Format("-merge {0}.etl {0}.clrRundown.etl {0}.kernel.etl {0}_merged.etl", Path.GetFileNameWithoutExtension(dataFileName)),
					CreateNoWindow = true,
					WindowStyle = ProcessWindowStyle.Hidden,
					WorkingDirectory = Environment.CurrentDirectory
				}
			};
			try
			{
				p.Start();
			}
			catch (Exception)
			{
			}
			p.WaitForExit();

			using (var zipped = new ZipFile(Path.ChangeExtension(dataFileName, ".zip"))
			{
				CompressionLevel = CompressionLevel.BestCompression
			})
			{
				zipped.AddSelectedFiles(string.Format("name = {0}*.etl", Path.GetFileNameWithoutExtension(dataFileName)));
				zipped.Save();
			}

		}

		private void generateJitRundownEvents(string dataFileName, string sessionName, int bufferSizeMb)
		{
			var rundownFileName = Path.ChangeExtension(dataFileName, ".clrRundown.etl");
			using (var rundownSession = new TraceEventSession(sessionName + "Rundown", rundownFileName))
			{
				rundownSession.EnableProvider(ClrRundownTraceEventParser.ProviderGuid, TraceEventLevel.Verbose, (ulong)(ClrRundownTraceEventParser.Keywords.Default));
				// Poll until 2 second goes by without growth.  
				for (var prevLength = new FileInfo(rundownFileName).Length; ;)
				{
					Thread.Sleep(TimeSpan.FromSeconds(2));
					var newLength = new FileInfo(rundownFileName).Length;
					if (newLength == prevLength) break;
					prevLength = newLength;
				}

				var eventsLostClr = rundownSession.EventsLost;
				Dispatcher.BeginInvoke(
					new Action(() =>
					{
						EventsLostClrLabel.Content = eventsLostClr;
					}));
			}
		}

	}
}
