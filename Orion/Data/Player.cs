using Orion.Interfaces;
using System;
using System.Runtime.CompilerServices;

namespace Orion.Data
{
	/// <summary>
	/// Wraps a Terraria player.
	/// </summary>
	public class Player : Entity, IPlayer
	{
		private static readonly ConditionalWeakTable<Terraria.Player, Player> Cache
			= new ConditionalWeakTable<Terraria.Player, Player>(); 

		/// <summary>
		/// Gets the player's defense.
		/// </summary>
		public int Defense => WrappedPlayer.statDefense;

		/// <summary>
		/// Gets or sets the player's HP.
		/// </summary>
		public int HP
		{
			get { return WrappedPlayer.statLife; }
			set { WrappedPlayer.statLife = value; }
		}

		/// <summary>
		/// Gets the player's inventory <see cref="IItemArray"/>.
		/// </summary>
		public IItemArray Inventory => ItemArray.Wrap(WrappedPlayer.inventory);

		/// <summary>
		/// Gets or sets the player's maximum HP.
		/// </summary>
		public int MaxHP
		{
			get { return WrappedPlayer.statLifeMax; }
			set { WrappedPlayer.statLifeMax = value; }
		}

		/// <summary>
		/// Gets or sets the player's maximum MP.
		/// </summary>
		public int MaxMP
		{
			get { return WrappedPlayer.statManaMax; }
			set { WrappedPlayer.statManaMax = value; }
		}

		/// <summary>
		/// Gets or sets the player's MP.
		/// </summary>
		public int MP
		{
			get { return WrappedPlayer.statMana; }
			set { WrappedPlayer.statMana = value; }
		}

		/// <summary>
		/// Gets the player's selected <see cref="IItem"/>.
		/// </summary>
		public IItem SelectedItem => Inventory[WrappedPlayer.selectedItem];

		/// <summary>
		/// Gets the wrapped Terraria entity.
		/// </summary>
		public override Terraria.Entity WrappedEntity => WrappedPlayer;

		/// <summary>
		/// Gets the wrapped Terraria player.
		/// </summary>
		public Terraria.Player WrappedPlayer { get; }
		
		private Player(Terraria.Player terrariaPlayer)
		{
			WrappedPlayer = terrariaPlayer;
		}

		/// <summary>
		/// Creates a new instance of the <see cref="Player"/> class wrapping the specified Terraria player. If this
		/// method is called multiple times on the same Terraria player, then the same <see cref="Player"/> will be
		/// returned.
		/// </summary>
		/// <param name="terrariaPlayer">The Terraria player to wrap.</param>
		/// <returns>An <see cref="Player"/> that wraps <paramref name="terrariaPlayer"/>.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="terrariaPlayer"/> was null.</exception>
		public static Player Wrap(Terraria.Player terrariaPlayer)
		{
			if (terrariaPlayer == null)
			{
				throw new ArgumentNullException(nameof(terrariaPlayer));
			}

			Player player;
			if (!Cache.TryGetValue(terrariaPlayer, out player))
			{
				player = new Player(terrariaPlayer);
				Cache.Add(terrariaPlayer, player);
			}
			return player;
		}
	}
}
