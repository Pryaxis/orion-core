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

            var itemTypeFields = typeof(ItemType).GetFields(BindingFlags.Public | BindingFlags.Static);
            var itemIdToField = new Dictionary<int, FieldInfo>();
            foreach (var field in itemTypeFields) {
                if (!(field.GetValue(null) is ItemType itemType)) continue;

                itemIdToField[itemType.Id] = field;
            }

            var itemRarityFields = typeof(ItemRarity).GetFields(BindingFlags.Public | BindingFlags.Static);
            var rarityLevelToField = new Dictionary<int, FieldInfo>();
            foreach (var field in itemRarityFields) {
                if (!(field.GetValue(null) is ItemRarity itemRarity)) continue;

                rarityLevelToField[itemRarity.Level] = field;
            }

            LanguageManager.Instance.SetLanguage(GameCulture.English);
            Lang.InitializeLegacyLocalization();
            Terraria.Main.player[Terraria.Main.myPlayer] = new Player();
            for (var i = 0; i < ItemID.Count; ++i) {
                var item = new Item();
                item.SetDefaults(i);
                if (ItemID.Sets.Deprecated[i]) continue;

                try {
                    if (item.maxStack != 1) {
                        Log.Information("[{ItemType}] = {MaxStackSize},", itemIdToField[i].Name, item.maxStack);
                    }
                } catch {

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
