using System.IO;

namespace Orion.Net.Packets
{
	public class ConnectRequest : TerrariaPacket
	{
		/// <summary>
		/// Terraria version sent by the client.
		/// Should be "Terraria" + <see cref="Terraria.Main.curRelease"/>
		/// </summary>
		public string Version { get; private set; }

		/// <summary>
		/// Used when packet is received
		/// </summary>
		/// <param name="reader"></param>
		internal ConnectRequest(BinaryReader reader) 
			: base(reader)
		{
			Version = reader.ReadString();
		}
	}
}
