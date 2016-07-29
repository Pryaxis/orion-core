using System;
using NUnit.Framework;
using Orion.Players.Events;

namespace Orion.Tests.Players.Events
{
	[TestFixture]
	public class PlayedJoiningEventArgsTests
	{
		[Test]
		public void Constructor_NullPlayer_ThrowsArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => new PlayerJoiningEventArgs(null));
		}
	}
}
