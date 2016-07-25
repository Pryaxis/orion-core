using System;
using NUnit.Framework;
using Orion.Events.Item;

namespace Orion.Tests.Events.Item
{
	[TestFixture]
	public class ItemSetDefaultsEventArgsTests
	{
		[Test]
		public void Constructor_NullItem_ThrowsException()
		{
			Assert.Throws<ArgumentNullException>(() => new ItemSetDefaultsEventArgs(null));
		}
	}
}
