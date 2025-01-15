using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Media;

namespace sbwpf.Core
{
    public static class Logger
    {
        public class LogEvent
        {
            public enum EventCategory
            {
                Information,
                Warning,
                Error,
                Notify,
                Debug
            }
            public EventCategory Category { get; set; }
            public DateTime Timestamp { get; set; }
            public string Message { get; set; } = string.Empty;
            public Color BrushColor { get; set; } = Colors.Orange;
        }

        public static ObservableCollection<LogEvent> Events = [];
        public static bool UseConsole { get; set; } = false;
        public static bool UseTrace { get; set; } = false;

        private static Color BrushColorFromCategory(LogEvent.EventCategory category)
        {
            Color color = Colors.Black;
            switch (category)
            {
                case LogEvent.EventCategory.Warning:
                    color = Colors.DarkOrange;
                    break;

                case LogEvent.EventCategory.Error:
                    color = Colors.OrangeRed;
                    break;

                case LogEvent.EventCategory.Notify:
                    color = Colors.Green;
                    break;
            }
            return color;
        }

        private static void NewEvent(LogEvent.EventCategory category, string message)
        {
            if (!Application.Current.Dispatcher.CheckAccess())
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(() => NewEvent(category, message)));
                return;
            }
            LogEvent ev = new()
            {
                Category = category,
                Timestamp = DateTime.Now,
                Message = message,
                BrushColor = BrushColorFromCategory(category)
            };
            Events.Add(ev);

            if (UseConsole)
            {
                LogToConsole(ev);
            }
            if (UseTrace)
            {
                LogToTrace(ev);
            }
        }

        private static void LogToConsole(LogEvent logEvent)
        {
            Console.WriteLine($"Logger:{logEvent.Category}:{logEvent.Message}");
        }

        private static void LogToTrace(LogEvent logEvent)
        {
            Trace.WriteLine($"Logger:{logEvent.Category}:{logEvent.Message}");
        }

        public static void Information(string message)
        {
            NewEvent(LogEvent.EventCategory.Information, message);
        }

        public static void Warning(string message)
        {
            NewEvent(LogEvent.EventCategory.Warning, message);
        }

        public static void Warning(Exception ex)
        {
            NewEvent(LogEvent.EventCategory.Warning, ex.Message);
        }

        public static void Error(string message)
        {
            NewEvent(LogEvent.EventCategory.Error, message);
        }

        public static void Error(string message, string funcName)
        {
            NewEvent(LogEvent.EventCategory.Error, $"{funcName}: {message}");
        }

        public static void Error(Exception ex)
        {
            NewEvent(LogEvent.EventCategory.Error, ex.Message);
        }

        public static void Notify(string message)
        {
            NewEvent(LogEvent.EventCategory.Notify, message);
        }

        public static void Exception(Exception ex)
        {
            // Show the exception message by default
            string message = ex.Message;

            // If possible, get a more detailed message consisting of
            // some data from the stack frame. I heart C#.
            System.Diagnostics.StackTrace stackTrace = new(ex);
            var frame = stackTrace.GetFrame(stackTrace.FrameCount - 1);
            if (frame is not null)
            {
                MethodBase? mb = frame.GetMethod();
                if (mb is not null)
                {
                    message = $"{mb.ReflectedType}.{mb.Name}{Environment.NewLine}{ex.Message}";
                }
            }
            NewEvent(LogEvent.EventCategory.Error, message);
        }
    }
}