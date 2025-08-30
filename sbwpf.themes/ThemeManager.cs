using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Windows.UI.ViewManagement;

namespace sbwpf.themes
{
    public class ThemeManager : INotifyPropertyChanged
    {
		///////////////////////////////////////////////////////////
		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler? PropertyChanged;

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
		}

		#endregion INotifyPropertyChanged
		///////////////////////////////////////////////////////////



		/////////////////////////////////////////////////////////
		#region Events

		public class ThemeChangedEventArgs : EventArgs
		{
			public string ThemeName { get; set; } = string.Empty;

			public ThemeChangedEventArgs()
			{ }

			public ThemeChangedEventArgs(string themeName)
            {
                ThemeName = themeName;
            }
        }

        public delegate void ThemeChangedEvent(object sender, ThemeChangedEventArgs e);
		public event ThemeChangedEvent? LightThemeChanged;
		public event ThemeChangedEvent? DarkThemeChanged;


        #endregion Events
        /////////////////////////////////////////////////////////


        /////////////////////////////////////////////////////////
        #region Fields

        private UISettings _uiSettings;

        private Application? _App;
		
		private static string _DefaultThemeName = "Default";
		
		private ObservableCollection<string> _LightThemes = [];
		private ObservableCollection<string> _DarkThemes = [];

        private string _SelectedLightTheme = _DefaultThemeName;
        private string _SelectedDarkTheme = _DefaultThemeName;

		#endregion Fields
		/////////////////////////////////////////////////////////



		/////////////////////////////////////////////////////////
		#region Properties


		public ObservableCollection<string> LightThemes
		{
			get => _LightThemes;
		}

		public ObservableCollection<string> DarkThemes
		{
			get => _DarkThemes;
		}

		public string SelectedLightTheme
		{
			get => _SelectedLightTheme;
			set => SetField(ref _SelectedLightTheme, value, nameof(SelectedLightTheme));
		}

		public string SelectedDarkTheme
		{
			get => _SelectedDarkTheme;
			set => SetField(ref _SelectedDarkTheme, value, nameof(SelectedDarkTheme));
		}

		#endregion Properties
		/////////////////////////////////////////////////////////


		public ThemeManager()
		{
			_uiSettings = new();
            _uiSettings.ColorValuesChanged += _uiSettings_ColorValuesChanged;
            PropertyChanged += ThemeManager_PropertyChanged;
		}

        public void Initialize(Application app)
        {
            _App = app;
        }

		public void UpdateColors()
		{
			if (_App is null)
			{
				sbdotnet.Logger.Warning("sbwpf.ThemeManager: Initialize() must be called before InitColorScheme");
				return;
			}

            var dictionaries = _App.Resources.MergedDictionaries;
			for (int i = 0; i < dictionaries.Count -1; i++)
			{
				var dictionary = dictionaries[i].Source?.ToString();
				if (dictionary is null) continue;
				var dname = dictionary.ToLower();
				if (dictionary.Contains("colors.dark") || dictionary.Contains("colors.light"))
				{
					dictionaries.RemoveAt(i);
				}
            }

            var isDark = IsSystemInDarkMode();
            var themeSource = isDark
                ? new Uri($"Schemes/Colors.Dark.{SelectedDarkTheme}.xaml", UriKind.Relative)
                : new Uri($"Schemes/Colors.Light.{SelectedLightTheme}.xaml", UriKind.Relative);
        }

        private void _uiSettings_ColorValuesChanged(UISettings sender, object args)
        {
            if (_App is null)
            {
                sbdotnet.Logger.Warning("sbwpf.ThemeManager: Initialize() must be called before _uiSettings_ColorValuesChanged");
                return;
            }

			_App.Dispatcher.BeginInvoke(() => { UpdateColors(); });
        }

        private static bool IsSystemInDarkMode()
        {
            try
            {
                using var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
                if (key != null)
                {
                    var value = key.GetValue("AppsUseLightTheme");
                    if (value is int intValue)
                        return intValue == 0; // 0 = dark, 1 = light
                }
            }
            catch (Exception ex)
            {
                sbdotnet.Logger.Error(ex);
            }
            return false; // Fallback to light if detection fails
        }

		private void ThemeManager_PropertyChanged(object? sender, PropertyChangedEventArgs e)
		{
			if (_App is null)
			{
				sbdotnet.Logger.Warning("sbwpf.ThemeManager: Initialize() must be called before ThemeManager_PropertyChanged");
				return;
			}

			if (e.PropertyName is null) return;

			if (e.PropertyName.Equals(nameof(SelectedLightTheme)))
			{
				_App.Dispatcher.BeginInvoke(UpdateColors);
				LightThemeChanged?.Invoke(this, new ThemeChangedEventArgs(SelectedLightTheme));
			}
			else if (e.PropertyName.Equals(nameof(SelectedDarkTheme)))
			{
				_App.Dispatcher.BeginInvoke(UpdateColors);
				DarkThemeChanged?.Invoke(this, new ThemeChangedEventArgs(SelectedDarkTheme));
			}
		}
    }
}
