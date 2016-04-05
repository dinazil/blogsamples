using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SelfProfiling
{
	class ThreadSafeRandom
	{
		private static readonly Random m_instance = new Random();

		public static int Next()
		{
			lock (m_instance) return m_instance.Next();
		}
	}
}
