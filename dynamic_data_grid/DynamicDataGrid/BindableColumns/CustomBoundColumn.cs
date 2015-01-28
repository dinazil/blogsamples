using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DynamicDataGrid.BindableColumns
{
    /// <summary>
    /// DataGrid columns type for dusplaying dynamically bound columns.
    /// </summary>
    public class CustomBoundColumn : DataGridBoundColumn
    {
        /// <summary>
        /// Gets or sets the cell DataTemplate.
        /// </summary>
        public DataTemplate CellTemplate { get; set; }

        /// <summary>
        /// Gets the read-only element that is bound to the column.
        /// </summary>
        /// <param name="cell">The cell that will contain the generated element.</param>
        /// <param name="dataItem">The data item that is represented by the row that contains the intended cell.</param>
        /// <returns>A new read-only element that is bound to the value of the column.</returns>
        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            var binding = new Binding(((Binding)Binding).Path.Path)
            // comment this out to fix the bug
            { Source = dataItem };

            var content = new ContentPresenter {ContentTemplate = CellTemplate};
            content.SetBinding(ContentControl.ContentProperty, binding);
            return content;
        }

        /// <summary>
        /// Gets the editing element that is bound to the column.
        /// </summary>
        /// <param name="cell">The cell that will contain the generated element.</param>
        /// <param name="dataItem">The data item that is represented by the row that contains the intended cell.</param>
        /// <returns>A new editing element that is bound to the value of the column.</returns>
        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            return GenerateElement(cell, dataItem);
        }
    }
}
