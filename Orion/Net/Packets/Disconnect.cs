namespace Orion.Net.Packets
{
	public class Disconnect : TerrariaPacket
	{
		/// <summary>
		/// Reason for disconnecting
		/// </summary>
		public string Reason { get; set; }

		/// <summary>
		/// Used when packet is sent
		/// </summary>
		/// <param name="id"></param>
		/// <param name="reason"></param>
		internal Disconnect(PacketTypes id, string reason)
			: base(id)
		{
			Reason = reason;
		}

		/// <summary>
		/// Used when packet is sent
		/// </summary>
		/// <param name="id"></param>
		/// <param name="reason"></param>
		internal Disconnect(byte id, string reason)
			: base(id)
		{
			Reason = reason;
		}

		/// <summary>
		/// Sets the text value of the SendDataEvent args
		/// </summary>
		/// <param name="e"></param>
		internal override void SetNewData(ref TerrariaApi.Server.SendDataEventArgs e)
		{
			e.text = Reason;
		}
	}
}
