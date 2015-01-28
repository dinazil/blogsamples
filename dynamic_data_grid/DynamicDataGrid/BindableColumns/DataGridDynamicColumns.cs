using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DynamicDataGrid.BindableColumns
{
    /// <summary>
    /// Helper class allowing to bind columns dynamically to a data grid.
    /// </summary>
    public static class DataGridDynamicColumns
    {
        private static readonly Dictionary<DataGrid,
            Dictionary<INotifyCollectionChanged, NotifyCollectionChangedEventHandler>> ObservableCollectionEventHandlers
            = new Dictionary<DataGrid, Dictionary<INotifyCollectionChanged, NotifyCollectionChangedEventHandler>>();

        /// <summary>
        /// The DisplayColumnsSource attached property.
        /// </summary>
        public static readonly DependencyProperty ColumnsSourceProperty = DependencyProperty.RegisterAttached(
          "DisplayColumnsSource",
          typeof(IList<ColumnHeader>),
          typeof(DataGridDynamicColumns), new PropertyMetadata(null, DisplayColumnsSourcePropertyChangedCallback));

        /// <summary>
        /// Sets the dynamic columns source for a data grid
        /// </summary>
        /// <param name="element">The elements to which to attach the column. This must be a DataGrid.</param>
        /// <param name="value">The list of columns to attach.</param>
        public static void SetDisplayColumnsSource(DependencyObject element, IList<ColumnHeader> value)
        {
            element.SetValue(ColumnsSourceProperty, value);
        }

        /// <summary>
        /// Gets the dynmic columns from a data grid
        /// </summary>
        /// <param name="element">The element which the columns are attached to.</param>
        /// <returns>The list of dynamic columns.</returns>
        public static IList<ColumnHeader> GetDisplayColumnsSource(DependencyObject element)
        {
            return (IList<ColumnHeader>)element.GetValue(ColumnsSourceProperty);
        }

        /// <summary>
        /// The header template attached property
        /// </summary>
        public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.RegisterAttached(
            "HeaderTemplate", typeof (DataTemplate), typeof (DataGridDynamicColumns), new PropertyMetadata(null, HeaderTemplatePropertyChangedCallback));

        private static void HeaderTemplatePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var target = dependencyObject as DataGrid;
            var headerTemplate = dependencyPropertyChangedEventArgs.NewValue as DataTemplate;

            CreateColumns(target, GetDisplayColumnsSource(dependencyObject), headerTemplate, GetCellTemplate(dependencyObject));
        }

        /// <summary>
        /// Sets the header template
        /// </summary>
        /// <param name="element">The element to which to attach the property.</param>
        /// <param name="value">The DataTemplate to be used to displaying the column headers.</param>
        public static void SetHeaderTemplate(DependencyObject element, DataTemplate value)
        {
            element.SetValue(HeaderTemplateProperty, value);
        }

        /// <summary>
        /// Gets the column header template.
        /// </summary>
        /// <param name="element">The element to exrtact the template from.</param>
        /// <returns>The DataTemplate used to display column headers.</returns>
        public static DataTemplate GetHeaderTemplate(DependencyObject element)
        {
            return (DataTemplate)element.GetValue(HeaderTemplateProperty);
        }

        /// <summary>
        /// The attached property which represents the template to be used to display the cell content.
        /// </summary>
        public static readonly DependencyProperty CellTemplateProperty = DependencyProperty.RegisterAttached(
            "CellTemplate", typeof(DataTemplate), typeof(DataGridDynamicColumns), new PropertyMetadata(null, CellTemplatePropertyChangedCallback));

        private static void CellTemplatePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var target = dependencyObject as DataGrid;
            var cellTemplate = dependencyPropertyChangedEventArgs.NewValue as DataTemplate;

            CreateColumns(target, GetDisplayColumnsSource(dependencyObject), GetHeaderTemplate(dependencyObject), cellTemplate);
        }

        /// <summary>
        /// Sets the cell template.
        /// </summary>
        /// <param name="element">The element to which the property is attached.</param>
        /// <param name="value">The DataTemplate used to display the cell data.</param>
        public static void SetCellTemplate(DependencyObject element, DataTemplate value)
        {
            element.SetValue(CellTemplateProperty, value);
        }

        /// <summary>
        /// Gets the cell DataTemplate.
        /// </summary>
        /// <param name="element">The element to which the property is attached.</param>
        /// <returns>The DataTemplate used to display the cell content.</returns>
        public static DataTemplate GetCellTemplate(DependencyObject element)
        {
            return (DataTemplate)element.GetValue(CellTemplateProperty);
        }
        
        private static void DisplayColumnsSourcePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var target = dependencyObject as DataGrid;
            if (target == null)
            {
                return;
            }

            if (!ObservableCollectionEventHandlers.ContainsKey(target))
            {
                ObservableCollectionEventHandlers.Add(target, new Dictionary<INotifyCollectionChanged, NotifyCollectionChangedEventHandler>());
            }

            var collectionsForThisGrid = ObservableCollectionEventHandlers[target];

            var columns = dependencyPropertyChangedEventArgs.NewValue as IList<ColumnHeader>;

            var oldColumns = dependencyPropertyChangedEventArgs.OldValue as INotifyCollectionChanged;
            if (oldColumns != null && collectionsForThisGrid.ContainsKey(oldColumns))
            {
                oldColumns.CollectionChanged -= collectionsForThisGrid[oldColumns];
                collectionsForThisGrid.Remove(oldColumns);
            }

            var observable = columns as INotifyCollectionChanged;
            if (observable != null)
            {
                NotifyCollectionChangedEventHandler handler = (sender, args) =>
                {
                    bool handlingComplete = true;
                    switch (args.Action)
                    {
                        case NotifyCollectionChangedAction.Add:
                            handlingComplete = HandledAdded(target, GetDisplayColumnsSource(dependencyObject), GetHeaderTemplate(dependencyObject), GetCellTemplate(dependencyObject), args);
                            break;
                        case NotifyCollectionChangedAction.Replace:
                            handlingComplete = HandledReplaced(target, GetDisplayColumnsSource(dependencyObject), GetHeaderTemplate(dependencyObject), GetCellTemplate(dependencyObject), args);
                            break;
                        case NotifyCollectionChangedAction.Remove:
                            handlingComplete = HandleRemoved(target, GetDisplayColumnsSource(dependencyObject), GetHeaderTemplate(dependencyObject), GetCellTemplate(dependencyObject), args);
                            break;
                        default:
                            CreateColumns(target, GetDisplayColumnsSource(dependencyObject), GetHeaderTemplate(dependencyObject), GetCellTemplate(dependencyObject));
                            break;
                    }

                    if (!handlingComplete)
                    {
                        CreateColumns(target, GetDisplayColumnsSource(dependencyObject), GetHeaderTemplate(dependencyObject), GetCellTemplate(dependencyObject));
                    }
                };
                observable.CollectionChanged += handler;
                collectionsForThisGrid.Add(observable, handler);
            }

            CreateColumns(target, columns, GetHeaderTemplate(dependencyObject), GetCellTemplate(dependencyObject));
        }

        private static bool HandleRemoved(DataGrid dataGrid, IList<ColumnHeader> columnHeaders, DataTemplate headerTemplate, DataTemplate cellTemplate, NotifyCollectionChangedEventArgs args)
        {
            if (args.OldItems.Count == 1)
            {
                RemoveColumn(dataGrid, args.OldStartingIndex);
                return true;
            }
            return false;
        }

        private static bool HandledReplaced(DataGrid dataGrid, IList<ColumnHeader> columnHeaders, DataTemplate headerTemplate, DataTemplate cellTemplate, NotifyCollectionChangedEventArgs args)
        {
            if (args.NewItems.Count == 1 && args.OldItems.Count == 1)
            {
                ReplaceColumns(dataGrid, headerTemplate, cellTemplate, columnHeaders[args.NewStartingIndex], args.NewStartingIndex);
                Rebind(dataGrid, args.NewStartingIndex, args.NewStartingIndex);
                return true;
            }
            return false;
        }
        
        private static bool HandledAdded(DataGrid dataGrid, IList<ColumnHeader> columnHeaders, DataTemplate headerTemplate, DataTemplate cellTemplate, NotifyCollectionChangedEventArgs args)
        {
            if (args.NewItems.Count == 1)
            {
                InsertColumn(dataGrid, headerTemplate, cellTemplate, columnHeaders[args.NewStartingIndex], args.NewStartingIndex);
                return true;
            }
            return false;
        }

        private static void Rebind(DataGrid dataGrid, int startIndex)
        {
            Rebind(dataGrid, startIndex, dataGrid.Columns.Count);
        }

        private static void Rebind(DataGrid dataGrid, int startIndex, int endIndex)
        {
            for (int i = startIndex; i < endIndex; ++i)
            {
                var boundColumn = dataGrid.Columns[i] as DataGridBoundColumn;
                if (boundColumn != null)
                {
                    boundColumn.Binding = CreateBindingToIndex(i);
                }
            }
        }

        private static void CreateColumns(DataGrid dataGrid, IList<ColumnHeader> columns, DataTemplate headerTemplate, DataTemplate cellTemplate)
        {
            if (dataGrid == null)
            {
                return;
            }

            dataGrid.Columns.Clear();

            if (columns == null)
            {
                return;
            }

            for (int i  = 0; i < columns.Count; ++i)
            {
                InsertColumn(dataGrid, headerTemplate, cellTemplate, columns[i], i);
            }
        }

        private static void InsertColumn(DataGrid dataGrid, 
            DataTemplate headerTemplate, DataTemplate cellTemplate, 
            ColumnHeader c, int index)
        {
            dataGrid.Columns.Insert(index, CreateColumnWithIndex(headerTemplate, cellTemplate, c, index));
            Rebind(dataGrid, index);
        }

        private static void RemoveColumn(DataGrid dataGrid, int index)
        {
            dataGrid.Columns.RemoveAt(index);
            Rebind(dataGrid, index);
        }
        
        private static void ReplaceColumns(DataGrid dataGrid, DataTemplate headerTemplate, DataTemplate cellTemplate, ColumnHeader columnHeader, int index)
        {
            dataGrid.Columns[index] = CreateColumnWithIndex(headerTemplate, cellTemplate, columnHeader, index);
        }

        private static DataGridColumn CreateColumnWithIndex(DataTemplate headerTemplate, DataTemplate cellTemplate, ColumnHeader c, int index)
        {
            var binding = CreateBindingToIndex(index);
            return new CustomBoundColumn
            {
                Header = c,
                Binding = binding,
                HeaderTemplate = headerTemplate,
                CellTemplate = cellTemplate
            };
        }

        private static Binding CreateBindingToIndex(int index)
        {
            return new Binding(string.Format("[{0}]", index));
        }
    }
}
