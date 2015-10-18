using Orion.Framework.Events;
using Orion.NetData.Packets;
using System.Collections.Generic;

namespace Orion.NetData
{
	public class PacketReceiver
	{
		private List<PacketTypes> DeserializerList;

		/// <summary>
		/// Fired when a ConnectRequest packet is received
		/// </summary>
		public event OrionEventHandler<PacketEventArgs<ConnectRequest>> ConnectRequest;
		/// <summary>
		/// Fired when a PlayerInfo packet is received
		/// </summary>
		public event OrionEventHandler<PacketEventArgs<PlayerInfo>> PlayerInfo;
		/// <summary>
		/// Fired when a PlayerSlot packet is received
		/// </summary>
		public event OrionEventHandler<PacketEventArgs<InventorySlot>> InventorySlot;
		/// <summary>
		/// Fired when a ContinueConnecting2 packet is received
		/// </summary>
		public event OrionEventHandler<PacketEventArgs<ContinueConnecting2>> ContinueConnecting2;
		/// <summary>
		/// Fired when a TileGetSection packet is received
		/// </summary>
		public event OrionEventHandler<PacketEventArgs<TileGetSection>> TileGetSection;
		/// <summary>
		/// TODO: Description
		/// </summary>
		public event OrionEventHandler<PacketEventArgs<SpawnPlayer>> SpawnPlayer;
		/// <summary>
		/// TODO: Description
		/// </summary>
		public event OrionEventHandler<PacketEventArgs<PlayerUpdate>> PlayerUpdate;

		internal PacketReceiver()
		{
			DeserializerList = new List<PacketTypes>
			{
				{PacketTypes.ConnectRequest},
				{PacketTypes.PlayerInfo},
				{PacketTypes.PlayerSlot},
				{PacketTypes.ContinueConnecting2},
				{PacketTypes.TileGetSection},
				{PacketTypes.PlayerSpawn}
			};
		}

		public void ReceivePacket(Orion orion, NetGetDataEventArgs e)
		{
			PacketTypes packetType = (PacketTypes)e.MessageID;
			if (!DeserializerList.Contains(packetType))
			{
				return;
			}

			TerrariaPacketBase packet = null;
			
			switch (packetType)
			{
				case PacketTypes.ConnectRequest:
					if (ConnectRequest != null)
					{
						packet = new ConnectRequest(e.CreateBinaryReader());
						ConnectRequest(orion, new PacketEventArgs<ConnectRequest>((ConnectRequest)packet));
					}
					break;

				case PacketTypes.PlayerInfo:
					if (PlayerInfo != null)
					{
						packet = new PlayerInfo(e.CreateBinaryReader());
						PlayerInfo(orion,
							new PacketEventArgs<PlayerInfo>((PlayerInfo)packet));
					}
					break;

				case PacketTypes.PlayerSlot:
					if (InventorySlot != null)
					{
						packet = new InventorySlot(e.CreateBinaryReader());
						InventorySlot(orion,
							new PacketEventArgs<InventorySlot>((InventorySlot)packet));
					}
					break;

				case PacketTypes.ContinueConnecting2:
					if (ContinueConnecting2 != null)
					{
						packet = new ContinueConnecting2(e.CreateBinaryReader());
						ContinueConnecting2(orion,
							new PacketEventArgs<ContinueConnecting2>((ContinueConnecting2)packet));
					}
					break;

				case PacketTypes.TileGetSection:
					if (TileGetSection != null)
					{
						packet = new TileGetSection(e.CreateBinaryReader());
						TileGetSection(orion,
							new PacketEventArgs<TileGetSection>((TileGetSection)packet));
					}
					break;

				case PacketTypes.PlayerSpawn:
					if (SpawnPlayer != null)
					{
						packet = new SpawnPlayer(e.CreateBinaryReader());
						SpawnPlayer(orion, new PacketEventArgs<SpawnPlayer>((SpawnPlayer)packet));
					}
					break;

				case PacketTypes.PlayerUpdate:
					if (PlayerUpdate != null)
					{
						packet = new PlayerUpdate(e.CreateBinaryReader());
						PlayerUpdate(orion, new PacketEventArgs<PlayerUpdate>((PlayerUpdate)packet));
					}
					break;
			}

			if (packet != null)
			{
				e.Cancelled = packet.Handled;
			}
		}
	}
}