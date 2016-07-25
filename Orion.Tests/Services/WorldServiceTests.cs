using NUnit.Framework;
using Orion.Services;

namespace Orion.Tests.Services
{
	[TestFixture]
	public class WorldServiceTests
	{
		[TestCase(100, 100)]
		public void MeteorDropping_IsCorrect(int x, int y)
		{
			using (var orion = new Orion())
			using (var worldService = new WorldService(orion))
			{
				for (int i = x - 50; i < x + 50; ++i)
				{
					for (int j = y - 50; j < y + 50; ++j)
					{
						Terraria.Main.tile[i, j] = new Terraria.Tile();
					}
				}
				bool eventOccurred = false;
				worldService.MeteorDropping += (s, a) =>
				{
					eventOccurred = true;
					Assert.AreEqual(x, a.X);
					Assert.AreEqual(y, a.Y);
				};

				Terraria.WorldGen.meteor(x, y);

				Assert.IsTrue(eventOccurred, "MeteorDropping event should have occurred.");
			}
		}

		[TestCase(100, 100)]
		public void MeteorDropping_Handled_StopsMeteor(int x, int y)
		{
			using (var orion = new Orion())
			using (var worldService = new WorldService(orion))
			{
				for (int i = x - 50; i < x + 50; ++i)
				{
					for (int j = y - 50; j < y + 50; ++j)
					{
						Terraria.Main.tile[i, j] = new Terraria.Tile();
					}
				}
				worldService.MeteorDropping += (s, a) => a.Handled = true;

				Terraria.WorldGen.meteor(x, y);
				
				for (int i = x - 50; i < x + 50; ++i)
				{
					for (int j = y - 50; j < y + 50; ++j)
					{
						Assert.IsFalse(Terraria.Main.tile[i, j].type == Terraria.ID.TileID.Meteorite,
							"Meteor should not have dropped.");
					}
				}
			}
		}

		[TestCase(false)]
		[TestCase(true)]
		public void WorldSaving_IsCorrect(bool resetTime)
		{
			using (var orion = new Orion())
			using (var worldService = new WorldService(orion))
			{
				Terraria.Main.worldName = "";
				bool eventOccurred = false;
				worldService.WorldSaving += (s, a) =>
				{
					eventOccurred = true;
					Assert.AreEqual(resetTime, a.ResetTime);
				};

				Terraria.IO.WorldFile.saveWorld(false, resetTime);

				Assert.IsTrue(eventOccurred, "WorldSaving event should have occurred.");
			}
		}

		[TestCase(false)]
		public void WorldSaving_Handled_StopsSaving(bool resetTime)
		{
			using (var orion = new Orion())
			using (var worldService = new WorldService(orion))
			{
				Terraria.Main.worldName = "";
				worldService.WorldSaving += (s, a) => a.Handled = true;

				Terraria.IO.WorldFile.saveWorld(false, resetTime);

				Assert.AreNotEqual("World", Terraria.Main.worldName, "WorldSaving event should not have occurred.");
			}
		}

		[TestCase(true)]
		[TestCase(false)]
		public void WorldSaved_IsCorrect(bool resetTime)
		{
			using (var orion = new Orion())
			using (var worldService = new WorldService(orion))
			{
				bool eventOccurred = false;
				worldService.WorldSaved += (s, a) => eventOccurred = true;

				Terraria.IO.WorldFile.saveWorld(false, resetTime);

				Assert.IsTrue(eventOccurred, "WorldSaved event should have occurred.");
			}
		}
	}
}
