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
    //TODO: Allow commands to specify permissions.
    //TODO: Logging of exceptions and issues.
    //TODO: Hook into a chat hook and process command strings.
    //TODO: Config options such as what the command specifier character is.
    [OrionModule("Command Provider", "Nyx Studios", Description = "Allows for other modules to register commands for use in-game by players.")]
    [DependsOn(typeof(Modules.Configuration.ConfigurationModule))]
    public class CommandProviderModule : OrionModuleBase
    {
        public readonly CommandManager Commands;
        private ConsolePlayer CPlayer { get; set; } = new ConsolePlayer();

        public CommandConfiguration Configuration { get; set; }

        public CommandProviderModule(Orion core) : base(core)
        {
            this.RegisterConfigurationProperty(p => p.Configuration);
                        
            Commands = new CommandManager();
            Core.ConsoleModule.ConsoleLine += ConsoleModule_ConsoleLine;
            Commands.AddCommand<BasePlayer, string>("help", HelpCommand);
            Commands.AddCommand<BasePlayer>("help", HelpCommand);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        private void ConsoleModule_ConsoleLine(object sender, ConsoleLineEventArgs e)
        {
            RunCommand(e.Player, e.Line);
        }

        private void HelpCommand(BasePlayer ply, string helpText)
        {
            
        }

        private void HelpCommand(BasePlayer ply)
        {

        }

        public void RunCommand(BasePlayer player, string commandString)
        {
            Commands.ParseAndCallCommand(player, commandString);
        }
    }
}
