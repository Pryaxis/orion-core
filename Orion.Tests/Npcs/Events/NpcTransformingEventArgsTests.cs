using System;
using NUnit.Framework;
using Orion.Npcs.Events; 

namespace Orion.Tests.Npcs.Events
{
	[TestFixture]
	public class NpcTransformingEventArgsTests
	{
		[Test]
		public void Constructor_NullNpc_ThrowsArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => new NpcTransformingEventArgs(null, global::Orion.Npcs.NpcType.None));
		}
	}
}
