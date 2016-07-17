using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Events
{
	public class NPCTeleportingEventArgs : NPCHandledEventArgs
	{
		public int TargetX;
		public int TargetY;
		public int TeleportStyle;
	}
}
