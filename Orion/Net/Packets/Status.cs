namespace Orion.Net.Packets
{
	/// <summary>
	/// Status [9] packet. Sent from the server to the client
	/// </summary>
	public class Status : TerrariaPacket
	{
		/// <summary>
		/// Only ever increases
		/// </summary>
		public int StatusMax { get; set; }
		/// <summary>
		/// Status message
		/// </summary>
		public string Message { get; set; }

		internal Status(string message) 
			: base(PacketTypes.Status)
		{
			Message = message;
		}
	}
}
