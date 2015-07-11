using System.IO;

namespace Orion.DataHandlers
{
	public class IncomingPacketHandler : IPacketHandler
	{
		private Orion _core;

		public IncomingPacketHandler(Orion core)
		{
			_core = core;
		}

		public void GetData(PacketTypes type, MemoryStream data)
		{

		}
	}
}
