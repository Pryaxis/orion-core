
using TerrariaApi.Server;

namespace Orion.Net.Packets
{
	/// <summary>
	/// ContinueConnecting packet
	/// </summary>
	public class ContinueConnecting : TerrariaPacket
	{
		byte Player { get; set; }

		internal ContinueConnecting(byte id, int player) 
			: base(id)
		{
			Player = (byte)player;
		}

		internal ContinueConnecting(PacketTypes id, int player) 
			: this((byte)id, player)
		{
		}

		internal override void SetNewData(ref SendDataEventArgs e)
		{
			e.remoteClient = Player;
		}
	}
}
