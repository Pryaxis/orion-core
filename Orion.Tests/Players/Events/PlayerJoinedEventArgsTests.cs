using System;
using NUnit.Framework;
using Orion.Players.Events;

namespace Orion.Tests.Players.Events
{
	[TestFixture]
	public class PlayedJoinedEventArgsTests
	{
		[Test]
		public void Constructor_NullPlayer_ThrowsArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => new PlayerJoinedEventArgs(null));
		}
	}
}
