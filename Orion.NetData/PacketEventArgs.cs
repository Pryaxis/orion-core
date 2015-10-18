using Orion.Framework.Events;

namespace Orion.NetData
{
	public class PacketEventArgs<TerrariaPacketBase> : OrionEventArgs
	{
		public TerrariaPacketBase Packet;

		public PacketEventArgs(TerrariaPacketBase packet)
		{
			Packet = packet;
		}
	}
}