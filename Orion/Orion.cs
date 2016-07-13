using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terraria;

namespace Orion
{
    public class Orion
    {

        protected string[] CommandArguments { get; set; }

        public Orion(params string[] args)
        {
            this.CommandArguments = args;
        }

        public void StartServer()
        {
            WindowsLaunch.Main(CommandArguments);
        }
    }
}
