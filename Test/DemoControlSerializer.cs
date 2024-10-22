using sbwpf.Controls;
using sbwpf.Core;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace sbwpf.Demo
{
    internal class DemoControlSerializer : IControlSerializer
    {
        public static DemoControlSerializer Instance = new();

        private string DataPath = string.Empty;

        string IControlSerializer.Deserialize(string controlId, string parameter)
        {
            try
            {
                if (File.Exists(DataPath))
                {
                    return File.ReadAllText(DataPath);
                }
            }
            catch(Exception ex)
            {
                Logger.Debug(ex);
            }
            return string.Empty;
        }

        void IControlSerializer.Serialize(string controlId, string parameter, string value)
        {
            File.WriteAllText(DataPath, value);
        }

        public DemoControlSerializer() 
        {
            try
            {
                string? processPath = Environment.ProcessPath;
                if (processPath is null)
                {
                    Logger.Debug("Environment.ProcessPath = null");
                    return;
                }
                DataPath = Path.Combine(
                    Path.GetDirectoryName(processPath) ?? string.Empty,
                    "dgex.json");
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
            }
        }
    }
}
