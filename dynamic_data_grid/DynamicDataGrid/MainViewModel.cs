using DynamicDataGrid.BindableColumns;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DynamicDataGrid
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ColumnHeader> _columns = 
            new ObservableCollection<ColumnHeader>();

        private readonly ObservableCollection<DynamicRow<int, PropertyData>> _rows =
            new ObservableCollection<DynamicRow<int, PropertyData>>();

        public ObservableCollection<ColumnHeader> Columns
        {
            get
            {
                return _columns;
            }
        }

        public ObservableCollection<DynamicRow<int, PropertyData>> Rows
        {
            get
            {
                return _rows;
            }
        }

        private int _currentColumn;
        public int CurrentColumn
        {
            get { return _currentColumn; }
            set 
            { 
                Set(() => CurrentColumn, ref _currentColumn, value);
                RemoveColumnCommand.RaiseCanExecuteChanged();
            }
        }

        private int _rowCounter = 0;
        private int _dataCounter = 0;
        private int _columnCounter = 0;

        private const int InitialColumnsNumber = 3;
        private const int InitialRowsNumber = 10;

        public MainViewModel()
        {
           for (int i = 0; i < InitialColumnsNumber; ++i)
           {
               Columns.Add(new ColumnHeader { Header = _columnCounter++ });
           }

           for (int j = 0; j < InitialRowsNumber; ++j)
           {
               var row = new DynamicRow<int, PropertyData> { Title = _rowCounter++ };
               for (int i = 0; i < InitialColumnsNumber; ++i)
               {
                   row.Add(new PropertyData { Data = _dataCounter++ });
               }
               Rows.Add(row);
           }
        }

        private void ResetData()
        {
            //_dataCounter = 0;
            foreach (var r in Rows)
            {
                foreach (var p in r)
                {
                    p.Data = _dataCounter++;
                }
            }
        }

        private ICommand _resetDataCommand;

        public ICommand ResetDataCommand
        {
            get
            {
                return _resetDataCommand ?? (_resetDataCommand = new RelayCommand(ResetData));
            }
        }

        private void AddColumn(int index)
        {
            Columns.Insert(index, new ColumnHeader { Header = _columnCounter++ });
            foreach (var r in Rows)
            {
                r.Insert(index, new PropertyData { Data = _dataCounter++ });
            }
            ++CurrentColumn;
        }

        private ICommand _addColumnCommand;

        public ICommand AddColumnCommand
        {
            get
            {
                return _addColumnCommand ?? (_addColumnCommand = new RelayCommand<int>(AddColumn));
            }
        }

        private void RemoveColumn(int index)
        {
            Columns.RemoveAt(index);
            foreach (var r in Rows)
            {
                r.RemoveAt(index);
            }
            CurrentColumn = Math.Max(0, CurrentColumn-1);
        }

        private bool CanRemoveColumn(int index)
        {
            return Columns.Any() && Columns.Count > index;
        }

        private RelayCommand<int> _removeColumnCommand;

        public RelayCommand<int> RemoveColumnCommand
        {
            get
            {
                return _removeColumnCommand ?? (_removeColumnCommand = new RelayCommand<int>(RemoveColumn, CanRemoveColumn));
            }
        }
    }
}
