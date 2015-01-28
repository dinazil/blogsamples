using GalaSoft.MvvmLight;

namespace DynamicDataGrid.BindableColumns
{
    /// <summary>
    /// Represents the header of a dynamic column in a data grid
    /// </summary>
    public class ColumnHeader : ObservableObject
    {
        private object _header;

        /// <summary>
        /// Gets or sets the header value.
        /// </summary>
        public object Header
        {
            get { return _header; }
            set { Set(() => Header, ref _header, value); }
        }
    }
}
