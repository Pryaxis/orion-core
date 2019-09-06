using System;
using System.IO;
using OTAPI;

namespace Orion.Launcher {
    internal class Program {
        internal static void Main(string[] args) {
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

                Hooks.Console.Write = WriteHandler;
                Hooks.Console.WriteLine = WriteLineHandler;

                Terraria.WindowsLaunch.Main(args);
            }
        }

        private static HookResult WriteLineHandler(ConsoleHookArgs value) {
            return HookResult.Continue;
        }

        private static HookResult WriteHandler(string message) {
            return HookResult.Continue;
        }
    }
}
