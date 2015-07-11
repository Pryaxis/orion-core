using System.IO;

namespace Orion.DataHandlers
{
	public interface IPacketHandler
	{
		void GetData(PacketTypes type, MemoryStream data);
	}
}
