using System;
using System.Collections.Generic;
using System.IO;
using Orion.Net.Packets;
using TerrariaApi.Server;

namespace Orion.Net
{
	public class PacketRepackager
	{
		private Orion _core;

		/// <summary>
		/// Contains a map that links packets and functions for deserializing packets
		/// </summary>
		private Dictionary<PacketTypes, Func<BinaryReader, TerrariaPacket>> DeserializerMap { get; set; }

		/// <summary>
		/// Delegate method for packet events
		/// </summary>
		/// <param name="packet">Packet being sent or received</param>
		public delegate void PacketEvent<TerrariaPacket>(TerrariaPacket packet);

		#region Received packet events

		/// <summary>
		/// Fired when a ConnectRequest packet is received
		/// </summary>
		public event PacketEvent<ConnectRequest> OnReceivedConnectRequest;
		/// <summary>
		/// Fired when a PlayerInfo packet is received
		/// </summary>
		public event PacketEvent<PlayerInfo> OnReceivedPlayerInfo;
		/// <summary>
		/// Fired when a PlayerSlot packet is received
		/// </summary>
		public event PacketEvent<InventorySlot> OnReceivedPlayerSlot;
		/// <summary>
		/// Fired when a ContinueConnecting2 packet is received
		/// </summary>
		public event PacketEvent<ContinueConnecting2> OnReceivedContinueConnecting2;

		#endregion

		#region Sent packet events

		/// <summary>
		/// Fired when a Disconnect packet is sent
		/// </summary>
		public event PacketEvent<Disconnect> OnSendDisconnect;
		/// <summary>
		/// Fired when a ContinueConnecting packet is sent
		/// </summary>
		public event PacketEvent<ContinueConnecting> OnSendContinueConnecting;
		/// <summary>
		/// Fired when a PlayerInfo packet is sent
		/// </summary>
		public event PacketEvent<PlayerInfo> OnSendPlayerInfo;
		/// <summary>
		/// Fired when a PlayerSlot packet is sent
		/// </summary>
		public event PacketEvent<InventorySlot> OnSendPlayerSlot;
		/// <summary>
		/// Fired when a WorldInfo packet is sent
		/// </summary>
		public event PacketEvent<WorldInfo> OnSendWorldInfo;

		#endregion

		/// <summary>
		/// Repackages raw packet data into packet objects
		/// </summary>
		/// <param name="core"></param>
		public PacketRepackager(Orion core)
		{
			_core = core;
			DeserializerMap = new Dictionary<PacketTypes, Func<BinaryReader, TerrariaPacket>>
			{
				{PacketTypes.ConnectRequest, br => new ConnectRequest(br)},
				{PacketTypes.PlayerInfo, br => new PlayerInfo(br)},
				{PacketTypes.PlayerSlot, br => new InventorySlot(br)},
                {PacketTypes.ContinueConnecting2, br => new ContinueConnecting2(br)}
			};
		}

		/// <summary>
		/// Repackages packets caught by the NetSendData hook
		/// </summary>
		/// <param name="e"></param>
		internal void SendAndRepackage(SendDataEventArgs e)
		{
			TerrariaPacket packet = null;

			switch (e.MsgId)
			{
				case PacketTypes.Disconnect:
					if (OnSendDisconnect != null)
					{
						packet = new Disconnect(e.MsgId, e.text);
						OnSendDisconnect((Disconnect)packet);
						packet.SetNewData(ref e);
					}
					break;

				case PacketTypes.ContinueConnecting:
					if (OnSendContinueConnecting != null)
					{
						packet = new ContinueConnecting(e.MsgId, e.remoteClient);
						OnSendContinueConnecting((ContinueConnecting)packet);
						packet.SetNewData(ref e);
					}
					break;

				case PacketTypes.PlayerInfo:
					if (OnSendPlayerInfo != null)
					{
						packet = new PlayerInfo(e.MsgId, e.number, e.text);
						OnSendPlayerInfo((PlayerInfo)packet);
						packet.SetNewData(ref e);
					}
					break;

				case PacketTypes.PlayerSlot:
					if (OnSendPlayerSlot != null)
					{
						packet = new InventorySlot(e.MsgId, e.number, e.number2, _core.NetUtils);
						OnSendPlayerSlot((InventorySlot)packet);
						packet.SetNewData(ref e);
					}
					break;

				case PacketTypes.WorldInfo:
					if (OnSendWorldInfo != null)
					{
						packet = new WorldInfo(e.MsgId);
						OnSendWorldInfo((WorldInfo)packet);
						packet.SetNewData(ref e);
					}
					break;
			}

			if (packet != null)
			{
				e.Handled = packet.handled;
			}
		}

		/// <summary>
		/// Repackages packets caught by the NetGetData hook
		/// </summary>
		/// <param name="e"></param>
		internal void GetAndRepackage(GetDataEventArgs e)
		{
			if (!DeserializerMap.ContainsKey(e.MsgID))
			{
				return;
			}

			//Deserialize packet inside switch so that we're not reading data from unhandled packets
			TerrariaPacket packet = null;

			switch (e.MsgID)
			{
				case PacketTypes.ConnectRequest:
					if (OnReceivedConnectRequest != null)
					{
						packet = DeserializerMap[e.MsgID](e.Msg.reader);
						OnReceivedConnectRequest((ConnectRequest)packet);
					}
					break;

				case PacketTypes.PlayerInfo:
					if (OnReceivedPlayerInfo != null)
					{
						packet = DeserializerMap[e.MsgID](e.Msg.reader);
						OnReceivedPlayerInfo((PlayerInfo)packet);
					}
					break;

				case PacketTypes.PlayerSlot:
					if (OnReceivedPlayerSlot != null)
					{
						packet = DeserializerMap[e.MsgID](e.Msg.reader);
						OnReceivedPlayerSlot((InventorySlot)packet);
					}
					break;

				case PacketTypes.ContinueConnecting2:
					if (OnReceivedContinueConnecting2 != null)
					{
						packet = DeserializerMap[e.MsgID](e.Msg.reader);
						OnReceivedContinueConnecting2((ContinueConnecting2)packet);
					}
					break;
			}

			if (packet != null)
			{
				e.Handled = packet.handled;
			}
		}
	}
}
