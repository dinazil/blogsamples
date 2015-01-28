using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicDataGrid
{
    public class PropertyData : ObservableObject
    {
        private int _data;
        public int Data
        {
            get { return _data; }
            set { Set(() => Data, ref _data, value); }
        }
    }
}
