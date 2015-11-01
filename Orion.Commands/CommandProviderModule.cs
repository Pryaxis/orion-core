using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Orion.Commands.Commands;
using Orion.Framework;
using Orion.Framework.Events;
using OTA;

namespace Orion.Commands
{
    //TODO: Allow commands to specify permissions.
    //TODO: Logging of exceptions and issues.
    //TODO: Hook into a chat hook and process command strings.
    //TODO: Config options such as what the command specifier character is.
    [OrionModule("Command Provider", "Nyx Studios", Description = "Allows for other modules to register commands for use in-game by players.")]
    public class CommandProviderModule : OrionModuleBase
    {
        public readonly CommandManager Commands;
        private ConsolePlayer CPlayer { get; set; } = new ConsolePlayer();
        protected Thread commandInputThread;

        public CommandProviderModule(Orion core) : base(core)
        {
            Commands = new CommandManager();
            Core.Hooks.ServerCommandThreadStarting += Core_ServerCommandThreadStarting;
            Commands.AddCommand<BasePlayer, string>("help", HelpCommand);
            Commands.AddCommand<BasePlayer>("help", HelpCommand);
        }

        private void HelpCommand(BasePlayer ply, string helpText)
        {
            
        }

        private void HelpCommand(BasePlayer ply)
        {

        }

        private void Core_ServerCommandThreadStarting(Orion orion, OrionEventArgs e)
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
            }
            
            commandInputThread.Start();
        }

        private void PrintPrompt()
        {
            var originalColour = Console.ForegroundColor;
            
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Orion");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("@");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{Core.Version.Major}.{Core.Version.Minor}");
            Console.ForegroundColor = originalColour;
            Console.Write(" > ");
        }

        private void ServerInputThread()
        {
            while (true)
            {
                PrintPrompt();
                
                string line = Console.ReadLine();
                
                RunCommand(CPlayer, line);
            }
        }

        public void RunCommand(BasePlayer player, string commandString)
        {
            Commands.ParseAndCallCommand(player, commandString);
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                commandInputThread.Abort();
            }
            
            base.Dispose(disposing);
        }
    }
}
