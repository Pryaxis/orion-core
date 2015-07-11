using System;
using Orion.Terraria;
using Terraria;

namespace Orion.Utilities
{
	public class Utils
	{
		/// <summary>
		/// DateTime object that represents the start date of the Unix time epoch
		/// </summary>
		public static readonly DateTime UnixDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		/// <summary>
		/// Orion instance
		/// </summary>
		private readonly Orion _core;

		/// <summary>
		/// Creates a new instance of the Utils class using the given Orion instance
		/// </summary>
		/// <param name="core"></param>
		public Utils(Orion core)
		{
			_core = core;
		}

		/// <summary>
		/// Converts a DateTime object to unix epoch format
		/// </summary>
		/// <param name="datetime"></param>
		/// <returns></returns>
		public long ConvertToUnixTime(DateTime datetime)
		{
			return (long) (datetime - UnixDateTime).TotalSeconds;
		}

		/// <summary>
		/// Converts an Int64 value into a DateTime value, assuming the Int64 value represents seconds.
		/// </summary>
		/// <param name="unixTime"></param>
		/// <returns></returns>
		public DateTime ConvertToDateTime(long unixTime)
		{
			return (UnixDateTime.AddSeconds(unixTime));
		}

		/// <summary>
		/// Clears items starting at the given Vector coordinate and moving outwards for the given radius
		/// </summary>
		/// <param name="start">Start point</param>
		/// <param name="radius">Clearance radius</param>
		/// <returns>Number of items cleared</returns>
		public int ClearItems(Vector2 start, float radius = 50f)
		{
			int cleared = 0;
			for (int i = 0; i < Main.maxItems; i++)
			{
				float dX = Main.item[i].position.X - start.X;
				float dY = Main.item[i].position.Y - start.Y;

				if (Main.item[i].active && dX * dX + dY * dY <= radius * radius * 256f)
				{
					Main.item[i].active = false;
					_core.NetUtils.SendPacketToEveryone(PacketTypes.ItemDrop, "", i);
					cleared++;
				}
			}
			return cleared;
		}

		/// <summary>
		/// Attempts to get a Buff by ID
		/// </summary>
		/// <param name="id">Buff ID</param>
		/// <returns>Buff object if an ID matches, null if not</returns>
		public Buff GetBuff(int id)
		{
			if (id < Main.buffName.Length && id >= 0)
			{
				return new Buff(Main.buffName[id], id, Main.buffTip[id]);
			}

			return null;
		}

		/// <summary>
		/// Attempts to get a Buff by name
		/// </summary>
		/// <param name="name">Buff name</param>
		/// <returns>Buff object if a name matches, null if not</returns>
		public Buff GetBuff(string name)
		{
			for (int i = 0; i < Main.buffName.Length; i++)
			{
				if (Main.buffName[i] == name)
				{
					return new Buff(Main.buffName[i], i, Main.buffTip[i]);
				}
			}

			return null;
		}
	}
}