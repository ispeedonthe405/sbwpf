using sbwpf.Core;
using System.Windows.Controls;

namespace sbwpf.Test
{
    /// <summary>
    /// Interaction logic for uc_LoggerView.xaml
    /// </summary>
    public partial class uc_LoggerView : UserControl
    {
        public uc_LoggerView()
        {
            InitializeComponent();
            Display.ItemsSource = Logger.Events;
        }
    }
}
