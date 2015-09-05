using System;
using System.Collections.Generic;
using System.Dynamic;
using Terraria;
using Orion.UserAccounts;

namespace Orion
{
	public class OrionPlayer
	{
		[Temporary("Needs to return Main.player at a given index")]
		public Player Player { get { return null; } }

		public UserAccount User { get; set; }
	}
}