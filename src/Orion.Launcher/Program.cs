// Copyright (c) 2019 Pryaxis & Orion Contributors
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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Destructurama;
using Ninject;
using Orion.Items;
using Orion.Launcher.Properties;
using Orion.Npcs;
using Orion.Players;
using Orion.Projectiles;
using Orion.World;
using Orion.World.TileEntities;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using Main = Terraria.Main;

namespace Orion.Launcher {
    internal class Program {
#if DEBUG
        private const LogEventLevel LogLevel = LogEventLevel.Debug;
#else
        private const LogEventLevel LogLevel = LogEventLevel.Information;
#endif

        private const int STD_OUTPUT_HANDLE = -11;
        private const int ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        private static ILogger SetupLogging() {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            // If Windows, we should enable 256-color mode.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                var stdoutHandle = GetStdHandle(STD_OUTPUT_HANDLE);
                if (stdoutHandle != (IntPtr)(-1) && GetConsoleMode(stdoutHandle, out var mode)) {
                    SetConsoleMode(stdoutHandle, mode | ENABLE_VIRTUAL_TERMINAL_PROCESSING);
                }
            }

            Directory.CreateDirectory("logs");
            var log = new LoggerConfiguration()
                .Destructure.UsingAttributes()
                .MinimumLevel.Is(LogLevel)
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {ServiceName}: {Message:l}{NewLine}{Exception}",
                    theme: AnsiConsoleTheme.Code)
                .WriteTo.File(Path.Combine("logs", "log-.txt"),
                    rollingInterval: RollingInterval.Day,
                    rollOnFileSizeLimit: true,
                    fileSizeLimitBytes: 2 << 20)
                .CreateLogger();

            AppDomain.CurrentDomain.UnhandledException += (sender, eventArgs) => {
                log.Fatal(eventArgs.ExceptionObject as Exception, Resources.UnhandledExceptionMessage);
            };

            return log;
        }

        private static void SetupPlugins(OrionKernel kernel) {
            Directory.CreateDirectory("plugins");
            foreach (var path in Directory.EnumerateFiles("plugins", "*.dll")) {
                try {
                    kernel.StartLoadingPlugins(path);
                } catch (BadImageFormatException) { }
            }

            kernel.FinishLoadingPlugins();
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
            var log = SetupLogging();
            using var kernel = new OrionKernel(log);
            SetupPlugins(kernel);
            SetupLanguage();

            kernel.Get<IItemService>();
            kernel.Get<INpcService>();
            kernel.Get<ITileEntityService>();
            kernel.Get<IPlayerService>();
            kernel.Get<IProjectileService>();
            kernel.Get<IWorldService>();

            using var game = new Main();
            game.DedServ();
        }
    }
}
