using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace QuizApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
        builder.Logging.AddDebug();
#endif

            try
            {
                var crashPath = Path.Combine(FileSystem.AppDataDirectory, "crash.log");

                AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                {
                    try
                    {
                        var text = e.ExceptionObject != null ? e.ExceptionObject.ToString() : "Unhandled exception";
                        File.WriteAllText(crashPath, text);
                    }
                    catch { }
                };

                TaskScheduler.UnobservedTaskException += (s, e) =>
                {
                    try
                    {
                        File.AppendAllText(crashPath, "\nTask Unobserved: " + e.Exception?.ToString());
                    }
                    catch { }
                };
            }
            catch { }

            return builder.Build();
        }
    }
}
