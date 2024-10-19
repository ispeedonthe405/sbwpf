using sbwpf.Core;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace sbwpf.Themer
{
    public static class ThemeSymbolManager
    {
        public static Dictionary<string, BitmapSource> ThemeSymbols { get; private set; } = [];

        private static void BuildSymbols()
        {
            string prefix = "sbwpf.Themer.Symbols.";
            string suffix = ".png";
            var symbolResources = IoUtil.GetResourceNames(prefix, suffix);
            foreach (var resource in symbolResources)
            {
                using (var assemblyResource = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
                {
                    if (assemblyResource is not null)
                    {
                        BitmapImage bmi = new();
                        bmi.BeginInit();
                        bmi.StreamSource = assemblyResource;
                        bmi.CacheOption = BitmapCacheOption.OnLoad;
                        bmi.EndInit();
                        ThemeSymbols.Add(resource.Replace(prefix, "").Replace(suffix, ""), bmi);
                    }
                }
            }
        }

        private static BitmapSource RecolorImage(BitmapSource source, Color targetColor)
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
                if (pixels[i+3] == 0)
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

        private static void RecolorSymbols()
        {
            foreach (var key in ThemeSymbols.Keys.ToList())
            {
                ThemeSymbols[key] = RecolorImage(ThemeSymbols[key], ThemeManager.ActiveTheme.SymbolColor);
            }
        }

        private static void ThemeManager_ThemeChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RecolorSymbols();
        }

        public static void Initialize()
        {
            BuildSymbols();
            ThemeManager.ThemeChanged += ThemeManager_ThemeChanged;
        }



#if OLD_CODE

        }

        public static DynamicSymbol? GetSymbol(string name)
        {
            List<DynamicSymbol>? list = SymbolLists.GetValueOrDefault(ThemeManager.ActiveTheme.SymbolColor);
            if (list is null) return null;

            return list.FirstOrDefault(s =>
            (s.SymbolColor == ThemeManager.ActiveTheme.SymbolColor) &&
            (s.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)));
        }
    }
#endif // OLD_CODE
    }
}
