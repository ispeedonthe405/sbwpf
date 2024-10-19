using sbwpf.Core;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Media;

namespace sbwpf.Themer
{
    public class Theme
    {
        ///////////////////////////////////////////////////////////
        #region Fields

        private string _Name = string.Empty;

        private string _Description = string.Empty;

        private Color _SymbolColor = Colors.Wheat;

        private Color _BackgroundNormalColor;
        private Color _BackgroundSelectedColor;
        private Color _BackgroundInactiveColor;
        private Color _BackgroundDisabledColor;
        private Color _BackgroundMouseOverColor;
        private Color _BackgroundPressedColor;
        private Color _BackgroundLightColor;
        private Color _BackgroundMediumColor;
        private Color _BackgroundDarkColor;

        private SolidColorBrush _BackgroundNormalBrush = Brushes.Crimson;
        private SolidColorBrush _BackgroundSelectedBrush = Brushes.Crimson;
        private SolidColorBrush _BackgroundInactiveBrush = Brushes.Crimson;
        private SolidColorBrush _BackgroundDisabledBrush = Brushes.Crimson;
        private SolidColorBrush _BackgroundMouseOverBrush = Brushes.Crimson;
        private SolidColorBrush _BackgroundPressedBrush = Brushes.Crimson;
        private SolidColorBrush _BackgroundLightBrush = Brushes.Crimson;
        private SolidColorBrush _BackgroundMediumBrush = Brushes.Crimson;
        private SolidColorBrush _BackgroundDarkBrush = Brushes.Crimson;


        private Color _ForegroundNormalColor;
        private Color _ForegroundSelectedColor;
        private Color _ForegroundInactiveColor;
        private Color _ForegroundDisabledColor;
        private Color _ForegroundMouseOverColor;
        private Color _ForegroundPressedColor;
        private Color _ForegroundLightColor;
        private Color _ForegroundMediumColor;
        private Color _ForegroundDarkColor;

        private SolidColorBrush _ForegroundNormalBrush = Brushes.Crimson;
        private SolidColorBrush _ForegroundSelectedBrush = Brushes.Crimson;
        private SolidColorBrush _ForegroundInactiveBrush = Brushes.Crimson;
        private SolidColorBrush _ForegroundDisabledBrush = Brushes.Crimson;
        private SolidColorBrush _ForegroundMouseOverBrush = Brushes.Crimson;
        private SolidColorBrush _ForegroundPressedBrush = Brushes.Crimson;
        private SolidColorBrush _ForegroundLightBrush = Brushes.Crimson;
        private SolidColorBrush _ForegroundMediumBrush = Brushes.Crimson;
        private SolidColorBrush _ForegroundDarkBrush = Brushes.Crimson;


        private Color _ControlNormalColor;
        private Color _ControlSelectedColor;
        private Color _ControlInactiveColor;
        private Color _ControlDisabledColor;
        private Color _ControlMouseOverColor;
        private Color _ControlPressedColor;
        private Color _ControlLightColor;
        private Color _ControlMediumColor;
        private Color _ControlDarkColor;

        private SolidColorBrush _ControlNormalBrush = Brushes.Crimson;
        private SolidColorBrush _ControlSelectedBrush = Brushes.Crimson;
        private SolidColorBrush _ControlInactiveBrush = Brushes.Crimson;
        private SolidColorBrush _ControlDisabledBrush = Brushes.Crimson;
        private SolidColorBrush _ControlMouseOverBrush = Brushes.Crimson;
        private SolidColorBrush _ControlPressedBrush = Brushes.Crimson;
        private SolidColorBrush _ControlLightBrush = Brushes.Crimson;
        private SolidColorBrush _ControlMediumBrush = Brushes.Crimson;
        private SolidColorBrush _ControlDarkBrush = Brushes.Crimson;


        private Color _BorderNormalColor;
        private Color _BorderSelectedColor;
        private Color _BorderInactiveColor;
        private Color _BorderDisabledColor;
        private Color _BorderMouseOverColor;
        private Color _BorderPressedColor;
        private Color _BorderLightColor;
        private Color _BorderMediumColor;
        private Color _BorderDarkColor;

        private SolidColorBrush _BorderNormalBrush = Brushes.Crimson;
        private SolidColorBrush _BorderSelectedBrush = Brushes.Crimson;
        private SolidColorBrush _BorderInactiveBrush = Brushes.Crimson;
        private SolidColorBrush _BorderDisabledBrush = Brushes.Crimson;
        private SolidColorBrush _BorderMouseOverBrush = Brushes.Crimson;
        private SolidColorBrush _BorderPressedBrush = Brushes.Crimson;
        private SolidColorBrush _BorderLightBrush = Brushes.Crimson;
        private SolidColorBrush _BorderMediumBrush = Brushes.Crimson;
        private SolidColorBrush _BorderDarkBrush = Brushes.Crimson;


        private Color _HighlightNormalColor;
        private Color _HighlightSelectedColor;
        private Color _HighlightInactiveColor;
        private Color _HighlightDisabledColor;
        private Color _HighlightMouseOverColor;
        private Color _HighlightPressedColor;
        private Color _HighlightLightColor;
        private Color _HighlightMediumColor;
        private Color _HighlightDarkColor;

        private SolidColorBrush _HighlightNormalBrush = Brushes.Crimson;
        private SolidColorBrush _HighlightSelectedBrush = Brushes.Crimson;
        private SolidColorBrush _HighlightInactiveBrush = Brushes.Crimson;
        private SolidColorBrush _HighlightDisabledBrush = Brushes.Crimson;
        private SolidColorBrush _HighlightMouseOverBrush = Brushes.Crimson;
        private SolidColorBrush _HighlightPressedBrush = Brushes.Crimson;
        private SolidColorBrush _HighlightLightBrush = Brushes.Crimson;
        private SolidColorBrush _HighlightMediumBrush = Brushes.Crimson;
        private SolidColorBrush _HighlightDarkBrush = Brushes.Crimson;

        private ResourceDictionary _Resource = [];

        #endregion Fields
        ///////////////////////////////////////////////////////////


        ///////////////////////////////////////////////////////////
        #region Properties

        public string Name { get => _Name; set => _Name = value; }

        public string Description { get => _Description; set => _Description = value; }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color SymbolColor { get => _SymbolColor; set => _SymbolColor = value; }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color BackgroundNormalColor
        {
            get => _BackgroundNormalColor;
            set { _BackgroundNormalColor = value; SetBrush(ref _BackgroundNormalBrush, value); }
        }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color BackgroundSelectedColor
        {
            get => _BackgroundSelectedColor;
            set { _BackgroundSelectedColor = value; SetBrush(ref _BackgroundSelectedBrush, value); }
        }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color BackgroundInactiveColor
        {
            get => _BackgroundInactiveColor;
            set { _BackgroundInactiveColor = value; SetBrush(ref _BackgroundInactiveBrush, value); }
        }
    
        [JsonConverter(typeof(ColorHexConverter))]
        public Color BackgroundDisabledColor 
        { 
            get => _BackgroundDisabledColor; 
            set { _BackgroundDisabledColor = value; SetBrush(ref _BackgroundDisabledBrush, value); }
        }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color BackgroundMouseOverColor
        {
            get => _BackgroundMouseOverColor;
            set { _BackgroundMouseOverColor = value; SetBrush(ref _BackgroundMouseOverBrush, value); }
        }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color BackgroundPressedColor 
        { 
            get => _BackgroundPressedColor; 
            set { _BackgroundPressedColor = value; SetBrush(ref _BackgroundPressedBrush, value); }
        }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color BackgroundLightColor
        {
            get => _BackgroundLightColor;
            set { _BackgroundLightColor = value; SetBrush(ref _BackgroundLightBrush, value); }
        }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color BackgroundMediumColor 
        { 
            get => _BackgroundMediumColor; 
            set { _BackgroundMediumColor = value; SetBrush(ref _BackgroundMediumBrush, value); }
        }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color BackgroundDarkColor
        {
            get => _BackgroundDarkColor;
            set { _BackgroundDarkColor = value; SetBrush(ref _BackgroundDarkBrush, value); }
        }
        

        [JsonIgnore]public SolidColorBrush BackgroundNormalBrush { get => _BackgroundNormalBrush; set => _BackgroundNormalBrush = value; }
        [JsonIgnore]public SolidColorBrush BackgroundSelectedBrush { get => _BackgroundSelectedBrush; set => _BackgroundSelectedBrush = value; }
        [JsonIgnore]public SolidColorBrush BackgroundInactiveBrush { get => _BackgroundInactiveBrush; set => _BackgroundInactiveBrush = value; }
        [JsonIgnore]public SolidColorBrush BackgroundDisabledBrush { get => _BackgroundDisabledBrush; set => _BackgroundDisabledBrush = value; }
        [JsonIgnore]public SolidColorBrush BackgroundMouseOverBrush { get => _BackgroundMouseOverBrush; set => _BackgroundMouseOverBrush = value; }
        [JsonIgnore]public SolidColorBrush BackgroundPressedBrush { get => _BackgroundPressedBrush; set => _BackgroundPressedBrush = value; }
        [JsonIgnore]public SolidColorBrush BackgroundLightBrush { get => _BackgroundLightBrush; set => _BackgroundLightBrush = value; }
        [JsonIgnore]public SolidColorBrush BackgroundMediumBrush { get => _BackgroundMediumBrush; set => _BackgroundMediumBrush = value; }
        [JsonIgnore]public SolidColorBrush BackgroundDarkBrush { get => _BackgroundDarkBrush; set => _BackgroundDarkBrush = value; }


        [JsonConverter(typeof(ColorHexConverter))]
        public Color ForegroundNormalColor
        {
            get => _ForegroundNormalColor;
            set { _ForegroundNormalColor = value; SetBrush(ref _ForegroundNormalBrush, value); }
        }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color ForegroundSelectedColor
        {
            get => _ForegroundSelectedColor;
            set { _ForegroundSelectedColor = value; SetBrush(ref _ForegroundSelectedBrush, value); }
        }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color ForegroundInactiveColor
        {
            get => _ForegroundInactiveColor;
            set { _ForegroundInactiveColor = value; SetBrush(ref _ForegroundInactiveBrush, value); }
        }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color ForegroundDisabledColor
        {
            get => _ForegroundDisabledColor;
            set { _ForegroundDisabledColor = value; SetBrush(ref _ForegroundDisabledBrush, value); }
        }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color ForegroundMouseOverColor
        {
            get => _ForegroundMouseOverColor;
            set { _ForegroundMouseOverColor = value; SetBrush(ref _ForegroundMouseOverBrush, value); }
        }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color ForegroundPressedColor
        {
            get => _ForegroundPressedColor;
            set { _ForegroundPressedColor = value; SetBrush(ref _ForegroundPressedBrush, value); }
        }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color ForegroundLightColor
        {
            get => _ForegroundLightColor;
            set { _ForegroundLightColor = value; SetBrush(ref _ForegroundLightBrush, value); }
        }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color ForegroundMediumColor 
        { 
            get => _ForegroundMediumColor;
            set { _ForegroundMediumColor = value;  SetBrush(ref _ForegroundMediumBrush, value); }
        }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color ForegroundDarkColor
        {
            get => _ForegroundDarkColor;
            set { _ForegroundDarkColor = value; SetBrush(ref _ForegroundDarkBrush, value); }
        }

        [JsonIgnore]public SolidColorBrush ForegroundNormalBrush { get => _ForegroundNormalBrush; set => _ForegroundNormalBrush = value; }
        [JsonIgnore]public SolidColorBrush ForegroundSelectedBrush { get => _ForegroundSelectedBrush; set => _ForegroundSelectedBrush = value; }
        [JsonIgnore]public SolidColorBrush ForegroundInactiveBrush { get => _ForegroundInactiveBrush; set => _ForegroundInactiveBrush = value; }
        [JsonIgnore]public SolidColorBrush ForegroundDisabledBrush { get => _ForegroundDisabledBrush; set => _ForegroundDisabledBrush = value; }
        [JsonIgnore]public SolidColorBrush ForegroundMouseOverBrush { get => _ForegroundMouseOverBrush; set => _ForegroundMouseOverBrush = value; }
        [JsonIgnore]public SolidColorBrush ForegroundPressedBrush { get => _ForegroundPressedBrush; set => _ForegroundPressedBrush = value; }
        [JsonIgnore]public SolidColorBrush ForegroundLightBrush { get => _ForegroundLightBrush; set => _ForegroundLightBrush = value; }
        [JsonIgnore]public SolidColorBrush ForegroundMediumBrush { get => _ForegroundMediumBrush; set => _ForegroundMediumBrush = value; }
        [JsonIgnore]public SolidColorBrush ForegroundDarkBrush { get => _ForegroundDarkBrush; set => _ForegroundDarkBrush = value; }


        [JsonConverter(typeof(ColorHexConverter))]
        public Color ControlNormalColor
        {
            get => _ControlNormalColor;
            set { _ControlNormalColor = value; SetBrush(ref _ControlNormalBrush, value); }
        }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color ControlSelectedColor
        {
            get => _ControlSelectedColor;
            set { _ControlSelectedColor = value; SetBrush(ref _ControlSelectedBrush, value); }
        }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color ControlInactiveColor 
        { 
            get => _ControlInactiveColor; 
            set { _ControlInactiveColor = value; SetBrush(ref _ControlInactiveBrush, value); }
        }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color ControlDisabledColor 
        { 
            get => _ControlDisabledColor;
            set { _ControlDisabledColor = value; SetBrush(ref _ControlDisabledBrush, value); }
        }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color ControlMouseOverColor 
        { 
            get => _ControlMouseOverColor;
            set { _ControlMouseOverColor = value; SetBrush(ref _ControlMouseOverBrush, value); }
        }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color ControlPressedColor 
        { 
            get => _ControlPressedColor;
            set { _ControlPressedColor = value; SetBrush(ref _ControlPressedBrush, value); }
        }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color ControlLightColor 
        { 
            get => _ControlLightColor;
            set { _ControlLightColor = value; SetBrush(ref _ControlLightBrush, value); }
        }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color ControlMediumColor 
        { 
            get => _ControlMediumColor; 
            set { _ControlMediumColor = value; SetBrush(ref _ControlMediumBrush, value); }
        }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color ControlDarkColor 
        { 
            get => _ControlDarkColor;
            set { _ControlDarkColor = value; SetBrush(ref _ControlDarkBrush, value); }
        }

        [JsonIgnore]public SolidColorBrush ControlNormalBrush { get => _ControlNormalBrush; set => _ControlNormalBrush = value; }
        [JsonIgnore]public SolidColorBrush ControlSelectedBrush { get => _ControlSelectedBrush; set => _ControlSelectedBrush = value; }
        [JsonIgnore]public SolidColorBrush ControlInactiveBrush { get => _ControlInactiveBrush; set => _ControlInactiveBrush = value; }
        [JsonIgnore]public SolidColorBrush ControlDisabledBrush { get => _ControlDisabledBrush; set => _ControlDisabledBrush = value; }
        [JsonIgnore]public SolidColorBrush ControlMouseOverBrush { get => _ControlMouseOverBrush; set => _ControlMouseOverBrush = value; }
        [JsonIgnore]public SolidColorBrush ControlPressedBrush { get => _ControlPressedBrush; set => _ControlPressedBrush = value; }
        [JsonIgnore]public SolidColorBrush ControlLightBrush { get => _ControlLightBrush; set => _ControlLightBrush = value; }
        [JsonIgnore]public SolidColorBrush ControlMediumBrush { get => _ControlMediumBrush; set => _ControlMediumBrush = value; }
        [JsonIgnore]public SolidColorBrush ControlDarkBrush { get => _ControlDarkBrush; set => _ControlDarkBrush = value; }


        [JsonConverter(typeof(ColorHexConverter))]
        public Color BorderNormalColor 
        { 
            get => _BorderNormalColor;
            set { _BorderNormalColor = value; SetBrush(ref _BorderNormalBrush, value); }
        }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color BorderSelectedColor 
        { 
            get => _BorderSelectedColor;
            set { _BorderSelectedColor = value; SetBrush(ref _BorderSelectedBrush, value); }
        }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color BorderInactiveColor 
        { 
            get => _BorderInactiveColor;
            set { _BorderInactiveColor = value; SetBrush(ref _BorderInactiveBrush, value); }
        }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color BorderDisabledColor 
        { 
            get => _BorderDisabledColor;
            set { _BorderDisabledColor = value; SetBrush(ref _BorderDisabledBrush, value); }
        }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color BorderMouseOverColor
        {
            get => _BorderMouseOverColor;
            set { _BorderMouseOverColor = value; SetBrush(ref _BorderMouseOverBrush, value); }
        }


        [JsonConverter(typeof(ColorHexConverter))]
        public Color BorderPressedColor 
        { 
            get => _BorderPressedColor;
            set { _BorderPressedColor = value; SetBrush(ref _BorderPressedBrush, value); }
        }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color BorderLightColor 
        { 
            get => _BorderLightColor;
            set { _BorderLightColor = value; SetBrush(ref _BorderLightBrush, value); }
        }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color BorderMediumColor
        {
            get => _BorderMediumColor;
            set { _BorderMediumColor = value; SetBrush(ref _BorderMediumBrush, value); }
        }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color BorderDarkColor 
        { 
            get => _BorderDarkColor;
            set { _BorderDarkColor = value; SetBrush(ref _BorderDarkBrush, value); }
        }

        [JsonIgnore]public SolidColorBrush BorderNormalBrush { get => _BorderNormalBrush; set => _BorderNormalBrush = value; }
        [JsonIgnore]public SolidColorBrush BorderSelectedBrush { get => _BorderSelectedBrush; set => _BorderSelectedBrush = value; }
        [JsonIgnore]public SolidColorBrush BorderInactiveBrush { get => _BorderInactiveBrush; set => _BorderInactiveBrush = value; }
        [JsonIgnore]public SolidColorBrush BorderDisabledBrush { get => _BorderDisabledBrush; set => _BorderDisabledBrush = value; }
        [JsonIgnore]public SolidColorBrush BorderMouseOverBrush { get => _BorderMouseOverBrush; set => _BorderMouseOverBrush = value; }
        [JsonIgnore]public SolidColorBrush BorderPressedBrush { get => _BorderPressedBrush; set => _BorderPressedBrush = value; }
        [JsonIgnore]public SolidColorBrush BorderLightBrush { get => _BorderLightBrush; set => _BorderLightBrush = value; }
        [JsonIgnore]public SolidColorBrush BorderMediumBrush { get => _BorderMediumBrush; set => _BorderMediumBrush = value; }
        [JsonIgnore]public SolidColorBrush BorderDarkBrush { get => _BorderDarkBrush; set => _BorderDarkBrush = value; }


        [JsonConverter(typeof(ColorHexConverter))]
        public Color HighlightNormalColor 
        { 
            get => _HighlightNormalColor;
            set { _HighlightNormalColor = value; SetBrush(ref _HighlightNormalBrush, value); }
        }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color HighlightSelectedColor 
        { 
            get => _HighlightSelectedColor;
            set { _HighlightSelectedColor = value; SetBrush(ref _HighlightSelectedBrush, value); }
        }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color HighlightInactiveColor 
        { 
            get => _HighlightInactiveColor;
            set { _HighlightInactiveColor = value; SetBrush(ref _HighlightInactiveBrush, value); }
        }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color HighlightDisabledColor 
        { 
            get => _HighlightDisabledColor;
            set { _HighlightDisabledColor = value; SetBrush(ref _HighlightDisabledBrush, value); }
        }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color HighlightMouseOverColor 
        { 
            get => _HighlightMouseOverColor;
            set { _HighlightMouseOverColor = value; SetBrush(ref _HighlightMouseOverBrush, value); }
        }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color HighlightPressedColor 
        { 
            get => _HighlightPressedColor;
            set { _HighlightPressedColor = value; SetBrush(ref _HighlightPressedBrush, value); }
        }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color HighlightLightColor 
        { 
            get => _HighlightLightColor;
            set { _HighlightLightColor = value; SetBrush(ref _HighlightLightBrush, value); }
        }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color HighlightMediumColor 
        { 
            get => _HighlightMediumColor;
            set { _HighlightMediumColor = value; SetBrush(ref _HighlightMediumBrush, value); }
        }

        [JsonConverter(typeof(ColorHexConverter))]
        public Color HighlightDarkColor 
        { 
            get => _HighlightDarkColor;
            set { _HighlightDarkColor = value; SetBrush(ref _HighlightDarkBrush, value); }
        }

        [JsonIgnore]public SolidColorBrush HighlightNormalBrush { get => _HighlightNormalBrush; set => _HighlightNormalBrush = value; }
        [JsonIgnore]public SolidColorBrush HighlightSelectedBrush { get => _HighlightSelectedBrush; set => _HighlightSelectedBrush = value; }
        [JsonIgnore]public SolidColorBrush HighlightInactiveBrush { get => _HighlightInactiveBrush; set => _HighlightInactiveBrush = value; }
        [JsonIgnore]public SolidColorBrush HighlightDisabledBrush { get => _HighlightDisabledBrush; set => _HighlightDisabledBrush = value; }
        [JsonIgnore]public SolidColorBrush HighlightMouseOverBrush { get => _HighlightMouseOverBrush; set => _HighlightMouseOverBrush = value; }
        [JsonIgnore]public SolidColorBrush HighlightPressedBrush { get => _HighlightPressedBrush; set => _HighlightPressedBrush = value; }
        [JsonIgnore]public SolidColorBrush HighlightLightBrush { get => _HighlightLightBrush; set => _HighlightLightBrush = value; }
        [JsonIgnore]public SolidColorBrush HighlightMediumBrush { get => _HighlightMediumBrush; set => _HighlightMediumBrush = value; }
        [JsonIgnore]public SolidColorBrush HighlightDarkBrush { get => _HighlightDarkBrush; set => _HighlightDarkBrush = value; }


        [JsonIgnore]
        public ResourceDictionary Resource
        {
            get => _Resource;
            set => _Resource = value;
        }

        #endregion Properties
        ///////////////////////////////////////////////////////////



        ///////////////////////////////////////////////////////////
        #region Interface

        public Theme()
        {

        }

        public static Theme? FromJson(string jsonString)
        {
            try
            {
                Theme? theme = JsonSerializer.Deserialize<Theme>(jsonString);
                if (theme is not null)
                {
                    theme.Resource = new()
                    {
                        { nameof(SymbolColor), theme.SymbolColor },
                        { nameof(BackgroundNormalColor), theme.BackgroundNormalColor },
                        { nameof(BackgroundSelectedColor), theme.BackgroundSelectedColor },
                        { nameof(BackgroundInactiveColor), theme.BackgroundInactiveColor },
                        { nameof(BackgroundDisabledColor), theme.BackgroundDisabledColor },
                        { nameof(BackgroundMouseOverColor), theme.BackgroundMouseOverColor },
                        { nameof(BackgroundPressedColor), theme.BackgroundPressedColor },
                        { nameof(BackgroundLightColor), theme.BackgroundLightColor },
                        { nameof(BackgroundMediumColor), theme.BackgroundMediumColor },
                        { nameof(BackgroundDarkColor), theme.BackgroundDarkColor },
                        { nameof(BackgroundNormalBrush), theme.BackgroundNormalBrush },
                        { nameof(BackgroundSelectedBrush), theme.BackgroundSelectedBrush },
                        { nameof(BackgroundInactiveBrush), theme.BackgroundInactiveBrush },
                        { nameof(BackgroundDisabledBrush), theme.BackgroundDisabledBrush },
                        { nameof(BackgroundMouseOverBrush), theme.BackgroundMouseOverBrush },
                        { nameof(BackgroundPressedBrush), theme.BackgroundPressedBrush },
                        { nameof(BackgroundLightBrush), theme.BackgroundLightBrush },
                        { nameof(BackgroundMediumBrush), theme.BackgroundMediumBrush },
                        { nameof(BackgroundDarkBrush), theme.BackgroundDarkBrush },
                        { nameof(ForegroundNormalColor), theme.ForegroundNormalColor },
                        { nameof(ForegroundSelectedColor), theme.ForegroundSelectedColor },
                        { nameof(ForegroundInactiveColor), theme.ForegroundInactiveColor },
                        { nameof(ForegroundDisabledColor), theme.ForegroundDisabledColor },
                        { nameof(ForegroundMouseOverColor), theme.ForegroundMouseOverColor },
                        { nameof(ForegroundPressedColor), theme.ForegroundPressedColor },
                        { nameof(ForegroundLightColor), theme.ForegroundLightColor },
                        { nameof(ForegroundMediumColor), theme.ForegroundMediumColor },
                        { nameof(ForegroundDarkColor), theme.ForegroundDarkColor },
                        { nameof(ForegroundNormalBrush), theme.ForegroundNormalBrush },
                        { nameof(ForegroundSelectedBrush), theme.ForegroundSelectedBrush },
                        { nameof(ForegroundInactiveBrush), theme.ForegroundInactiveBrush },
                        { nameof(ForegroundDisabledBrush), theme.ForegroundDisabledBrush },
                        { nameof(ForegroundMouseOverBrush), theme.ForegroundMouseOverBrush },
                        { nameof(ForegroundPressedBrush), theme.ForegroundPressedBrush },
                        { nameof(ForegroundLightBrush), theme.ForegroundLightBrush },
                        { nameof(ForegroundMediumBrush), theme.ForegroundMediumBrush },
                        { nameof(ForegroundDarkBrush), theme.ForegroundDarkBrush },
                        { nameof(ControlNormalColor), theme.ControlNormalColor },
                        { nameof(ControlSelectedColor), theme.ControlSelectedColor },
                        { nameof(ControlInactiveColor), theme.ControlInactiveColor },
                        { nameof(ControlDisabledColor), theme.ControlDisabledColor },
                        { nameof(ControlMouseOverColor), theme.ControlMouseOverColor },
                        { nameof(ControlPressedColor), theme.ControlPressedColor },
                        { nameof(ControlLightColor), theme.ControlLightColor },
                        { nameof(ControlMediumColor), theme.ControlMediumColor },
                        { nameof(ControlDarkColor), theme.ControlDarkColor },
                        { nameof(ControlNormalBrush), theme.ControlNormalBrush },
                        { nameof(ControlSelectedBrush), theme.ControlSelectedBrush },
                        { nameof(ControlInactiveBrush), theme.ControlInactiveBrush },
                        { nameof(ControlDisabledBrush), theme.ControlDisabledBrush },
                        { nameof(ControlMouseOverBrush), theme.ControlMouseOverBrush },
                        { nameof(ControlPressedBrush), theme.ControlPressedBrush },
                        { nameof(ControlLightBrush), theme.ControlLightBrush },
                        { nameof(ControlMediumBrush), theme.ControlMediumBrush },
                        { nameof(ControlDarkBrush), theme.ControlDarkBrush },
                        { nameof(BorderNormalColor), theme.BorderNormalColor },
                        { nameof(BorderSelectedColor), theme.BorderSelectedColor },
                        { nameof(BorderInactiveColor), theme.BorderInactiveColor },
                        { nameof(BorderDisabledColor), theme.BorderDisabledColor },
                        { nameof(BorderMouseOverColor), theme.BorderMouseOverColor },
                        { nameof(BorderPressedColor), theme.BorderPressedColor },
                        { nameof(BorderLightColor), theme.BorderLightColor },
                        { nameof(BorderMediumColor), theme.BorderMediumColor },
                        { nameof(BorderDarkColor), theme.BorderDarkColor },
                        { nameof(BorderNormalBrush), theme.BorderNormalBrush },
                        { nameof(BorderSelectedBrush), theme.BorderSelectedBrush },
                        { nameof(BorderInactiveBrush), theme.BorderInactiveBrush },
                        { nameof(BorderDisabledBrush), theme.BorderDisabledBrush },
                        { nameof(BorderMouseOverBrush), theme.BorderMouseOverBrush },
                        { nameof(BorderPressedBrush), theme.BorderPressedBrush },
                        { nameof(BorderLightBrush), theme.BorderLightBrush },
                        { nameof(BorderMediumBrush), theme.BorderMediumBrush },
                        { nameof(BorderDarkBrush), theme.BorderDarkBrush },
                        { nameof(HighlightNormalColor), theme.HighlightNormalColor },
                        { nameof(HighlightSelectedColor), theme.HighlightSelectedColor },
                        { nameof(HighlightInactiveColor), theme.HighlightInactiveColor },
                        { nameof(HighlightDisabledColor), theme.HighlightDisabledColor },
                        { nameof(HighlightMouseOverColor), theme.HighlightMouseOverColor },
                        { nameof(HighlightPressedColor), theme.HighlightPressedColor },
                        { nameof(HighlightLightColor), theme.HighlightLightColor },
                        { nameof(HighlightMediumColor), theme.HighlightMediumColor },
                        { nameof(HighlightDarkColor), theme.HighlightDarkColor },
                        { nameof(HighlightNormalBrush), theme.HighlightNormalBrush },
                        { nameof(HighlightSelectedBrush), theme.HighlightSelectedBrush },
                        { nameof(HighlightInactiveBrush), theme.HighlightInactiveBrush },
                        { nameof(HighlightDisabledBrush), theme.HighlightDisabledBrush },
                        { nameof(HighlightMouseOverBrush), theme.HighlightMouseOverBrush },
                        { nameof(HighlightPressedBrush), theme.HighlightPressedBrush },
                        { nameof(HighlightLightBrush), theme.HighlightLightBrush },
                        { nameof(HighlightMediumBrush), theme.HighlightMediumBrush },
                        { nameof(HighlightDarkBrush), theme.HighlightDarkBrush }
                    };

                    return theme;
                }
            }
            catch(Exception ex)
            {
                Logger.Debug(ex);
            }
            return null;
        }

        public string ToJson()
        {
            try
            {
                return JsonSerializer.Serialize<Theme>(this, Core.IoUtil.JsonWriterOptions);
            }
            catch(Exception ex)
            {
                Logger.Debug(ex);
            }
            return string.Empty;
        }

        #endregion Interface
        ///////////////////////////////////////////////////////////



        ///////////////////////////////////////////////////////////
        #region Internal

        private static void SetBrush(ref SolidColorBrush brush, Color color)
        {
            // This may look daft, but the incoming brush is frozen at this time. 
            // It can be replaced but not modified.

            SolidColorBrush newBrush = brush.Clone();
            newBrush.Color = color;
            newBrush.Opacity = MathUtils.NormalizeByte(brush.Color.A);
            brush = newBrush;
        }

        #endregion Internal
        ///////////////////////////////////////////////////////////
    }
}
