using System.Windows.Controls;

namespace sbwpf.Test
{
    /// <summary>
    /// Interaction logic for uc_DataGridEx.xaml
    /// </summary>
    public partial class uc_DataGridEx : UserControl
    {
        public uc_DataGridEx()
        {
            InitializeComponent();

            datagrid.DataContext = SampleDataset.Samples;
        }
    }
}
