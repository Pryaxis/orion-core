using System;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using Orion.Interfaces;
using Orion.Services;
using System.Collections.Generic;
using System.Linq;

namespace Orion.Tests.Services
{
	[TestFixture]
	public class PlayerServiceTests
	{
		private static readonly Predicate<IPlayer>[] Predicates = {player => player.Position.X < 100};

		[TestCase(0)]
		[TestCase(1)]
		[TestCase(100)]
		public void Find_Null_ReturnsAll(int populate)
		{
			using (var orion = new Orion())
			using (var playerService = new PlayerService(orion))
			{
				for (int i = 0; i < Terraria.Main.player.Length; ++i)
				{
					Terraria.Main.player[i] = new Terraria.Player {active = i < populate, name = "A"};
				}
				List<IPlayer> players = playerService.Find().ToList();

				Assert.AreEqual(populate, players.Count);
				foreach (IPlayer player in players)
				{
					Assert.AreEqual("A", player.Name);
				}
			}
		}

		[Test, TestCaseSource(nameof(Predicates))]
		public void Find_IsCorrect(Predicate<IPlayer> predicate)
		{
			using (var orion = new Orion())
			using (var playerService = new PlayerService(orion))
			{
				for (int i = 0; i < Terraria.Main.player.Length; ++i)
				{
					Terraria.Main.player[i] = new Terraria.Player {active = true, position = new Vector2(i, 0)};
				}
				List<IPlayer> players = playerService.Find(predicate).ToList();
				IEnumerable<IPlayer> otherPlayers = playerService.Find(p => !players.Contains(p));

				foreach (IPlayer player in players)
				{
					Assert.IsTrue(predicate(player));
				}
				foreach (IPlayer player in otherPlayers)
				{
					Assert.IsFalse(predicate(player));
				}
			}
		}
	}
}
