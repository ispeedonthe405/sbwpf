using System.IO;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Xml.Linq;

namespace sbwpf.Core
{
    public static class IoUtil
    {
        public static JsonSerializerOptions JsonWriterOptions
        {
            get { return new() { WriteIndented = true }; }
        }

        public static bool IsValidFilename(string filename)
        {
            if (filename.IsNull()) return false;
            if (filename.IndexOfAny(Path.GetInvalidFileNameChars()) != -1) return false;

            return true;
        }

        public static bool IsValidPathname(string pathname)
        {
            if (pathname.IsNull()) return false;
            if (pathname.IndexOfAny(Path.GetInvalidPathChars()) != -1) return false;

            return true;
        }

        public static void EnsureFilePath(string fullPath)
        {
            try
            {
                string? path = Path.GetDirectoryName(fullPath);
                if (path is not null)
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        public static void EnsureFolder(string fullPath)
        {
            try
            {
                string path = Path.GetFullPath(fullPath);
                DirectoryInfo di = Directory.CreateDirectory(path);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        public static string LoadResourceString(string path)
        {
            string result = string.Empty;

            try
            {
                using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(path))
                {
                    if (resource is not null)
                    {
                        using (var stream = new StreamReader(resource))
                        {
                            result = stream.ReadToEnd();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
            }

            return result;
        }
    }
}
