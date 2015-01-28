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

        private int _rowCounter = 0;
        private int _dataCounter = 0;

        private const int InitialColumnsNumber = 3;
        private const int InitialRowsNumber = 10;

        public MainViewModel()
        {
           for (int i = 0; i < InitialColumnsNumber; ++i)
           {
               Columns.Add(new ColumnHeader { Header = i });
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
    }
}
