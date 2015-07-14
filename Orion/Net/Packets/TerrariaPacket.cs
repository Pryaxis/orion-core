using System.IO;

namespace Orion.Net.Packets
{
	public class TerrariaPacket
	{
		private short _length;
		public byte id;
		public bool handled;

		internal TerrariaPacket(BinaryReader reader)
		{
			_length = reader.ReadInt16();
			id = reader.ReadByte();
		}

		internal TerrariaPacket(byte id)
		{
			this.id = id;
		}

		internal TerrariaPacket(PacketTypes id)
			:this((byte)id)
		{
			
		}
	}
}
