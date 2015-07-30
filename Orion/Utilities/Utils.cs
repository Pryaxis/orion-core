using System;
using System.Collections.Generic;
using System.Text;
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
		/// Clears NPCs starting at the given Vector coordinate and moving outwards for the given radius
		/// </summary>
		/// <param name="start">Start point</param>
		/// <param name="clearFriendly">Clear friendly NPCs</param>
		/// <param name="radius">Clearance radius</param>
		/// <returns>Number of NPCs cleared</returns>
		public int ClearNpcs(Vector2 start, bool clearFriendly = false, float radius = 50f)
		{
			int cleared = 0;
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				if (Main.npc[i].friendly && !clearFriendly)
				{
					continue;
				}

				float dX = Main.npc[i].position.X - start.X;
				float dY = Main.npc[i].position.Y - start.Y;

				if (Main.npc[i].active && dX * dX + dY * dY <= radius * radius * 256f)
				{
					Main.npc[i].active = false;
					Main.npc[i].type = 0;
					_core.NetUtils.SendPacketToEveryone(PacketTypes.NpcUpdate, "", i);
					cleared++;
				}
			}
			return cleared;
		}

		/// <summary>
		/// Clears projectiles starting at the given Vector coordinate and moving outwards for the given radius
		/// </summary>
		/// <param name="start">Start point</param>
		/// <param name="radius">Clearance radius</param>
		/// <returns>Number of projectiles cleared</returns>
		public int ClearProjectiles(Vector2 start, float radius = 50f)
		{
			int cleared = 0;
			for (int i = 0; i < Main.maxProjectiles; i++)
			{
				float dX = Main.projectile[i].position.X - start.X;
				float dY = Main.projectile[i].position.Y - start.Y;

				if (Main.projectile[i].active && dX * dX + dY * dY <= radius * radius * 256f)
				{
					Main.projectile[i].active = false;
					Main.projectile[i].type = 0;
					_core.NetUtils.SendPacketToEveryone(PacketTypes.ProjectileNew, "", i);
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

			ColorStrings(new ColoredString("Test", Color.White));

			return null;
		}

		/// <summary>
		/// Colors a string using Terraria's color tag
		/// </summary>
		/// <param name="str">String to color</param>
		/// <param name="color">Color to color string with</param>
		/// <returns></returns>
		public string ColorString(string str, Color color)
		{
			return $"[c/{color.Hex3()}:{str}]";
		}

		/// <summary>
		/// Colors a string using Terraria's color tag
		/// </summary>
		/// <param name="str">ColoredString object to use</param>
		/// <returns></returns>
		public string ColorString(ColoredString str)
		{
			return str.ToString();
		}

		/// <summary>
		/// Colors multiple strings using Terraria's color tag
		/// </summary>
		/// <param name="strings">ColoredString objects to use</param>
		/// <returns></returns>
		public string ColorStrings(params ColoredString[] strings)
		{
			StringBuilder sb = new StringBuilder();

			foreach (ColoredString str in strings)
			{
				if (str.Color == null)
				{
					sb.Append(str.String);
				}
				else
				{
					sb.Append(str.ToString());
				}
			}

			return sb.ToString();
		}

		/// <summary>
		/// Colors multiple strings using Terraria's color tag
		/// </summary>
		/// <param name="strings"></param>
		/// <returns></returns>
		public string ColorStrings(Dictionary<string, Color> strings)
		{
			StringBuilder sb = new StringBuilder();

			foreach (KeyValuePair<string, Color> pair in strings)
			{
				if (pair.Value == null)
				{
					sb.Append(pair.Key);
				}
				else
				{
					sb.Append(ColorString(pair.Key, pair.Value));
				}
			}

			return sb.ToString();
		}
	}
}