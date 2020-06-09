// Copyright (c) 2020 Pryaxis & Orion Contributors
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
using System.Reflection;
using System.Text;
using System.Threading;
using Destructurama;
using Orion.Core;
using Orion.Core.Events.Server;
using Orion.Core.Launcher.Properties;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace Orion.Launcher {
    /// <summary>
    /// Holds the main logic for the launcher.
    /// </summary>
    public static class Program {
        // Sets up and returns a log.
        private static ILogger SetupLog() {
            Directory.CreateDirectory("logs");

            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            var log = new LoggerConfiguration()
                .Destructure.UsingAttributes()
#if DEBUG
                .MinimumLevel.Is(LogEventLevel.Debug)
#else
                .MinimumLevel.Is(LogEventLevel.Information)
#endif
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Name}: {Message:l}{NewLine}{Exception}",
                    theme: AnsiConsoleTheme.Code)
                .WriteTo.File(Path.Combine("logs", "log-.txt"),
                    outputTemplate:
                        "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Name}: {Message:l}{NewLine}{Exception}",
                    rollingInterval: RollingInterval.Day,
                    rollOnFileSizeLimit: true,
                    fileSizeLimitBytes: 2 << 20)
                .CreateLogger()
                .ForContext("Name", "launcher");

            AppDomain.CurrentDomain.UnhandledException += (sender, eventArgs) => {
                log.Fatal(eventArgs.ExceptionObject as Exception, Resources.UnhandledExceptionMessage);
            };

            return log;
        }

        // Sets up plugins.
        private static void SetupPlugins(OrionKernel kernel) {
            Directory.CreateDirectory("plugins");

            foreach (var path in Directory.EnumerateFiles("plugins", "*.dll")) {
                try {
                    var assembly = Assembly.LoadFile(path);
                    kernel.LoadFrom(assembly);
                } catch (BadImageFormatException) { }
            }

            kernel.Initialize();
        }

        // Sets up the language.
        private static void SetupLanguage() {
            // Save cultures since `LanguageManager.SetLanguage` overrides them if the language is not supported by
            // Terraria.
            var previousCulture = Thread.CurrentThread.CurrentCulture;
            var previousUICulture = Thread.CurrentThread.CurrentUICulture;
            Terraria.Localization.LanguageManager.Instance.SetLanguage(previousUICulture.Name);
            Terraria.Lang.InitializeLegacyLocalization();
            Thread.CurrentThread.CurrentCulture = previousCulture;
            Thread.CurrentThread.CurrentUICulture = previousUICulture;
        }

        /// <summary>
        /// Acts as the main entry point of the launcher.
        /// </summary>
        /// <param name="args">The arguments supplied to the launcher.</param>
        public static void Main(string[] args) {
            var log = SetupLog();
            using var kernel = new OrionKernel(log);
            SetupPlugins(kernel);
            SetupLanguage();

            kernel.Raise(new ServerArgsEvent(args), log);

            using var game = new Terraria.Main();
            game.DedServ();
        }
    }
}
