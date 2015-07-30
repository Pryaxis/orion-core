using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Orion.Net.Packets
{
	/// <summary>
	/// Tile Get Section / Request Sync [8] packet. Sent from the client to the server.
	/// </summary>
	public class TileGetSection : TerrariaPacket
	{
		/// <summary>
		/// The tile X coordinate. If -1, sends spawn area tile sections.
		/// </summary>
		public int X { get; private set; }

		/// <summary>
		/// The tile Y coordinate. If -1, sends spawn area tile sections.
		/// </summary>
		public int Y { get; private set; }

		/// <summary>
		/// Creates a new Tile Get Section packet by reading data from <paramref name="reader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="BinaryReader"/> object with the data to be read.</param>
		internal TileGetSection(BinaryReader reader)
			: base(reader)
		{
			X = reader.ReadInt32();
			Y = reader.ReadInt32();
		}
	}
}
