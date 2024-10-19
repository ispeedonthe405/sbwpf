using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Media;

namespace sbwpf.Core
{
    public class ColorHexConverter : JsonConverter<System.Windows.Media.Color>
    {
        public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
            (Color)System.Windows.Media.ColorConverter.ConvertFromString(reader.GetString());

        public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options) =>
            writer.WriteStringValue(value.ToString());
    }

    public class SolidColorBrushConverter : JsonConverter<System.Windows.Media.SolidColorBrush>
    {
        public override SolidColorBrush? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            SolidColorBrush brush = new()
            {
                Color = (Color)System.Windows.Media.ColorConverter.ConvertFromString(reader.GetString())
            };
            brush.Opacity = MathUtils.NormalizeByte(brush.Color.A);
            return brush;
        }

        public override void Write(Utf8JsonWriter writer, SolidColorBrush value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Color.ToString());
        }
    }

    public static class JsonUtil
    {
        
    }
}
