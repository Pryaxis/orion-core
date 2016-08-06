using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using Orion.Players;

namespace Orion.Tests.Players
{
	[TestFixture]
	public class PlayerServiceTests
	{
		private static readonly Predicate<IPlayer>[] FindTestCases = {player => player.Position.X < 100};
		
		[TestCase(0)]
		[TestCase(1)]
		public void FindPlayers_Null_ReturnsAll(int populate)
		{
			using (var orion = new Orion())
			{
				var playerService = orion.GetService<PlayerService>();
				for (var i = 0; i < Terraria.Main.player.Length; ++i)
				{
					Terraria.Main.player[i] = new Terraria.Player {active = i < populate};
				}

				List<IPlayer> players = playerService.FindPlayers().ToList();

				Assert.AreEqual(populate, players.Count);
				for (var i = 0; i < populate; ++i)
				{
					Assert.AreSame(Terraria.Main.player[i], players[i].WrappedPlayer);
				}
			}
		}

		[TestCase(1)]
		public void FindPlayers_MultipleTimes_ReturnsSameInstance(int populate)
		{
			using (var orion = new Orion())
			{
				var playerService = orion.GetService<PlayerService>();
				for (var i = 0; i < Terraria.Main.player.Length; ++i)
				{
					Terraria.Main.player[i] = new Terraria.Player { active = i < populate };
				}

				List<IPlayer> players = playerService.FindPlayers().ToList();
				List<IPlayer> players2 = playerService.FindPlayers().ToList();

				for (var i = 0; i < populate; ++i)
				{
					Assert.AreSame(players[i], players2[i]);
				}
			}
		}

		[Test, TestCaseSource(nameof(FindTestCases))]
		public void FindPlayers_IsCorrect(Predicate<IPlayer> predicate)
		{
			using (var orion = new Orion())
			{
				var playerService = orion.GetService<PlayerService>();

				for (var i = 0; i < Terraria.Main.player.Length; ++i)
				{
					Terraria.Main.player[i] = new Terraria.Player {active = true, position = new Vector2(i, 0)};
				}

				List<IPlayer> players = playerService.FindPlayers(predicate).ToList();
				
				foreach (IPlayer player in players)
				{
					Assert.IsTrue(predicate(player));
				}
			}
		}
	}
}
