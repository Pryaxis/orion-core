using Orion.Framework.Events;
using System.IO;

namespace Orion.NetData
{
	public class TerrariaPacketBase
	{
		/// <summary>
		/// Packet length.
		/// </summary>
		private short _length;

		/// <summary>
		/// Packet ID.
		/// </summary>
		public byte ID;

		/// <summary>
		/// Whether or not this packet has been handled.
		/// </summary>
		public bool Handled;

		/// <summary>
		/// Obtains packet data from a BinaryReader.
		/// Used when receiving packets.
		/// </summary>
		/// <param name="reader">The reader object with the data to be read.</param>
		internal TerrariaPacketBase(BinaryReader reader)
		{
			_length = reader.ReadInt16();
			ID = reader.ReadByte();
		}

		/// <summary>
		/// Creates a packet based on it's ID.
		/// Used when sending packets.
		/// </summary>
		/// <param name="id">The packet id.</param>
		internal TerrariaPacketBase(byte id)
		{
			ID = id;
		}

		/// <summary>
		/// Creates a packet based on it's ID.
		/// Used when sending packets.
		/// </summary>
		/// <param name="id">The packet id.</param>
		internal TerrariaPacketBase(PacketTypes id)
			:this((byte)id)
		{

		}

		internal virtual void SetNewData(ref NetSendDataEventArgs e) { }
	}
}
