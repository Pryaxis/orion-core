using System;
using Multiplicity.Packets;

namespace Orion.Networking.Events
{
	/// <summary>
	/// Provides data for the <see cref="INetworkService.ReceivedPacket"/> event.
	/// </summary>
	public class ReceivedPacketEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the packet that was received.
		/// </summary>
		public TerrariaPacket Packet { get; }

		/// <summary>
		/// Gets the packet's sender.
		/// </summary>
		public Terraria.RemoteClient Sender { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ReceivedPacketEventArgs"/> class.
		/// </summary>
		/// <param name="sender">The packet's sender.</param>
		/// <param name="packet">The packet that was received.</param>
		public ReceivedPacketEventArgs(Terraria.RemoteClient sender, TerrariaPacket packet)
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
