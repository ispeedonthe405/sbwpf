using System.Windows.Controls;
using sbwpf.Themer;


namespace sbwpf.Test
{
    /// <summary>
    /// Interaction logic for uc_Themer.xaml
    /// </summary>
    public partial class uc_Themer : UserControl
    {
        public uc_Themer()
        {
            InitializeComponent();

            cb_Themes.DisplayMemberPath = "Name";
            cb_Themes.ItemsSource = ThemeManager.Themes;
        }

        private void cb_Themes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cb_Themes.SelectedItem is Theme theme)
            {
                ThemeManager.ActiveTheme = theme;
            }
        }
    }
}
