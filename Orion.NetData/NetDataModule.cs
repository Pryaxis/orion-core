using Orion.Framework;

namespace Orion.NetData
{
	[OrionModule("Network Data Provider", "Nyx Studios", Description = "Allows for other modules to read/write to sent and received packets.")]
	public class NetDataModule : OrionModuleBase
    {
		public readonly PacketReceiver IncomingPackets;
		public readonly PacketSender OutgoingPackets;

		public NetDataModule(Orion core) : base(core)
		{
			IncomingPackets = new PacketReceiver();
			OutgoingPackets = new PacketSender();
			Core.Hooks.NetGetData += IncomingPackets.ReceivePacket;
			Core.Hooks.NetSendData += OutgoingPackets.SendPacket;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				Core.Hooks.NetGetData -= IncomingPackets.ReceivePacket;
				Core.Hooks.NetSendData -= OutgoingPackets.SendPacket;
			}
			base.Dispose(disposing);
		}
	}
}