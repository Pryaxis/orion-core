using System;
using System.IO;
using Orion.Framework;
using Ninject;
using OTAPI;

namespace Orion.Launcher {
    internal class Program {
        internal static void Main(string[] args) {
            Directory.CreateDirectory(Resources.strings.PluginDirectory);

            using (var kernel = new StandardKernel(new OrionNinjectModule(Resources.strings.PluginDirectory))) {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(Resources.strings.LoadingServicesAndPluginsMessage);
                foreach (var service in kernel.GetAll<IService>()) {
                    Console.WriteLine(Resources.strings.LoadedServiceMessage,
                                      service.Name, service.Version, service.Author);
                }
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
