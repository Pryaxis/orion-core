// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
// 
// This file is part of Orion.
// 
// Orion is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Orion is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Orion.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.IO;
using System.Text;
using System.Threading;
using Orion.Launcher.Properties;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace Orion.Launcher {
    internal class Program {
        private static void SetupLogging() {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            
            Directory.CreateDirectory("logs");
            Log.Logger = new LoggerConfiguration()
#if DEBUG
                         .MinimumLevel.Verbose()
#else
                         .MinimumLevel.Information()
#endif
                         .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                         .WriteTo.File(Path.Combine("logs", "log-.txt"),
                                       rollingInterval: RollingInterval.Day,
                                       rollOnFileSizeLimit: true,
                                       fileSizeLimitBytes: 2 << 20)
                         .CreateLogger();

            AppDomain.CurrentDomain.UnhandledException += (sender, eventArgs) => {
                Log.Fatal(eventArgs.ExceptionObject as Exception, Resources.UnhandledExceptionMessage);
            };
        }

        private static void SetupPlugins(OrionKernel kernel) {
            Directory.CreateDirectory("plugins");
            Log.Information(Resources.LoadingPluginsMessage);

            foreach (var path in Directory.EnumerateFiles("plugins", "*.dll")) {
                try {
                    Log.Information(Resources.LoadingPluginMessage, path);

                    kernel.QueuePluginsFromPath(path);
                } catch (Exception ex) when (ex is BadImageFormatException || ex is IOException) {
                    Log.Information(ex, Resources.FailedToLoadPluginMessage, path);
                }
            }

            kernel.FinishLoadingPlugins(
                p => Log.Information(Resources.LoadedPluginMessage, p.Name, p.Version, p.Author));
            Console.ResetColor();
            Console.WriteLine();
        }

        private static void SetupLanguage() {
            // Save cultures since LanguageManager.SetLanguage overrides them.
            var previousCulture = Thread.CurrentThread.CurrentCulture;
            var previousUICulture = Thread.CurrentThread.CurrentUICulture;
            Terraria.Localization.LanguageManager.Instance.SetLanguage(previousUICulture.Name);
            Terraria.Lang.InitializeLegacyLocalization();
            Thread.CurrentThread.CurrentCulture = previousCulture;
            Thread.CurrentThread.CurrentUICulture = previousUICulture;
        }

        // TODO: provide event to use these arguments.
        internal static void Main(string[] args) {
            using var kernel = new OrionKernel();
            
            SetupLogging();
            SetupPlugins(kernel);
            SetupLanguage();

            using var game = new Terraria.Main();
            game.DedServ();
        }
    }
}
