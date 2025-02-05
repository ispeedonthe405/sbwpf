using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Diagnostics;
using System.Windows;
using sbwpf.Core;
using System.Reflection;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace sbwpf.Themer
{
    /// <summary>
    /// Use instead of System.Windows.Controls.Image to automatically update the image source when the theme changes.
    /// </summary>
    public partial class ThemeSymbol : Image, INotifyPropertyChanged
    {
        ///////////////////////////////////////////////////////////
        #region INotifyPropertyChanged
        /////////////////////////////

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

        /////////////////////////////
        #endregion INotifyPropertyChanged
        ///////////////////////////////////////////////////////////

        public static readonly DependencyProperty SymbolNameProperty =
            DependencyProperty.Register(
                "SymbolName",
                typeof(string),
                typeof(ThemeSymbol),
                new PropertyMetadata("add", OnSymbolNameChanged),
                SymbolNameValidationCallback);

        private static void OnSymbolNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ThemeSymbol dynamicImage)
            {
                dynamicImage.LoadSymbol(dynamicImage.SymbolName);
            }
        }

        public string SymbolName
        {
            get { return (string)GetValue(SymbolNameProperty); }
            set 
            { 
                SetValue(SymbolNameProperty, value);
                LoadSymbol(SymbolName);
            }
        }

        private static bool SymbolNameValidationCallback(object value)
        {
            if (value is not string) return false;
            return true;
        }

        private void LoadSymbol(string symbolName)
        {
            try
            {
                string prefix = "sbwpf.Themer.Symbols.";
                string suffix = ".png";
                string resourceName = $"{prefix}{symbolName}{suffix}";

                using (var assemblyResource = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                {
                    if (assemblyResource is not null)
                    {
                        BitmapImage bmi = new();
                        bmi.BeginInit();
                        bmi.StreamSource = assemblyResource;
                        bmi.CacheOption = BitmapCacheOption.OnLoad;
                        bmi.EndInit();
                        BitmapSource src = RecolorImage(bmi, ThemeManager.ActiveTheme.SymbolColor);
                        Source = src;
                    }
                }
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private BitmapSource RecolorImage(BitmapSource source, Color targetColor)
        {
            FormatConvertedBitmap newFormat = new();
            newFormat.BeginInit();
            newFormat.Source = source;
            newFormat.DestinationFormat = PixelFormats.Pbgra32;
            newFormat.EndInit();

            var pixels = new byte[newFormat.PixelWidth * newFormat.PixelHeight * 4];
            newFormat.CopyPixels(pixels, newFormat.PixelWidth * 4, 0);

            for (int i = 0; i < pixels.Length; i += 4)
            {
                // Skip the transparent pixels
                if (pixels[i + 3] == 0)
                {
                    continue;
                }

                // Color pixels are overwritten with the new color, preserving alpha value
                pixels[i] = targetColor.B;
                pixels[i + 1] = targetColor.G;
                pixels[i + 2] = targetColor.R;
                pixels[i + 3] = pixels[i + 3];
            }

            var newBitmap = BitmapSource.Create(
                newFormat.PixelWidth, newFormat.PixelHeight,
                newFormat.DpiX, newFormat.DpiY,
                newFormat.Format, newFormat.Palette,
                pixels, newFormat.PixelWidth * 4);

            return newBitmap;
        }

        public ThemeSymbol() : base()
        {
            LoadSymbol(SymbolName);
            ThemeManager.ThemeChanged += (sender, e) => LoadSymbol(SymbolName);
        }
    }
}
