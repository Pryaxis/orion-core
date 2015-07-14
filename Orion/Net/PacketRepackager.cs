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

		public delegate void PacketEventD<in T>(T packet) where T : TerrariaPacket;

		#region Received packet events

		public event PacketEventD<ConnectRequest> OnReceivedConnectRequest;

		public event PacketEventD<ContinueConnecting> OnReceivedContinueConnecting;

		#endregion

		#region Sent packet events

		public event PacketEventD<Disconnect> OnSendDisconnect;

		#endregion

		public PacketRepackager(Orion core)
		{
			_core = core;
			DeserializerMap = new Dictionary<PacketTypes, Func<BinaryReader, TerrariaPacket>>
			{
				{PacketTypes.ConnectRequest, br => new ConnectRequest(br)},
				{PacketTypes.ContinueConnecting, br => new ContinueConnecting(br)},
				{PacketTypes.PlayerInfo, br => new PlayerInfo(br)}
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
						e.text = ((Disconnect) packet).Reason;
					}
					break;
			}

			if (packet != null)
			{
				e.Handled = packet.handled;
			}
		}

		internal void GetAndRepackage(GetDataEventArgs e)
		{
			if (!DeserializerMap.ContainsKey(e.MsgID))
			{
				//Do... something
				return;
			}

			//Deserialize packet inside switch so that we're not reading data from unhandled packets
			TerrariaPacket packet = null;

			switch (e.MsgID)
			{
				case PacketTypes.ConnectRequest:

					if (OnReceivedConnectRequest != null)
					{
						packet = DeserializerMap[e.MsgID](e.Msg.binaryReader);
						OnReceivedConnectRequest((ConnectRequest) packet);
					}
					break;

				case PacketTypes.ContinueConnecting:

					if (OnReceivedContinueConnecting != null)
					{
						packet = DeserializerMap[e.MsgID](e.Msg.binaryReader);
						OnReceivedContinueConnecting((ContinueConnecting) packet);
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
