using sbwpf.Core;
using System.Windows;

namespace Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GenerateLogEvents();
        }

        private void GenerateLogEvents()
        {
            Logger.Information("This is an info event");
            Logger.Notify("This is a notify event");
            Logger.Warning("This is your first and last warning. Just kidding.");
            Logger.Error("The end of the demo is nigh!");
        }
    }
}