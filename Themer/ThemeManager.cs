
using sbwpf.Core;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;
using System.Reflection;

namespace sbwpf.Themer
{
    public class ThemeManager : INotifyPropertyChanged
    {
        ///////////////////////////////////////////////////////////
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;
        public static event PropertyChangedEventHandler? ThemeChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetField<TField>(ref TField field, TField value, string propertyName)
        {
            if (EqualityComparer<TField>.Default.Equals(field, value))
            {
                return;
            }

            field = value;
            OnPropertyChanged(propertyName);
            if(propertyName.Equals("ActiveTheme", StringComparison.CurrentCultureIgnoreCase))
            {
                ThemeChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion INotifyPropertyChanged
        ///////////////////////////////////////////////////////////



        ///////////////////////////////////////////////////////////
        #region Fields

        private static ObservableCollection<Theme> _Themes = [];
        private static ObservableCollection<ResourceDictionary> _Templates = [];
        private static Theme _ActiveTheme = new();
        private static Application? HostApp;

        #endregion Fields
        ///////////////////////////////////////////////////////////



        ///////////////////////////////////////////////////////////
        #region Properties

        public static ObservableCollection<Theme> Themes
        {
            get => _Themes;
        }

        public static ObservableCollection<ResourceDictionary> Templates
        {
            get => _Templates;
        }

        public static Theme ActiveTheme
        {
            get => _ActiveTheme;
            set
            {
                if (HostApp is null) return;
                if (value is not Theme theme) return;

                // Special case for system theme: even though the incoming value is technically not new,
                // allowing the flow to proceed past this point will trigger resampling of the system colors.
                if (_ActiveTheme == value && !IsSystemTheme(value)) return;                

                try
                {
                    // Refresh system colors on selection of System theme
                    // (to account for changes the user might have made)
                    if (IsSystemTheme(value))
                    {
                        SampleSystemColors();
                    }

                    HostApp.Resources.MergedDictionaries.Remove(_ActiveTheme.Resource);
                    _ActiveTheme = theme;
                    HostApp.Resources.MergedDictionaries.Add(_ActiveTheme.Resource);

                    ReloadTemplates();

                    ThemeChanged?.Invoke(value, new PropertyChangedEventArgs(nameof(ActiveTheme)));
                }
                catch(Exception ex)
                {
                    Logger.Warning(ex);
                }
            }
        }

        #endregion Properties
        ///////////////////////////////////////////////////////////



        ///////////////////////////////////////////////////////////
        #region Interface

        public static void Integrate(Application? application)
        {
            HostApp = application;
            if (HostApp is null) return;

            Initialize();

            application?.Resources.MergedDictionaries.Add(ActiveTheme.Resource);

            FrameworkElement.StyleProperty.OverrideMetadata(typeof(Window), new FrameworkPropertyMetadata
            {
                DefaultValue = application?.FindResource(typeof(Window))
            });
            FrameworkElement.StyleProperty.OverrideMetadata(typeof(UserControl), new FrameworkPropertyMetadata
            {
                DefaultValue = application?.FindResource(typeof(UserControl))
            });
        }

        public static void SetTheme(string themeName)
        {
            Theme? theme = Themes.Where(t => t.Name.Equals(themeName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            if(theme is not null)
            {
                ActiveTheme = theme;
                ReloadTemplates();
            }
        }

        public static void SetTheme(Theme theme)
        {
            ActiveTheme = theme;
            ReloadTemplates();
        }

        public static void AddTheme(Theme theme)
        {
            Themes.AddUnique(theme);
        }

        public static void LoadTheme(string jsonString)
        {
            Theme? theme = Theme.FromJson(jsonString);
            if (theme is not null)
            {
                Themes.Add(theme);
            }
        }

        public static void LoadTheme(Stream stream)
        {
            try
            {
                string value = new StreamReader(stream).ReadToEnd();
                LoadTheme(value);
            }
            catch(Exception ex)
            {
                Logger.Warning(ex);
            }
        }
        
        #endregion Interface
        ///////////////////////////////////////////////////////////


        ///////////////////////////////////////////////////////////
        #region Internal

        private static bool IsSystemTheme(Theme theme)
        {
            return theme.Name.Equals("system", StringComparison.CurrentCultureIgnoreCase);
        }

        private static void LoadTemplate(string filename)
        {
            string uri = $"/sbwpf.Themer;component/Templates/{filename}";
            Templates.Add(new ResourceDictionary() { Source = new Uri(uri, UriKind.RelativeOrAbsolute) });
        }

        private static void LoadDefaultThemes()
        {
            List<string> resourceNames = IoUtil.GetResourceNames("sbwpf.Themer.Themes.", ".json");
            foreach (string resourceName in resourceNames)
            {
                Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
                if (stream is not null)
                {
                    LoadTheme(stream);
                }
                else
                {
                    Logger.Warning($"Failed to extract resource stream: {resourceName}");
                }
            }
        }

        private static void Initialize()
        {
            LoadDefaultThemes();

            LoadTemplate("Border.xaml");
            LoadTemplate("Button.xaml");
            LoadTemplate("CheckBox.xaml");
            LoadTemplate("ComboBox.xaml");
            LoadTemplate("ContextMenu.xaml");
            LoadTemplate("DataGrid.xaml");
            LoadTemplate("GridSplitter.xaml");
            LoadTemplate("Label.xaml");
            LoadTemplate("ListBox.xaml");
            LoadTemplate("ListBoxItem.xaml");
            LoadTemplate("ListView.xaml");
            LoadTemplate("Menu.xaml");
            LoadTemplate("RadioButton.xaml");
            LoadTemplate("ScrollBar.xaml");
            LoadTemplate("Separator.xaml");
            LoadTemplate("StatusBar.xaml");
            LoadTemplate("TabControl.xaml");
            LoadTemplate("TextBlock.xaml");
            LoadTemplate("TextBox.xaml");
            LoadTemplate("Thumb.xaml");
            LoadTemplate("ToggleButton.xaml");
            LoadTemplate("ToolTip.xaml");
            LoadTemplate("TreeView.xaml");
            LoadTemplate("Window.xaml");

            // Breaking the alphabetical pattern here, because Toolbar is a bit of a special case.
            LoadTemplate("Toolbar.xaml");

            LoadTemplates();
        }

        private static void LoadTemplates()
        {
            foreach (var template in Templates)
            {
                HostApp?.Resources.MergedDictionaries.Add(template);
            }
        }

        private static void ReloadTemplates()
        {
            foreach (var template in Templates)
            {
                HostApp?.Resources.MergedDictionaries.Remove(template);
                //if (!IsSystemTheme(ActiveTheme))
                {
                    HostApp?.Resources.MergedDictionaries.Add(template);
                }
            }
        }

        private static void SampleSystemColors()
        {
            //Theme? theme = Themes.Where(t => t.DisplayName.Equals("system", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            //if(theme is null) return;

            Logger.Notify("Sampling system colors");

            ActiveTheme.Resource["BackgroundNormalColor"] = SystemColors.WindowColor;
            ActiveTheme.Resource["ForegroundNormalColor"] = SystemColors.WindowTextColor;
            ActiveTheme.Resource["BorderNormalColor"] = SystemColors.ActiveBorderColor;
            ActiveTheme.Resource["BackgroundNormalColor"] = SystemColors.ControlColor;
            ActiveTheme.Resource["BackgroundSelected"] = SystemColors.HighlightColor;
            ActiveTheme.Resource["BackgroundInactive"] = SystemColors.HighlightColor;
            ActiveTheme.Resource["BackgroundDisabled"] = SystemColors.ControlColor;
            ActiveTheme.Resource["BackgroundMouseOver"] = SystemColors.ControlLightColor;
            ActiveTheme.Resource["BackgroundPressed"] = SystemColors.ControlDarkColor;
            ActiveTheme.Resource["BackgroundLight"] = SystemColors.ControlDarkColor;
            ActiveTheme.Resource["BackgroundMedium"] = SystemColors.ControlDarkColor;
            ActiveTheme.Resource["BackgroundDark"] = SystemColors.ControlDarkColor;

            ActiveTheme.Resource["BackgroundNormalBrush"] = SystemColors.ControlBrush;
            ActiveTheme.Resource["BackgroundSelectedBrush"] = SystemColors.MenuHighlightBrush;
            ActiveTheme.Resource["BackgroundInactiveBrush"] = SystemColors.InactiveSelectionHighlightBrush;
            ActiveTheme.Resource["BackgroundDisabledBrush"] = SystemColors.ControlBrush;
            ActiveTheme.Resource["BackgroundMouseOverBrush"] = SystemColors.ControlLightColor;
            ActiveTheme.Resource["BackgroundPressedBrush"] = SystemColors.ControlDarkColor;
            ActiveTheme.Resource["BackgroundLightBrush"] = SystemColors.ControlDarkColor;
            ActiveTheme.Resource["BackgroundMediumBrush"] = SystemColors.ControlDarkColor;
            ActiveTheme.Resource["BackgroundDarkBrush"] = SystemColors.ControlDarkColor;

            ActiveTheme.Resource["ForegroundNormalColor"] = SystemColors.ControlTextColor;
            ActiveTheme.Resource["ForegroundSelectedColor"] = SystemColors.HighlightTextColor;
            ActiveTheme.Resource["ForegroundInactiveColor"] = SystemColors.HighlightTextColor;
            ActiveTheme.Resource["ForegroundDisabledColor"] = SystemColors.GrayTextColor;
            ActiveTheme.Resource["ForegroundMouseOver"] = SystemColors.ControlTextColor;
            ActiveTheme.Resource["ForegroundPressed"] = SystemColors.ControlTextColor;
            ActiveTheme.Resource["ForegroundLight"] = SystemColors.ControlTextColor;
            ActiveTheme.Resource["ForegroundDark"] = SystemColors.ControlTextColor;

            ActiveTheme.Resource["ForegroundNormalBrush"] = SystemColors.ControlTextBrush;
            ActiveTheme.Resource["ForegroundSelectedBrush"] = SystemColors.ControlTextBrush;
            ActiveTheme.Resource["ForegroundInactiveBrush"] = SystemColors.ControlTextBrush;
            ActiveTheme.Resource["ForegroundDisabledBrush"] = SystemColors.GrayTextBrush;
            ActiveTheme.Resource["ForegroundMouseOverBrush"] = SystemColors.ControlTextBrush;
            ActiveTheme.Resource["ForegroundPressedBrush"] = SystemColors.ControlTextBrush;
            ActiveTheme.Resource["ForegroundLightBrush"] = SystemColors.ControlTextBrush;
            ActiveTheme.Resource["ForegroundDarkBrush"] = SystemColors.ControlTextBrush;

            ActiveTheme.Resource["BorderNormalColor"] = SystemColors.ActiveBorderColor;
            ActiveTheme.Resource["BorderSelected"] = SystemColors.ActiveBorderColor;
            ActiveTheme.Resource["BorderInactive"] = SystemColors.ActiveBorderColor;
            ActiveTheme.Resource["BorderDisabledColor"] = SystemColors.InactiveBorderColor;
            ActiveTheme.Resource["BorderMouseOver"] = SystemColors.ActiveBorderColor;
            ActiveTheme.Resource["BorderPressedColor"] = SystemColors.ActiveBorderColor;
            ActiveTheme.Resource["BorderLightColor"] = SystemColors.ControlLightColor;
            ActiveTheme.Resource["BorderMediumColor"] = SystemColors.ControlDarkColor;
            ActiveTheme.Resource["BorderDarkColor"] = SystemColors.ControlDarkDarkColor;

            ActiveTheme.Resource["BorderNormalBrush"] = SystemColors.ActiveBorderBrush;
            ActiveTheme.Resource["BorderSelectedBrush"] = SystemColors.ActiveBorderBrush;
            ActiveTheme.Resource["BorderInactiveBrush"] = SystemColors.InactiveBorderBrush;
            ActiveTheme.Resource["BorderDisabledBrush"] = SystemColors.InactiveBorderBrush;
            ActiveTheme.Resource["BorderMouseOverBrush"] = SystemColors.ControlTextBrush;
            ActiveTheme.Resource["BorderPressedBrush"] = SystemColors.ActiveBorderBrush;
            ActiveTheme.Resource["BorderLightBrush"] = SystemColors.ActiveBorderBrush;
            ActiveTheme.Resource["BorderMediumBrush"] = SystemColors.ActiveBorderBrush;
            ActiveTheme.Resource["BorderDarkBrush"] = SystemColors.ActiveBorderBrush;

            ActiveTheme.Resource["ControlNormalColor"] = SystemColors.ControlColor;
            ActiveTheme.Resource["ControlSelected"] = SystemColors.HighlightColor;
            ActiveTheme.Resource["ControlInactive"] = SystemColors.HighlightColor;
            ActiveTheme.Resource["ControlDisabledColor"] = SystemColors.ControlColor;
            ActiveTheme.Resource["ControlMouseOverColor"] = SystemColors.ControlLightLightColor;
            ActiveTheme.Resource["ControlPressedColor"] = SystemColors.ControlDarkColor;
            ActiveTheme.Resource["ControlLightColor"] = SystemColors.ControlDarkColor;
            ActiveTheme.Resource["ControlDarkColor"] = SystemColors.ControlDarkColor;

            ActiveTheme.Resource["ControlNormalBrush"] = SystemColors.ControlBrush;
            ActiveTheme.Resource["ControlSelectedBrush"] = SystemColors.ControlDarkBrush;
            ActiveTheme.Resource["ControlInactiveBrush"] = SystemColors.InactiveSelectionHighlightBrush;
            ActiveTheme.Resource["ControlDisabledBrush"] = SystemColors.InactiveSelectionHighlightBrush;
            ActiveTheme.Resource["ControlMouseOverBrush"] = SystemColors.ControlLightBrush;
            ActiveTheme.Resource["ControlPressedBrush"] = SystemColors.ControlDarkBrush;
            ActiveTheme.Resource["ControlLightBrush"] = SystemColors.ControlLightBrush;
            ActiveTheme.Resource["ControlMediumBrush"] = SystemColors.ControlLightBrush;
            ActiveTheme.Resource["ControlDarkBrush"] = SystemColors.ControlDarkBrush;
        }

        #endregion Internal
        ///////////////////////////////////////////////////////////

    }
}
