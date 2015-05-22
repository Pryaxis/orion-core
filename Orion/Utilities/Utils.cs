using System;
using Orion.Terraria;
using Terraria;

namespace Orion.Utilities
{
	public class Utils
	{
		public static readonly DateTime UnixDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		/// <summary>
		/// Converts a DateTime object to unix epoch
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

		public Buff GetBuff(int id)
		{
			if (id < Main.buffName.Length && id >= 0)
			{
				return new Buff(Main.buffName[id], id, Main.buffTip[id]);
			}

			return null;
		}

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