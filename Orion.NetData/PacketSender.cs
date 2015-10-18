using Orion.Framework.Events;
using Orion.NetData.Packets;

namespace Orion.NetData
{
	public class PacketSender
	{
		/// <summary>
		/// Fired when a Disconnect packet is sent
		/// </summary>
		public event OrionEventHandler<PacketEventArgs<Disconnect>> Disconnect;
		/// <summary>
		/// Fired when a ContinueConnecting packet is sent
		/// </summary>
		public event OrionEventHandler<PacketEventArgs<ContinueConnecting>> ContinueConnecting;
		/// <summary>
		/// Fired when a PlayerInfo packet is sent
		/// </summary>
		public event OrionEventHandler<PacketEventArgs<PlayerInfo>> PlayerInfo;
		/// <summary>
		/// Fired when a PlayerSlot packet is sent
		/// </summary>
		public event OrionEventHandler<PacketEventArgs<InventorySlot>> InventorySlot;
		/// <summary>
		/// Fired when a WorldInfo packet is sent
		/// </summary>
		public event OrionEventHandler<PacketEventArgs<WorldInfo>> WorldInfo;


		public void SendPacket(Orion orion, NetSendDataEventArgs e)
		{
			PacketTypes packetType = (PacketTypes)e.MsgType;
			TerrariaPacketBase packet = null;

			switch (packetType)
			{
				case PacketTypes.Disconnect:
					if (Disconnect != null)
					{
						packet = new Disconnect(e.Text);
                        Disconnect(orion, new PacketEventArgs<Disconnect>((Disconnect)packet));
					}
					break;

				case PacketTypes.ContinueConnecting:
					if (ContinueConnecting != null)
					{
						packet = new ContinueConnecting(e.RemoteClient);
						ContinueConnecting(orion, new PacketEventArgs<ContinueConnecting>((ContinueConnecting)packet));
					}
					break;

				case PacketTypes.PlayerInfo:
					if (PlayerInfo != null)
					{
						packet = new PlayerInfo(e.Number, e.Text);
						PlayerInfo(orion, new PacketEventArgs<PlayerInfo>((PlayerInfo)packet));
					}
					break;

				case PacketTypes.PlayerSlot:
					if (InventorySlot != null)
					{
						packet = new InventorySlot(e.Number, (int)e.Number2);
						InventorySlot(orion, new PacketEventArgs<InventorySlot>((InventorySlot)packet));
					}
					break;

				case PacketTypes.WorldInfo:
					if (WorldInfo != null)
					{
						packet = new WorldInfo();
						WorldInfo(orion, new PacketEventArgs<WorldInfo>((WorldInfo)packet));
					}
					break;
			}

			if (packet != null)
			{
				packet.SetNewData(ref e);
				e.Cancelled = packet.Handled;
			}
		}
	}
}