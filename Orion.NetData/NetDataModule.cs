using Orion.Framework;

namespace Orion.NetData
{
	[OrionModule("Network Data Provider", "Nyx Studios", Description = "Allows for other modules to read/write to sent and received packets.")]
	[DependsOn(typeof(Modules.Hooks.OTAPIHookModule))]
	public class NetDataModule : OrionModuleBase
    {
		public PacketReceiver IncomingPackets => new PacketReceiver();
		public PacketSender OutgoingPackets => new PacketSender();

		public NetDataModule(Orion core) : base(core)
		{
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