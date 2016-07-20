using NUnit.Framework;
using System;
using System.IO;

namespace Orion.Tests
{
	[SetUpFixture]
	public class Setup
	{
		[OneTimeSetUp]
		public void TestSetup()
		{
			Environment.CurrentDirectory = Path.GetDirectoryName(this.GetType().Assembly.Location);
		}
	}
}
