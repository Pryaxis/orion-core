using System;
using System.Reflection;
using NUnit.Framework;
using Orion.World;
using Orion.World.Events;

namespace Orion.Tests.World
{
	[TestFixture]
	public class WorldServiceTests
	{
		/*
		 * NOTE: HardmodeStarted, HardmodeStarting, IsExpertMode, WorldLoaded, and WorldLoading cannot be tested.
		 */

		private static readonly object[] GetPropertyTestCases =
		{
			new object[] {nameof(WorldService.Height), nameof(Terraria.Main.maxTilesY), 1000},
			new object[] {nameof(WorldService.IsBloodMoon), nameof(Terraria.Main.bloodMoon), true},
			new object[] {nameof(WorldService.IsChristmas), nameof(Terraria.Main.xMas), true},
			new object[] {nameof(WorldService.IsDaytime), nameof(Terraria.Main.dayTime), false},
			new object[] {nameof(WorldService.IsEclipse), nameof(Terraria.Main.eclipse), true},
			new object[] {nameof(WorldService.IsFrostMoon), nameof(Terraria.Main.snowMoon), true},
			new object[] {nameof(WorldService.IsHalloween), nameof(Terraria.Main.halloween), true},
			new object[] {nameof(WorldService.IsPumpkinMoon), nameof(Terraria.Main.pumpkinMoon), true},
			new object[] {nameof(WorldService.Time), nameof(Terraria.Main.time), 0.0},
			new object[] {nameof(WorldService.Width), nameof(Terraria.Main.maxTilesX), 1000},
			new object[] {nameof(WorldService.WorldName), nameof(Terraria.Main.worldName), "Name"}
		};

		private static readonly object[] SetPropertyTestCases =
		{
			new object[] {nameof(WorldService.Height), nameof(Terraria.Main.maxTilesY), 1000},
			new object[] {nameof(WorldService.IsBloodMoon), nameof(Terraria.Main.bloodMoon), true},
			new object[] {nameof(WorldService.IsChristmas), nameof(Terraria.Main.xMas), true},
			new object[] {nameof(WorldService.IsDaytime), nameof(Terraria.Main.dayTime), false},
			new object[] {nameof(WorldService.IsEclipse), nameof(Terraria.Main.eclipse), true},
			new object[] {nameof(WorldService.IsFrostMoon), nameof(Terraria.Main.snowMoon), true},
			new object[] {nameof(WorldService.IsHalloween), nameof(Terraria.Main.halloween), true},
			new object[] {nameof(WorldService.IsPumpkinMoon), nameof(Terraria.Main.pumpkinMoon), true},
			new object[] {nameof(WorldService.Time), nameof(Terraria.Main.time), 0.0},
			new object[] {nameof(WorldService.Width), nameof(Terraria.Main.maxTilesX), 1000},
			new object[] {nameof(WorldService.WorldName), nameof(Terraria.Main.worldName), "Name"}
		};

		[SetUp]
		public void SetUp()
		{
			// Required for meteor based tests. The legacy lang arrays are blank
			// and cause NullReferenceExceptions.
			Terraria.Lang.InitializeLegacyLocalization();
		}
		
		[TestCaseSource(nameof(GetPropertyTestCases))]
		public void GetProperty_IsCorrect(string worldServicePropertyName, string terrariaMainFieldName, object value)
		{
			using (var orion = new Orion())
			{
				var worldService = orion.GetService<WorldService>();
				FieldInfo terrariaMainField = typeof(Terraria.Main).GetField(terrariaMainFieldName);
				terrariaMainField.SetValue(null, Convert.ChangeType(value, terrariaMainField.FieldType));
				PropertyInfo worldServiceProperty = typeof(WorldService).GetProperty(worldServicePropertyName);

				object actualValue = worldServiceProperty.GetValue(worldService);

				Assert.AreEqual(value, actualValue);
			}
		}

		[TestCaseSource(nameof(SetPropertyTestCases))]
		public void SetProperty_IsCorrect(string worldServicePropertyName, string terrariaMainFieldName, object value)
		{
			using (var orion = new Orion())
			{
				var worldService = orion.GetService<WorldService>();
				FieldInfo terrariaMainField = typeof(Terraria.Main).GetField(terrariaMainFieldName);
				PropertyInfo worldServiceProperty = typeof(WorldService).GetProperty(worldServicePropertyName);

				worldServiceProperty.SetValue(worldService, value);

				Assert.AreEqual(
					Convert.ChangeType(value, terrariaMainField.FieldType), terrariaMainField.GetValue(null));
			}
		}

		[Test]
		public void CheckingChristmas_IsCorrect()
		{
			using (var orion = new Orion())
			{
				var worldService = orion.GetService<WorldService>();
				var eventOccurred = false;
				EventHandler<CheckingChristmasEventArgs> handler = (sender, e) => eventOccurred = true;
				worldService.CheckingChristmas += handler;

				Terraria.Main.checkXMas();
				worldService.CheckingChristmas -= handler;

				Assert.IsTrue(eventOccurred);
			}
		}

		[TestCase(true)]
		[TestCase(false)]
		public void CheckingChristmas_Handled_StopsCheck(bool christmas)
		{
			using (var orion = new Orion())
			{
				var worldService = orion.GetService<WorldService>();
				EventHandler<CheckingChristmasEventArgs> handler = (sender, e) => e.Handled = true;
				worldService.CheckingChristmas += handler;
				Terraria.Main.xMas = christmas;

				Terraria.Main.checkXMas();
				worldService.CheckingChristmas -= handler;

				Assert.AreEqual(christmas, Terraria.Main.xMas);
			}
		}

		[Test]
		public void CheckingHalloween_IsCorrect()
		{
			using (var orion = new Orion())
			{
				var worldService = orion.GetService<WorldService>();
				var eventOccurred = false;
				EventHandler<CheckingHalloweenEventArgs> handler = (sender, e) => eventOccurred = true;
				worldService.CheckingHalloween += handler;
				
				Terraria.Main.checkHalloween();
				worldService.CheckingHalloween -= handler;

				Assert.IsTrue(eventOccurred);
			}
		}
		
		[TestCase(true)]
		[TestCase(false)]
		public void CheckingHalloween_Handled_StopsCheck(bool halloween)
		{
			using (var orion = new Orion())
			{
				var worldService = orion.GetService<WorldService>();
				EventHandler<CheckingHalloweenEventArgs> handler = (sender, e) => e.Handled = true;
				worldService.CheckingHalloween += handler;
				Terraria.Main.halloween = halloween;

				Terraria.Main.checkHalloween();
				worldService.CheckingHalloween -= handler;

				Assert.AreEqual(halloween, Terraria.Main.halloween);
			}
		}

		[TestCase(100, 100)]
		public void MeteorDropping_IsCorrect(int x, int y)
		{
			using (var orion = new Orion())
			{
				var worldService = orion.GetService<WorldService>();
				InitializeMeteorRange(x, y);
				var eventOccurred = false;
				EventHandler<MeteorDroppingEventArgs> handler = (sender, e) =>
				{
					eventOccurred = true;
					Assert.AreEqual(x, e.X);
					Assert.AreEqual(y, e.Y);
				};
				worldService.MeteorDropping += handler;

				Terraria.WorldGen.meteor(x, y);
				worldService.MeteorDropping -= handler;

				Assert.IsTrue(eventOccurred);
			}
		}

		[TestCase(100, 100)]
		public void MeteorDropping_Handled_StopsMeteor(int x, int y)
		{
			using (var orion = new Orion())
			{
				var worldService = orion.GetService<WorldService>();
				InitializeMeteorRange(x, y);
				EventHandler<MeteorDroppingEventArgs> handler = (sender, e) => e.Handled = true;
				worldService.MeteorDropping += handler;

				Terraria.WorldGen.meteor(x, y);
				worldService.MeteorDropping -= handler;

				Assert.IsFalse(MeteorIsInRange(x, y));
			}
		}

		[TestCase(100, 100, 200, 200)]
		public void MeteorDropping_ModifiesXY(int x, int y, int newX, int newY)
		{
			using (var orion = new Orion())
			{
				var worldService = orion.GetService<WorldService>();
				InitializeMeteorRange(newX, newY);
				EventHandler<MeteorDroppingEventArgs> handler = (sender, e) =>
				{
					e.X = newX;
					e.Y = newY;
				};
				worldService.MeteorDropping += handler;

				Terraria.WorldGen.meteor(x, y);
				worldService.MeteorDropping -= handler;

				Assert.IsTrue(MeteorIsInRange(newX, newY));
			}
		}

		[TestCase(false)]
		[TestCase(true)]
		public void WorldSaving_IsCorrect(bool resetTime)
		{
			using (var orion = new Orion())
			{
				var worldService = orion.GetService<WorldService>();
				Terraria.Main.worldName = "";
				Terraria.WorldGen.saveLock = false;
				var eventOccurred = false;
				EventHandler<WorldSavingEventArgs> handler = (sender, e) =>
				{
					eventOccurred = true;
					Assert.AreEqual(resetTime, e.ResetTime);
				};
				worldService.WorldSaving += handler;

				worldService.Save(resetTime);
				worldService.WorldSaving -= handler;

				Assert.IsTrue(eventOccurred);
			}
		}

		[TestCase(false)]
		public void WorldSaving_Handled_StopsSaving(bool resetTime)
		{
			using (var orion = new Orion())
			{
				var worldService = orion.GetService<WorldService>();
				Terraria.Main.worldName = "";
				Terraria.WorldGen.saveLock = false;
				EventHandler<WorldSavingEventArgs> handler = (sender, e) => e.Handled = true;
				worldService.WorldSaving += handler;

				worldService.Save(resetTime);
				worldService.WorldSaving -= handler;

				Assert.AreNotEqual("World", Terraria.Main.worldName, "World should not have saved.");
			}
		}

		[TestCase(true, false)]
		[TestCase(false, true)]
		public void WorldSaving_ModifiesResetTime(bool resetTime, bool newResetTime)
		{
			using (var orion = new Orion())
			{
				var worldService = orion.GetService<WorldService>();
				Terraria.Main.time = 0.0;
				Terraria.IO.WorldFile.tempTime = 0.0;
				Terraria.WorldGen.saveLock = false;
				EventHandler<WorldSavingEventArgs> handler = (sender, e) => e.ResetTime = newResetTime;
				worldService.WorldSaving += handler;

				worldService.Save(resetTime);
				worldService.WorldSaving -= handler;

				Assert.AreEqual(newResetTime, Terraria.IO.WorldFile.tempTime == 13500.0);
			}
		}

		[TestCase(true)]
		[TestCase(false)]
		public void WorldSaved_IsCorrect(bool resetTime)
		{
			using (var orion = new Orion())
			{
				var worldService = orion.GetService<WorldService>();
				var eventOccurred = false;
				EventHandler<WorldSavedEventArgs> handler = (sender, e) =>
				{
					eventOccurred = true;
					Assert.AreEqual(resetTime, e.ResetTime);
				};
				worldService.WorldSaved += handler;

				worldService.Save(resetTime);
				worldService.WorldSaved -= handler;

				Assert.IsTrue(eventOccurred);
			}
		}

		[TestCase(true)]
		[TestCase(false)]
		public void Save_IsCorrect(bool resetTime)
		{
			using (var orion = new Orion())
			{
				var worldService = orion.GetService<WorldService>();
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
		public void BreakBlock_IsCorrect(int x, int y)
		{
			using (var orion = new Orion())
			{
				var worldService = orion.GetService<WorldService>();
				var tile = new Terraria.Tile();
				tile.active(true);
				Terraria.Main.tile[x, y] = tile;

				worldService.BreakBlock(x, y);

				Assert.IsFalse(tile.active());
			}
		}

		[TestCase(-1, 0)]
		[TestCase(100000, 0)]
		[TestCase(0, -1)]
		[TestCase(0, 100000)]
		public void BreakBlock_PositionOutOfRange_ThrowsArgumentOutOfRangeException(int x, int y)
		{
			using (var orion = new Orion())
			{
				var worldService = orion.GetService<WorldService>();
				Assert.Throws<ArgumentOutOfRangeException>(() => worldService.BreakBlock(x, y));
			}
		}

		[TestCase(100, 100)]
		public void BreakWall_IsCorrect(int x, int y)
		{
			using (var orion = new Orion())
			{
				var worldService = orion.GetService<WorldService>();
				var tile = new Terraria.Tile {wall = 1};
				Terraria.Main.tile[x, y] = tile;

				worldService.BreakWall(x, y);

				Assert.AreEqual(0, tile.wall);
			}
		}

		[TestCase(-1, 0)]
		[TestCase(100000, 0)]
		[TestCase(0, -1)]
		[TestCase(0, 100000)]
		public void BreakWall_PositionOutOfRange_ThrowsArgumentOutOfRangeException(int x, int y)
		{
			using (var orion = new Orion())
			{
				var worldService = orion.GetService<WorldService>();
				Assert.Throws<ArgumentOutOfRangeException>(() => worldService.BreakWall(x, y));
			}
		}

		[TestCase(100, 100)]
		public void DropMeteor_IsCorrect(int x, int y)
		{
			using (var orion = new Orion())
			{
				var worldService = orion.GetService<WorldService>();
				InitializeMeteorRange(x, y);

				worldService.DropMeteor(x, y);

				Assert.IsTrue(MeteorIsInRange(x, y));
			}
		}

		[TestCase(-1, 0)]
		[TestCase(100000, 0)]
		[TestCase(0, -1)]
		[TestCase(0, 100000)]
		public void DropMeteor_PositionOutOfRange_ThrowsArgumentOutOfRangeException(int x, int y)
		{
			using (var orion = new Orion())
			{
				var worldService = orion.GetService<WorldService>();
				Assert.Throws<ArgumentOutOfRangeException>(() => worldService.DropMeteor(x, y));
			}
		}

		[TestCase(100, 100, 1)]
		public void PaintBlock_IsCorrect(int x, int y, byte color)
		{
			using (var orion = new Orion())
			{
				var worldService = orion.GetService<WorldService>();
				var tile = new Terraria.Tile();
				tile.active(true);
				Terraria.Main.tile[x, y] = tile;

				worldService.PaintBlock(x, y, color);

				Assert.AreEqual(color, tile.color());
			}
		}

		[TestCase(-1, 0)]
		[TestCase(100000, 0)]
		[TestCase(0, -1)]
		[TestCase(0, 100000)]
		public void PaintBlock_PositionOutOfRange_ThrowsArgumentOutOfRangeException(int x, int y)
		{
			using (var orion = new Orion())
			{
				var worldService = orion.GetService<WorldService>();
				Assert.Throws<ArgumentOutOfRangeException>(() => worldService.PaintBlock(x, y, 0));
			}
		}

		[TestCase(100, 100, 1)]
		public void PaintWall_IsCorrect(int x, int y, byte color)
		{
			using (var orion = new Orion())
			{
				var worldService = orion.GetService<WorldService>();
				var tile = new Terraria.Tile {wall = 1};
				Terraria.Main.tile[x, y] = tile;

				worldService.PaintWall(x, y, color);

				Assert.AreEqual(color, tile.wallColor());
			}
		}

		[TestCase(-1, 0)]
		[TestCase(100000, 0)]
		[TestCase(0, -1)]
		[TestCase(0, 100000)]
		public void PaintWall_PositionOutOfRange_ThrowsArgumentOutOfRangeException(int x, int y)
		{
			using (var orion = new Orion())
			{
				var worldService = orion.GetService<WorldService>();
				Assert.Throws<ArgumentOutOfRangeException>(() => worldService.PaintWall(x, y, 0));
			}
		}

		[TestCase(100, 100, (ushort)1)]
		public void PlaceBlock_Simple_IsCorrect(int x, int y, ushort block)
		{
			using (var orion = new Orion())
			{
				var worldService = orion.GetService<WorldService>();
				var tile = new Terraria.Tile();
				Terraria.Main.tile[x, y] = tile;

				worldService.PlaceBlock(x, y, block);

				Assert.AreEqual(block, tile.type);
			}
		}

		[TestCase(-1, 0)]
		[TestCase(100000, 0)]
		[TestCase(0, -1)]
		[TestCase(0, 100000)]
		public void PlaceBlock_PositionOutOfRange_ThrowsArgumentOutOfRangeException(int x, int y)
		{
			using (var orion = new Orion())
			{
				var worldService = orion.GetService<WorldService>();
				Assert.Throws<ArgumentOutOfRangeException>(() => worldService.PlaceBlock(x, y, 0));
			}
		}

		[TestCase(100, 100, (byte)1)]
		public void PlaceWall_IsCorrect(int x, int y, byte wall)
		{
			using (var orion = new Orion())
			{
				var worldService = orion.GetService<WorldService>();
				var tile = new Terraria.Tile();
				Terraria.Main.tile[x, y] = tile;

				worldService.PlaceWall(x, y, wall);

				Assert.AreEqual(wall, tile.wall);
			}
		}

		[TestCase(-1, 0)]
		[TestCase(100000, 0)]
		[TestCase(0, -1)]
		[TestCase(0, 100000)]
		public void PlaceWall_PositionOutOfRange_ThrowsArgumentOutOfRangeException(int x, int y)
		{
			using (var orion = new Orion())
			{
				var worldService = orion.GetService<WorldService>();

				Assert.Throws<ArgumentOutOfRangeException>(() => worldService.PlaceWall(x, y, 0));
			}
		}

		[Test]
		public void SettleLiquids_IsCorrect()
		{
			using (var orion = new Orion())
			{
				var worldService = orion.GetService<WorldService>();
				Terraria.Liquid.panicMode = false;

				worldService.SettleLiquids();

				Assert.IsTrue(Terraria.Liquid.panicMode);
			}
		}

		private static void InitializeMeteorRange(int x, int y)
		{
			for (int i = x - 50; i < x + 50; ++i)
			{
				for (int j = y - 50; j < y + 50; ++j)
				{
					Terraria.Main.tile[i, j] = new Terraria.Tile();
				}
			}
		}

		private static bool MeteorIsInRange(int x, int y)
		{
			for (int i = x - 50; i < x + 50; ++i)
			{
				for (int j = y - 50; j < y + 50; ++j)
				{
					if (Terraria.Main.tile[i, j].type == Terraria.ID.TileID.Meteorite)
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
