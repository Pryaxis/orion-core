using System.IO;

namespace Orion.Net.Packets
{
	public class ContinueConnecting : TerrariaPacket
	{
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
