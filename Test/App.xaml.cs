using sbwpf.Themer;
using System.Windows;

namespace Demo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Initialize and integrate sbwpf.Themer
            ThemeManager.Integrate(this);
            ThemeManager.ActiveTheme = ThemeManager.Themes.First();
        }
    }

}
