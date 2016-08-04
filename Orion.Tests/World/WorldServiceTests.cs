using System.Reflection;
using NUnit.Framework;
using Orion.World;

namespace Orion.Tests.World
{
	[TestFixture]
	public class WorldServiceTests
	{
		// NOTE: cannot test WorldService.IsExpertMode since it requires an active world file

		private static readonly object[] GetPropertyTestCases =
		{
			new object[] {nameof(WorldService.IsBloodMoon), nameof(Terraria.Main.bloodMoon), true},
			new object[] {nameof(WorldService.IsDaytime), nameof(Terraria.Main.dayTime), false},
			new object[] {nameof(WorldService.IsEclipse), nameof(Terraria.Main.eclipse), true},
			new object[] {nameof(WorldService.Time), nameof(Terraria.Main.time), 0.0}
		};

		private static readonly object[] SetPropertyTestCases =
		{
			new object[] {nameof(WorldService.IsBloodMoon), nameof(Terraria.Main.bloodMoon), true},
			new object[] {nameof(WorldService.IsDaytime), nameof(Terraria.Main.dayTime), false},
			new object[] {nameof(WorldService.IsEclipse), nameof(Terraria.Main.eclipse), true},
			new object[] {nameof(WorldService.Time), nameof(Terraria.Main.time), 0.0}
		};

		[TestCaseSource(nameof(GetPropertyTestCases))]
		public void GetProperty_IsCorrect(string worldServicePropertyName, string terrariaMainFieldName, object value)
		{
			using (var orion = new Orion())
			using (var worldService = new WorldService(orion))
			{
				FieldInfo terrariaMainField = typeof(Terraria.Main).GetField(terrariaMainFieldName);
				terrariaMainField.SetValue(null, value);
				PropertyInfo worldServiceProperty = typeof(WorldService).GetProperty(worldServicePropertyName);

				object actualValue = worldServiceProperty.GetValue(worldService);

				Assert.AreEqual(value, actualValue);
			}
		}

		[TestCaseSource(nameof(SetPropertyTestCases))]
		public void SetProperty_IsCorrect(string worldServicePropertyName, string terrariaMainFieldName, object value)
		{
			using (var orion = new Orion())
			using (var worldService = new WorldService(orion))
			{
				FieldInfo terrariaMainField = typeof(Terraria.Main).GetField(terrariaMainFieldName);
				PropertyInfo worldServiceProperty = typeof(WorldService).GetProperty(worldServicePropertyName);

				worldServiceProperty.SetValue(worldService, value);

				Assert.AreEqual(value, terrariaMainField.GetValue(null));
			}
		}

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
				var eventOccurred = false;
				worldService.MeteorDropping += (sender, args) =>
				{
					eventOccurred = true;
					Assert.AreEqual(x, args.X);
					Assert.AreEqual(y, args.Y);
				};

				Terraria.WorldGen.meteor(x, y);

				Assert.IsTrue(eventOccurred);
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

				var foundMeteor = false;
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
				Assert.IsFalse(foundMeteor);
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

				var foundMeteor = false;
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
				Assert.IsTrue(foundMeteor);
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
				var eventOccurred = false;
				worldService.WorldSaving += (sender, args) =>
				{
					eventOccurred = true;
					Assert.AreEqual(resetTime, args.ResetTime);
				};

				worldService.Save(resetTime);

				Assert.IsTrue(eventOccurred);
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

				worldService.Save(resetTime);

				Assert.AreNotEqual("World", Terraria.Main.worldName, "World should not have saved.");
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

				worldService.Save(resetTime);

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
				var eventOccurred = false;
				worldService.WorldSaved += (sender, args) =>
				{
					eventOccurred = true;
					Assert.AreEqual(resetTime, args.ResetTime);
				};

				worldService.Save(resetTime);

				Assert.IsTrue(eventOccurred);
			}
		}

		[TestCase(true)]
		[TestCase(false)]
		public void Save_IsCorrect(bool resetTime)
		{
			using (var orion = new Orion())
			using (var worldService = new WorldService(orion))
			{
				Terraria.Main.time = 0.0;
				Terraria.Main.worldName = "";
				Terraria.IO.WorldFile.tempTime = 0.0;
				Terraria.WorldGen.saveLock = false;

				worldService.Save(resetTime);

				Assert.AreEqual(resetTime, Terraria.IO.WorldFile.tempTime == 13500.0);
				Assert.AreEqual("World", Terraria.Main.worldName, "World should have saved.");
			}
		}

		[TestCase(100, 100)]
		public void DropMeteor_IsCorrect(int x, int y)
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

				worldService.DropMeteor(x, y);

				var foundMeteor = false;
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
				Assert.IsTrue(foundMeteor);
			}
		}

		[Test]
		public void SettleLiquids_IsCorrect()
		{
			using (var orion = new Orion())
			using (var worldService = new WorldService(orion))
			{
				Terraria.Liquid.panicMode = false;

				worldService.SettleLiquids();

				Assert.IsTrue(Terraria.Liquid.panicMode);
			}
		}
	}
}
