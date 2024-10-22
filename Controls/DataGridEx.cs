using sbwpf.Core;
using System.ComponentModel;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;


namespace sbwpf.Controls
{
    /// <summary>
    /// A DataGrid which saves the Columns collection when:
    ///     - A column is resized
    ///     - A column is reordered
    ///     - A column is sorted
    /// 
    /// Columns are restored upon Loaded.
    /// Saving & loading are done via IControlSerializer.
    /// </summary>
    public class DataGridEx : DataGrid 
    {
        ///////////////////////////////////////////////////////////
        // A subset of DataGridColumn that works with JsonSerializer
        #region JsonColumn

        internal class JsonColumn
        {
            public object header { get; set; } = string.Empty;
            public double width { get; set; } = 1.0;
            public int displayIndex { get; set; } = 0;
            public ListSortDirection? sortDirection { get; set; }

            public JsonColumn()
            { }

            public JsonColumn(DataGridColumn incol)
            {
                header = incol.Header;
                width = incol.ActualWidth;
                displayIndex = incol.DisplayIndex;
                sortDirection = incol.SortDirection;
            }

            public void ImportToColumn(DataGridColumn incol)
            {
                if(incol.Header != header)
                {
                    Logger.Debug("Error: Column header does not match. Your indexing might be off.");
                    return;
                }
                incol.Width = width;
                incol.DisplayIndex = displayIndex;
                incol.SortDirection = sortDirection;
            }
        }


        #endregion JsonColumn
        ///////////////////////////////////////////////////////////


        ///////////////////////////////////////////////////////////
        #region Fields

        private IControlSerializer? _Serializer;
        private static readonly string ColumnData = "ColumnData";
        private bool ColumnsLoaded = false;
        private uint LoadedCount = 0;

        JsonSerializerOptions JsonOptions = new()
        {
            NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals
        };

        #endregion Fields
        ///////////////////////////////////////////////////////////



        ///////////////////////////////////////////////////////////
        #region Properties

        public IControlSerializer? Serializer
        {
            get => _Serializer;
            set
            {
                if (_Serializer != value)
                {
                    _Serializer = value;
                    LoadColumns();
                }
            }
        }

        #endregion Properties
        ///////////////////////////////////////////////////////////



        ///////////////////////////////////////////////////////////
        #region Interface

        public DataGridEx() : base()
        {
            CommonCtor();
        }

        public DataGridEx(IControlSerializer serializer) : base()
        {
            Serializer = serializer;
            CommonCtor();
        }

        public void Sort(int columnIndex, ListSortDirection direction)
        {
            try
            {
                if (columnIndex < 0 || columnIndex > (Columns.Count - 1))
                {
                    
                    return;
                }

                var column = Columns[columnIndex];
                if (column is null) return;

                Items.SortDescriptions.Clear();
                Items.SortDescriptions.Add(new SortDescription(column.SortMemberPath, direction));

                foreach (var col in Columns)
                {
                    col.SortDirection = null;
                }
                column.SortDirection = direction;
            }
            catch(Exception ex)
            {
                Logger.Debug(ex);
            }

            Items.Refresh();
        }

        #endregion Interface
        ///////////////////////////////////////////////////////////



        ///////////////////////////////////////////////////////////
        #region Internal

        private void CommonCtor()
        {
            Initialized += DataGridEx_Initialized;
            Loaded += DataGridEx_Loaded;
        }

        private void DataGridEx_Initialized(object? sender, EventArgs e)
        {
            Columns.CollectionChanged += Columns_CollectionChanged;
            ColumnReordered += DataGridEx_ColumnReordered;
            Sorting += DataGridEx_Sorting;
            // Note: the hook for column resizing is more complex

            LoadColumns();
        }

        private void Columns_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add && e.NewItems is not null)
            {
                Logger.Debug("Columns_CollectionChanged");
                foreach (DataGridColumn column in e.NewItems)
                {
                    AddColumnSizeChangedEvent(column);
                }
            }
        }

        private void AddColumnSizeChangedEvent(DataGridColumn column)
        {
            DependencyPropertyDescriptor.FromProperty(DataGridColumn.WidthProperty, typeof(DataGridColumn))
                    .AddValueChanged(column, DataGridColumnWidthChanged);
        }

        private void SaveColumns()
        {
            if (!ColumnsLoaded) return;
            try
            {
                List<JsonColumn> columns = [];
                foreach(var col in Columns)
                {
                    columns.Add(new(col));
                }

                string jsonString = JsonSerializer.Serialize<List<JsonColumn>>(columns);
                Serializer?.Serialize(Name, ColumnData, jsonString);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex.Message);
            }
        }

        private void LoadColumns()
        {
            try
            {
                string? jsonString = Serializer?.Deserialize(Name, ColumnData);
                if (jsonString is null) return;
                var collection = JsonSerializer.Deserialize<List<JsonColumn>>(jsonString);
                if (collection is not null)
                {
                    foreach(var item in collection)
                    {
                        var col = Columns.Where(c => ((string)c.Header).Equals(item.header.ToString())).FirstOrDefault();
                        if (col is null) continue;
                        col.Width = item.width;
                        col.DisplayIndex = item.displayIndex;
                        col.SortDirection = item.sortDirection;
                    }
                }
            }
            catch(Exception ex)
            {
                Logger.Debug(ex.Message);
            }
        }

        private void DataGridEx_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ++LoadedCount;
            if (LoadedCount == 2)
            {
                LoadColumns();
                ColumnsLoaded = true;
            }
        }

        private void DataGridColumnWidthChanged(object? sender, EventArgs e)
        {
            SaveColumns();
        }

        private void DataGridEx_ColumnReordered(object? sender, DataGridColumnEventArgs e)
        {
            SaveColumns();
        }

        private void OnColumnSizedChanged(object sender, SizeChangedEventArgs args)
        {
            if (args.NewSize == args.PreviousSize) return;
            SaveColumns();
        }

        private void DataGridEx_Sorting(object sender, DataGridSortingEventArgs e)
        {
            e.Handled = true;
            ListSortDirection? sortDirection = e.Column.SortDirection;
            if (sortDirection is null || sortDirection == ListSortDirection.Ascending)
            {
                sortDirection = ListSortDirection.Descending;
            }
            else
            {
                sortDirection = ListSortDirection.Ascending;
            }

            Sort(e.Column.DisplayIndex, (ListSortDirection)sortDirection);
            SaveColumns();
        }

        #endregion Internal
        ///////////////////////////////////////////////////////////



        
    }
}
