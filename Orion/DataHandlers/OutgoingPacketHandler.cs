using System.IO;

namespace Orion.DataHandlers
{
	public class OutgoingPacketHandler : IPacketHandler
	{
		private Orion _core;

		public OutgoingPacketHandler(Orion core)
		{
			_core = core;
		}

		public void GetData(PacketTypes type, MemoryStream data)
		{
			
		}
	}
}
