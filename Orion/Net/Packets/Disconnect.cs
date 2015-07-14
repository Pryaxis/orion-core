namespace Orion.Net.Packets
{
	public class Disconnect : TerrariaPacket
	{
		public string Reason { get; set; }

		internal Disconnect(PacketTypes id, string reason) 
			: base(id)
		{
			Reason = reason;
		}
	}
}
