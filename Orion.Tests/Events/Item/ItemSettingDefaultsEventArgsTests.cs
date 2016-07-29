using System;
using NUnit.Framework;
using Orion.Items.Events;

namespace Orion.Tests.Events.Item
{
	[TestFixture]
	public class ItemSettingDefaultsEventArgsTests
	{
		[Test]
		public void Constructor_NullItem_ThrowsArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => new ItemSettingDefaultsEventArgs(null, 0));
		}
	}
}
