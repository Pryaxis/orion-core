using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Orion.Commands.Commands;
using Orion.Framework;
using Orion.Framework.Events;
using Orion.Extensions;
using OTA;

namespace Orion.Commands
{
    //TODO: Logging of exceptions and issues.
    //TODO: Hook into a chat hook and process command strings.
    [OrionModule("Command Provider", "Nyx Studios", Description = "Allows for other modules to register commands for use in-game by players.")]
    [DependsOn(typeof(Modules.Configuration.ConfigurationModule), typeof(Modules.Console.ConsoleModule))]
    public class CommandProviderModule : OrionModuleBase
    {
        public CommandConfiguration Configuration { get; set; }
        public CommandManager Commands { get; private set; }

        public CommandProviderModule(Orion core) : base(core)
        {
            Core.ConsoleModule.ConsoleLine += ConsoleModule_ConsoleLine;
            this.RegisterConfigurationProperty(p => p.Configuration);
                        
            Commands = new CommandManager(Configuration);
        }

        private void ConsoleModule_ConsoleLine(object sender, ConsoleLineEventArgs e)
        {
            RunCommand(e.Player, e.Line);
        }

        public void RunCommand(BasePlayer player, string commandString)
        {
            Commands.RunCommand(player, commandString);
        }
    }
}
