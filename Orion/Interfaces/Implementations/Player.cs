using System;
using System.Runtime.CompilerServices;

namespace Orion.Interfaces.Implementations
{
	/// <summary>
	/// Wraps a <see cref="Terraria.Player"/>.
	/// </summary>
	public class Player : Entity, IPlayer
	{
		private static readonly ConditionalWeakTable<Terraria.Player, Player> Cache
			= new ConditionalWeakTable<Terraria.Player, Player>(); 

		/// <summary>
		/// Gets the defense.
		/// </summary>
		public int Defense => WrappedPlayer.statDefense;

		/// <summary>
		/// Gets or sets the HP.
		/// </summary>
		public int HP
		{
			get { return WrappedPlayer.statLife; }
			set { WrappedPlayer.statLife = value; }
		}

		/// <summary>
		/// Gets the inventory <see cref="IItemArray"/>.
		/// </summary>
		public IItemArray Inventory => ItemArray.Wrap(WrappedPlayer.inventory);

		/// <summary>
		/// Gets or sets the maximum HP.
		/// </summary>
		public int MaxHP
		{
			get { return WrappedPlayer.statLifeMax; }
			set { WrappedPlayer.statLifeMax = value; }
		}

		/// <summary>
		/// Gets or sets the maximum MP.
		/// </summary>
		public int MaxMP
		{
			get { return WrappedPlayer.statManaMax; }
			set { WrappedPlayer.statManaMax = value; }
		}

		/// <summary>
		/// Gets or sets the MP.
		/// </summary>
		public int MP
		{
			get { return WrappedPlayer.statMana; }
			set { WrappedPlayer.statMana = value; }
		}

		/// <summary>
		/// Gets the selected <see cref="IItem"/>.
		/// </summary>
		public IItem SelectedItem => Inventory[WrappedPlayer.selectedItem];

		/// <summary>
		/// Gets the wrapped <see cref="Terraria.Entity"/>.
		/// </summary>
		public override Terraria.Entity WrappedEntity => WrappedPlayer;

		/// <summary>
		/// Gets the wrapped <see cref="Terraria.Player"/>.
		/// </summary>
		public Terraria.Player WrappedPlayer { get; }
		
		private Player(Terraria.Player terrariaPlayer)
		{
			WrappedPlayer = terrariaPlayer;
		}

		/// <summary>
		/// Creates a new instance of the <see cref="Player"/> class wrapping the specified
		/// <see cref="Terraria.Player"/>. If this method is called multiple times on the same
		/// <see cref="Terraria.Player"/>, then the same <see cref="Player"/> will be returned.
		/// </summary>
		/// <param name="terrariaPlayer">The <see cref="Terraria.Player"/>.</param>
		/// <returns>A <see cref="Player"/> wrapping <paramref name="terrariaPlayer"/>.</returns>
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
