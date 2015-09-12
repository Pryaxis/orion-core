namespace Orion.Net
{
	public class NetItem
	{
		public short ID { get; set; }
		public byte Prefix { get; set; }
		public short Stack { get; set; }
		public bool Favorite { get; set; }

		public NetItem(short id, byte prefix, short stack, bool favorite)
		{
			ID = id;
			Prefix = prefix;
			Stack = stack;
			Favorite = favorite;
		}

		public NetItem(Terraria.Item item)
		{
			ID = (short)item.netID;
			Prefix = item.prefix;
			Stack = (short)item.stack;
			Favorite = item.favorited;
		}

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
