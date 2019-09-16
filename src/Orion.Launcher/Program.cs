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
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Orion.Entities;
using Orion.Launcher.Properties;
using Orion.World;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using Terraria;
using Terraria.ID;
using Terraria.Localization;

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

            Terraria.Main main = new Main();
            main.Initialize();
            LanguageManager.Instance.SetLanguage(GameCulture.English);
            Lang.InitializeLegacyLocalization();
            Terraria.Main.player[Terraria.Main.myPlayer] = new Player();
            SortedDictionary<(int id, int style), FieldInfo> items =
                new SortedDictionary<(int id, int style), FieldInfo>();
            for (var i = 0; i < ItemID.Count; ++i) {
                var item = new Item();
                item.SetDefaults(i);

                if (item.createTile >= 0 && item.createTile == 139) {
                    try {
                        items.Add((item.createTile, item.placeStyle), ItemType.IdToField[(short)i]);
                    } catch
                    {

                    }
                }
            }

            foreach (var kvp in items) {
                if (kvp.Key.style == 0) {
                    Log.Information("public static readonly BlockType {Name} = new BlockType({Id});", kvp.Value.Name, kvp.Key.id);
                } else {
                    Log.Information("public static readonly BlockType {Name} = new BlockType({Id}, {Style});", kvp.Value.Name, kvp.Key.id, kvp.Key.style);
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
