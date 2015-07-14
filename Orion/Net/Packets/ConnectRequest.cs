using System.IO;

namespace Orion.Net.Packets
{
	public class ConnectRequest : TerrariaPacket
	{
		public string Version { get; private set; }

		internal ConnectRequest(BinaryReader reader) 
			: base(reader)
		{
			Version = reader.ReadString();
		}
	}
}
