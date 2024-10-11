using sbwpf.Core;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;


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
        #region Fields

        private IControlSerializer? _Serializer;
        private static readonly string ColumnData = "ColumnData";

        #endregion Fields
        ///////////////////////////////////////////////////////////



        ///////////////////////////////////////////////////////////
        #region Properties

        public IControlSerializer? Serializer
        {
            get => _Serializer;
            set => _Serializer = value;
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

        private void CommonCtor()
        {
            ColumnReordered += DataGridEx_ColumnReordered;
            Sorting += DataGridEx_Sorting;
            Loaded += DataGridEx_Loaded;
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
            catch
            {
            }

            Items.Refresh();
        }

        #endregion Interface
        ///////////////////////////////////////////////////////////
        


        ///////////////////////////////////////////////////////////
        #region Internal

        private void SaveColumns()
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(Columns);
                Serializer?.Serialize(Name, ColumnData, jsonString);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        private void LoadColumns()
        {
            try
            {
                string? jsonString = Serializer?.Deserialize(Name, ColumnData);
                var collection = JsonSerializer.Deserialize<ObservableCollection<DataGridColumn>>(jsonString ?? string.Empty);
                if (collection is not null)
                {
                    Columns.Clear();
                    Columns.AddRange(collection);
                }
            }
            catch(Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        private void DataGridEx_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            LoadColumns();
            foreach (var column in Columns)
            {
                var header = GetDataGridColumnHeader(column);
                if (header != null)
                {
                    DependencyPropertyDescriptor.FromProperty(DataGridColumnHeader.WidthProperty, typeof(DataGridColumnHeader))
                        .AddValueChanged(header, DataGridColumnWidthChanged);
                }
            }
        }

        private DataGridColumnHeader? GetDataGridColumnHeader(DataGridColumn column)
        {
            if (ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
            {
                return null;
            }

            return ItemContainerGenerator.ContainerFromIndex(Columns.IndexOf(column)) as DataGridColumnHeader;
        }

        private void DataGridColumnWidthChanged(object? sender, EventArgs e)
        {
            SaveColumns();
        }

        private void DataGridEx_ColumnReordered(object? sender, DataGridColumnEventArgs e)
        {
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
