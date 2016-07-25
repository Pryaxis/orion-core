using System;
using NUnit.Framework;
using Orion.Events.Player;

namespace Orion.Tests.Events.Player
{
	[TestFixture]
	public class PlayedQuitEventArgsTests
	{
		[Test]
		public void Constructor_NullPlayer_ThrowsException()
		{
			Assert.Throws<ArgumentNullException>(() => new PlayerQuitEventArgs(null));
		}
	}
}
