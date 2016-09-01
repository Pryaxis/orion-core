using System;
using Multiplicity.Packets;
using Orion.Networking.Events;

namespace Orion.Networking
{
	/// <summary>
	/// Provides a mechanism for managing the network.
	/// </summary>
	public interface INetworkService
	{
		/// <summary>
		/// Occurs when a packet was received.
		/// </summary>
		event EventHandler<ReceivedPacketEventArgs> ReceivedPacket;
		
		/// <summary>
		/// Occurs when a packet is being received.
		/// </summary>
		event EventHandler<ReceivingPacketEventArgs> ReceivingPacket; 

		/// <summary>
		/// Sends the specified packet to the target.
		/// </summary>
		/// <param name="target">The target. <c>-1</c> represents everyone.</param>
		/// <param name="packet">The packet.</param>
		void SendPacket(int target, TerrariaPacket packet);
	}
}
