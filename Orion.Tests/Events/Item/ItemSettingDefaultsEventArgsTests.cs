using System;
using NUnit.Framework;
using Orion.Events.Item;

namespace Orion.Tests.Events.Item
{
	[TestFixture]
	public class ItemSettingDefaultsEventArgsTests
	{
		[Test]
		public void Constructor_Null_ThrowsException()
		{
			Assert.Throws<ArgumentNullException>(() => new ItemSettingDefaultsEventArgs(null, 0));
		}
	}
}
