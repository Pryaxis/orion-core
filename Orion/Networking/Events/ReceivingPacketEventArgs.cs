using System;
using System.ComponentModel;
using Multiplicity.Packets;

namespace Orion.Networking.Events
{
	/// <summary>
	/// Provides data for the <see cref="INetworkService.ReceivingPacket"/> event.
	/// </summary>
	public class ReceivingPacketEventArgs : HandledEventArgs
	{
		/// <summary>
		/// Gets the packet that is being received.
		/// </summary>
		public TerrariaPacket Packet { get; }

		/// <summary>
		/// Gets the packet's sender.
		/// </summary>
		public Terraria.RemoteClient Sender { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ReceivingPacketEventArgs"/> class.
		/// </summary>
		/// <param name="sender">The packet's sender.</param>
		/// <param name="packet">The packet that is being received.</param>
		public ReceivingPacketEventArgs(Terraria.RemoteClient sender, TerrariaPacket packet)
		{
			if (sender == null)
			{
				throw new ArgumentNullException(nameof(sender));
			}
			if (packet == null)
			{
				throw new ArgumentNullException(nameof(packet));
			}

			Sender = sender;
			Packet = packet;
		}
	}
}
