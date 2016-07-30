using System;
using NUnit.Framework;
using Orion.Items;
using Orion.Items.Events;

namespace Orion.Tests.Items.Events
{
	[TestFixture]
	public class ItemSettingDefaultsEventArgsTests
	{
		[Test]
		public void Constructor_NullItem_ThrowsArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => new ItemSettingDefaultsEventArgs(null, ItemType.None));
		}
	}
}
