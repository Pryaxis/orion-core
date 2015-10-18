using System.IO;

namespace Orion.NetData.Packets
{
	/// <summary>
	/// Connect Request [1] packet. Sent from the client to the server.
	/// </summary>
	public class ConnectRequest : TerrariaPacketBase
	{
		/// <summary>
		/// Terraria version sent by the client.
		/// Should be "Terraria" + <see cref="Terraria.Main.curRelease"/>.
		/// </summary>
		public string Version { get; private set; }

		/// <summary>
		/// Creates a new Connect Request packet by reading data from <paramref name="reader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="BinaryReader"/> object with the data to be read.</param>
		internal ConnectRequest(BinaryReader reader) 
			: base(reader)
		{
			Version = reader.ReadString();
		}
	}
}
