﻿using System;
using System.IO;

namespace Orion.Launcher {
    internal class Program {
        internal static void Main(string[] args) {
            AppDomain.CurrentDomain.UnhandledException += (sender, eventArgs) => {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Resources.strings.UnhandledExceptionMessage, eventArgs.ExceptionObject);
                Console.ResetColor();
            };

            Directory.CreateDirectory(Resources.strings.PluginDirectory);

            using (var kernel = new OrionKernel()) {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(Resources.strings.LoadingPluginsMessage);

                foreach (var path in Directory.EnumerateFiles(Resources.strings.PluginDirectory, "*.dll")) {
                    try {
                        kernel.QueuePluginsFromPath(path);
                    } catch (Exception ex) when (ex is BadImageFormatException || ex is IOException) { }
                }

                kernel.FinishLoadingPlugins(plugin => Console.WriteLine(Resources.strings.LoadedPluginMessage,
                                                                        plugin.Name, plugin.Version, plugin.Author));

                Console.ResetColor();
                Console.WriteLine();

                Terraria.WindowsLaunch.Main(args);
            }
        }
    }
}
