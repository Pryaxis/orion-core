using Orion.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Modules.Console
{
    using Framework;
    using OTA;
    using System.Threading;
    using Console = System.Console;

    [OrionModule("OrionConsole", "Nyx Studios", Description = "Provides console support for Orion")]
    [DependsOn(typeof(Hooks.OTAPIHookModule))]
    public class ConsoleModule : OrionModuleBase, IConsoleProvider
    {
        protected Thread commandInputThread;

        public event EventHandler<Framework.Events.ConsoleLineEventArgs> ConsoleLine;

        public BasePlayer ActivePlayer { get; set; }

        public ConsoleModule(Orion core)
            : base(core)
        {
            Core.Hooks.ServerCommandThreadStarting += Hooks_ServerCommandThreadStarting;
        }

        public override void Initialize()
        {
            
        }

        private void Hooks_ServerCommandThreadStarting(Orion orion, Framework.Events.OrionEventArgs e)
        {
            /*
           * If stdin is redirected, there is no point in starting a command
           * input thread.
           */
            if (Console.IsInputRedirected == true)
            {
                return;
            }

            if (commandInputThread == null)
            {
                commandInputThread = new Thread(ServerInputThread);
                commandInputThread.IsBackground = true;
                commandInputThread.Start();
            }
        }

        protected string GetUserPrompt()
        {
            if (ActivePlayer == null)
            {
                return "server";
            }

            return ActivePlayer.SenderName;
        }

        private void PrintPrompt()
        {
            var originalColour = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(GetUserPrompt());
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("@");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"orion-{Core.Version.Major}.{Core.Version.Minor}");
            Console.ForegroundColor = originalColour;
            Console.Write(" > ");
        }

        private void ServerInputThread()
        {
            while (true)
            {
                PrintPrompt();
                string line = Console.ReadLine();

                if (string.IsNullOrEmpty(line) == true)
                {
                    continue;
                }

                if (ConsoleLine != null)
                {
                    ConsoleLine(this, new Framework.Events.ConsoleLineEventArgs() {
                        Player = ActivePlayer,
                        Line = line
                    });
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Core.Hooks.ServerCommandThreadStarting -= Hooks_ServerCommandThreadStarting;
            }
        }
    }
}
