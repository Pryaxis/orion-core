using Terraria;
using Orion.UserAccounts;

namespace Orion
{
	public class OrionPlayer
	{
		/// <summary>
		/// <see cref="Terraria.Player"></see> object that this <see cref="OrionPlayer"></see> represents
		/// </summary>
		public Player Player { get; private set; }

		/// <summary>
		/// <see cref="Terraria.Player"></see> index
		/// </summary>
		public int Index { get { return Player.whoAmI; } }

		/// <summary>
		/// <see cref="UserAccount"></see> assosciated with this <see cref="OrionPlayer"></see>
		/// </summary>
		public UserAccount User { get; set; }

		public OrionPlayer(Player player)
		{
			Player = player;
		}

		/// <summary>
		/// Provided for abstraction with <see cref="SSC.SSCharacter"></see>. Does nothing in this base class
		/// </summary>
		/// <param name="item"></param>
		public virtual void EditItemSlot(int index, Item item)
		{

		}

		/// <summary>
		/// Provided for abstraction with <see cref="SSC.SSCharacter"></see>. Does nothing in this base class
		/// </summary>
		/// <param name="item"></param>
		public virtual void EditItemSlot(int index, Net.NetItem item)
		{

		}

		/// <summary>
		/// Provided for abstraction with <see cref="SSC.SSCharacter"></see>. Does nothing in this base class
		/// </summary>
		public virtual void SaveInventory()
		{

		}
	}
}