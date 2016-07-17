using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Events
{
	public class NPCHandledEventArgs : HandledEventArgs
	{
		public Terraria.NPC NPC { get; set; }
	}
}
