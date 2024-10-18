using System.Windows;
using System.Windows.Media;

namespace sbwpf.Themer
{
    public class Theme
    {
        ///////////////////////////////////////////////////////////
        #region Fields
        /////////////////////////////

        private string _Name = string.Empty;
        private string _Description = string.Empty;
        private Color _SymbolColor = Colors.Wheat;
        private ResourceDictionary _Resource = [];

        /////////////////////////////
        #endregion Fields
        ///////////////////////////////////////////////////////////


        ///////////////////////////////////////////////////////////
        #region Properties
        /////////////////////////////

        public string Name
        {
            get => _Name;
            set => _Name = value;
        }

        public string Description
        {
            get => _Description;
            set => _Description = value;
        }

        public Color SymbolColor
        {
            get => _SymbolColor;
            set => _SymbolColor = value;
        }

        public ResourceDictionary Resource
        {
            get => _Resource;
            set => _Resource = value;
        }

        /////////////////////////////
        #endregion Properties
        ///////////////////////////////////////////////////////////

        public Theme()
        {

        }

        public Theme(string displayName, string description, Color symbolColor, ResourceDictionary resource)
        {
            Name = displayName;
            Description = description;
            SymbolColor = symbolColor;
            Resource = resource;
        }
    }
}
