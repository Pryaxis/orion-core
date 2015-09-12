namespace Orion.Net
{
	public class NetItem
	{
		/// <summary>
		/// Item ID
		/// </summary>
		public short ID { get; set; }
		/// <summary>
		/// Item Prefix
		/// </summary>
		public byte Prefix { get; set; }
		/// <summary>
		/// Item Stack
		/// </summary>
		public short Stack { get; set; }
		/// <summary>
		/// Item Favorite status
		/// </summary>
		public bool Favorite { get; set; }

		/// <summary>
		/// Constructs a new <see cref="NetItem"></see> from item attributes
		/// </summary>
		/// <param name="id">A valid <see cref="Terraria.ID.ItemID"></see></param>
		/// <param name="prefix">Item prefix</param>
		/// <param name="stack">Item stack</param>
		/// <param name="favorite">Item favorite status</param>
		public NetItem(short id, byte prefix, short stack, bool favorite)
		{
			if (id < -24 || id >= Terraria.ID.ItemID.Count)
			{
				id = 0;
			}
			ID = id;

			if (prefix < 0 || prefix > Terraria.Item.maxPrefixes)
			{
				prefix = 0;
			}
			Prefix = prefix;

			if (stack < 0 || stack > 999)
			{
				stack = 0;
			}
			Stack = stack;

			Favorite = favorite;
		}

		/// <summary>
		/// Constructs a new <see cref="NetItem"></see> from a <see cref="Terraria.Item"></see>
		/// </summary>
		/// <param name="item"><see cref="Terraria.Item"></see> object</param>
		public NetItem(Terraria.Item item)
		{
			ID = (short)item.netID;
			Prefix = item.prefix;
			Stack = (short)item.stack;
			Favorite = item.favorited;
		}

		/// <summary>
		/// Constructs a new <see cref="NetItem"></see> from a <see cref="Terraria.Item"></see>
		/// </summary>
		/// <param name="item"></param>
		public static implicit operator NetItem(Terraria.Item item)
		{
			if (item == null)
			{
				return new NetItem(0, 0, 0, false);
			}
			return new NetItem((short)item.netID, item.prefix, (short)item.stack, item.favorited);
		}

		public static implicit operator Terraria.Item(NetItem item)
		{
			Terraria.Item i = new Terraria.Item();
			i.netDefaults(item.ID);
			i.stack = item.Stack;
			i.prefix = item.Prefix;

			return i;
		}
	}
}
