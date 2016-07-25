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
			// Setup for tests that are highly coupled to Terraria
			Terraria.Main.dedServ = true;
			new Terraria.Main().Initialize();
			Terraria.Lang.setLang();

			Environment.CurrentDirectory = Path.GetDirectoryName(this.GetType().Assembly.Location);
		}
	}
}
