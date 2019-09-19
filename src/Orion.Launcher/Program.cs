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
using Orion.Launcher.Properties;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using Terraria.ID;

namespace Orion.Launcher {
    internal class Program {
        internal static void Main(string[] args) {
            Directory.CreateDirectory(Resources.LogsDirectory);
            Directory.CreateDirectory(Resources.PluginDirectory);

            Log.Logger = new LoggerConfiguration()
#if DEBUG
                         .MinimumLevel.Verbose()
#else
                         .MinimumLevel.Information()
#endif
                         .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                         .WriteTo.File(Path.Combine(Resources.LogsDirectory, "log-.txt"),
                                       rollingInterval: RollingInterval.Day,
                                       rollOnFileSizeLimit: true,
                                       fileSizeLimitBytes: 2 << 20)
                         .CreateLogger();

            AppDomain.CurrentDomain.UnhandledException += (sender, eventArgs) => {
                Log.Fatal(eventArgs.ExceptionObject as Exception, Resources.UnhandledExceptionMessage);
            };

            var main = new Terraria.Main();
            main.Initialize();

            for (var i = 0; i < NPCID.Count; ++i) {
                if (Terraria.Main.npcCatchable[i]) {
                    Console.WriteLine(i);
                }
            }

            using (var kernel = new OrionKernel()) {
                Log.Information(Resources.LoadingPluginsMessage);

                foreach (var path in Directory.EnumerateFiles(Resources.PluginDirectory, "*.dll")) {
                    try {
                        Log.Information(Resources.LoadingPluginMessage, path);

                        kernel.QueuePluginsFromPath(path);
                    } catch (Exception ex) when (ex is BadImageFormatException || ex is IOException) {
                        Log.Information(ex, Resources.FailedToLoadPluginMessage, path);
                    }
                }

                kernel.FinishLoadingPlugins(
                    plugin => Log.Information(Resources.LoadedPluginMessage, plugin.Name, plugin.Version,
                                              plugin.Author));

                Console.ResetColor();
                Console.WriteLine();

                // Set SkipAssemblyLoad so that we don't JIT the social API.
                Terraria.Main.SkipAssemblyLoad = true;
                Terraria.Program.LaunchGame(args);
            }
        }
    }
}
