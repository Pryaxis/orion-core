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
				worldService.MeteorDropping += (sender, args) =>
				{
					eventOccurred = true;
					Assert.AreEqual(x, args.X);
					Assert.AreEqual(y, args.Y);
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
				worldService.MeteorDropping += (sender, args) => args.Handled = true;

				Terraria.WorldGen.meteor(x, y);

				bool foundMeteor = false;
				for (int i = x - 50; i < x + 50; ++i)
				{
					for (int j = y - 50; j < y + 50; ++j)
					{
						if (Terraria.Main.tile[i, j].type == Terraria.ID.TileID.Meteorite)
						{
							foundMeteor = true;
							break;
						}
					}
				}
				Assert.IsFalse(foundMeteor, "Meteor should not have dropped.");
			}
		}

		[TestCase(100, 100, 200, 200)]
		public void MeteorDropping_ModifiesXY(int x, int y, int newX, int newY)
		{
			using (var orion = new Orion())
			using (var worldService = new WorldService(orion))
			{
				for (int i = newX - 50; i < newX + 50; ++i)
				{
					for (int j = newY - 50; j < newY + 50; ++j)
					{
						Terraria.Main.tile[i, j] = new Terraria.Tile();
					}
				}
				worldService.MeteorDropping += (sender, args) =>
				{
					args.X = newX;
					args.Y = newY;
				};

				Terraria.WorldGen.meteor(x, y);

				bool foundMeteor = false;
				for (int i = newX - 50; i < newX + 50; ++i)
				{
					for (int j = newY - 50; j < newY + 50; ++j)
					{
						if (Terraria.Main.tile[i, j].type == Terraria.ID.TileID.Meteorite)
						{
							foundMeteor = true;
							break;
						}
					}
				}
				Assert.IsTrue(foundMeteor, "Meteor should have been moved.");
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
				Terraria.WorldGen.saveLock = false;
				bool eventOccurred = false;
				worldService.WorldSaving += (sender, args) =>
				{
					eventOccurred = true;
					Assert.AreEqual(resetTime, args.ResetTime);
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
				Terraria.WorldGen.saveLock = false;
				worldService.WorldSaving += (sender, args) => args.Handled = true;

				Terraria.IO.WorldFile.saveWorld(false, resetTime);

				Assert.AreNotEqual("World", Terraria.Main.worldName);
			}
		}

		[TestCase(true, false)]
		[TestCase(false, true)]
		public void WorldSaving_ModifiesResetTime(bool resetTime, bool newResetTime)
		{
			using (var orion = new Orion())
			using (var worldService = new WorldService(orion))
			{
				Terraria.Main.time = 0.0;
				Terraria.IO.WorldFile.tempTime = 0.0;
				Terraria.WorldGen.saveLock = false;
				worldService.WorldSaving += (sender, args) => args.ResetTime = newResetTime;

				Terraria.IO.WorldFile.saveWorld(false, resetTime);

				Assert.AreEqual(newResetTime, Terraria.IO.WorldFile.tempTime == 13500.0);
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
				worldService.WorldSaved += (sender, args) =>
				{
					eventOccurred = true;
					Assert.AreEqual(resetTime, args.ResetTime);
				};

				Terraria.IO.WorldFile.saveWorld(false, resetTime);

				Assert.IsTrue(eventOccurred, "WorldSaved event should have occurred.");
			}
		}
	}
}
