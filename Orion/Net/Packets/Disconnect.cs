using TerrariaApi.Server;

namespace Orion.Net.Packets
{
	/// <summary>
	/// Disconnect [2] packet. Sent from the server to the client.
	/// </summary>
	public class Disconnect : TerrariaPacket
	{
		/// <summary>
		/// Reason for disconnecting.
		/// </summary>
		public string Reason { get; set; }

		/// <summary>
		/// Creates a new Disconnect packet with the given disconnect <paramref name="reason"/>.
		/// </summary>
		/// <param name="reason">Reason for disconnecting.</param>
		internal Disconnect(string reason)
			: base(PacketTypes.Disconnect)
		{
			Reason = reason;
		}

		/// <summary>
		/// Sets the text value of <paramref name="e"/> to <see cref="Reason"/>.
		/// </summary>
		/// <param name="e">The <see cref="SendDataEventArgs"/> to be set.</param>
		internal override void SetNewData(ref SendDataEventArgs e)
		{
			e.text = Reason;
		}
	}
}
