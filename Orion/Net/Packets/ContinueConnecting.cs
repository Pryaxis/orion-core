using System.IO;

namespace Orion.Net.Packets
{
	public class ContinueConnecting : TerrariaPacket
	{
		/// <summary>
		/// Used when the packet is read
		/// </summary>
		/// <param name="reader"></param>
		internal ContinueConnecting(BinaryReader reader)
			: base(reader)
		{
		}

		internal ContinueConnecting(byte id) 
			: base(id)
		{
		}

		internal ContinueConnecting(PacketTypes id) 
			: base(id)
		{
		}
	}
}
