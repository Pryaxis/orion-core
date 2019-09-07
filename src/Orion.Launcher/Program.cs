using System;
using System.IO;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace Orion.Launcher {
    internal class Program {
        internal static void Main(string[] args) {
            Directory.CreateDirectory(Resources.strings.LogsDirectory);
            Directory.CreateDirectory(Resources.strings.PluginDirectory);

            Log.Logger = new LoggerConfiguration()
#if DEBUG
                         .MinimumLevel.Debug()
#else
                         .MinimumLevel.Information()
#endif
                         .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                         .WriteTo.File(Path.Combine(Resources.strings.LogsDirectory, "log-.txt"),
                                       rollingInterval: RollingInterval.Day,
                                       rollOnFileSizeLimit: true,
                                       fileSizeLimitBytes: 2 << 20)
                         .CreateLogger();

            AppDomain.CurrentDomain.UnhandledException += (sender, eventArgs) => {
                Log.Fatal(eventArgs.ExceptionObject as Exception, Resources.strings.UnhandledExceptionMessage);
            };

            using (var kernel = new OrionKernel()) {
                Log.Information(Resources.strings.LoadingPluginsMessage);
                foreach (var path in Directory.EnumerateFiles(Resources.strings.PluginDirectory, "*.dll")) {
                    try {
                        Log.Information(Resources.strings.LoadingPluginMessage, path);
                        kernel.QueuePluginsFromPath(path);
                    } catch (Exception ex) when (ex is BadImageFormatException || ex is IOException) {
                        Log.Information(ex, Resources.strings.FailedToLoadPluginMessage, path);
                    }
                }

                kernel.FinishLoadingPlugins(plugin => Log.Information(Resources.strings.LoadedPluginMessage,
                                                                      plugin.Name, plugin.Version, plugin.Author));

                Console.ResetColor();
                Console.WriteLine();

                Terraria.WindowsLaunch.Main(args);
            }
        }
    }
}
