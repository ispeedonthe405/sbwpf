using sbwpf.Themer;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Test
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Init and integrate sbwpf.Themer
            ThemeManager.SetApplication(this);
            ThemeManager.ActiveTheme = ThemeManager.Themes.First();

            FrameworkElement.StyleProperty.OverrideMetadata(typeof(Window), new FrameworkPropertyMetadata
            {
                DefaultValue = FindResource(typeof(Window))
            });
            FrameworkElement.StyleProperty.OverrideMetadata(typeof(UserControl), new FrameworkPropertyMetadata
            {
                DefaultValue = FindResource(typeof(UserControl))
            });

            
        }
    }

}
