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
            string resourcePrefix = "sbwpf.Themer.Symbols.";
            var assembly = Assembly.GetExecutingAssembly();

            var symbolResources = assembly.GetManifestResourceNames().Where(r =>
                r.StartsWith(resourcePrefix, StringComparison.CurrentCultureIgnoreCase) &&
                r.EndsWith(".png", StringComparison.CurrentCultureIgnoreCase)).ToList();

            foreach (var resource in symbolResources)
            {
                string resourceName = resource[resourcePrefix.Length..];
                string symbolName = resourceName.Replace(".png", "");
                string uriString = $"pack://application:,,,/{assembly.GetName().Name};component/Symbols/{resourceName}";

                ThemeSymbols.Add(symbolName, new BitmapImage(new(uriString)));
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
                pixels[i] = targetColor.B;
                pixels[i + 1] = targetColor.G;
                pixels[i + 2] = targetColor.R;
                pixels[i + 3] = pixels[i + 3]; // Preserve alpha channel
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
