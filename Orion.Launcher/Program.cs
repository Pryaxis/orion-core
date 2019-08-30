namespace Orion.Launcher {
    using System;
    using System.IO;
    using Framework;
    using Ninject;

    internal class Program {
        internal static void Main(string[] args) {
            Directory.CreateDirectory(strings.PluginDirectory);

            using (var kernel = new StandardKernel(new OrionNinjectModule(strings.PluginDirectory))) {
                // Load all services.
                Console.ForegroundColor = ConsoleColor.Yellow;
                foreach (var service in kernel.GetAll<IService>()) {
                    Console.WriteLine(strings.LoadedServiceMessage, service.Name, service.Author);
                }
                Console.ResetColor();

                Terraria.WindowsLaunch.Main(args);
            }
        }
    }
}
