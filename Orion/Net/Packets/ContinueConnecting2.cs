using System.IO;

namespace Orion.Net.Packets
{
	/// <summary>
	/// Continue Connecting 2 [6] packet. Sent by the client to the server.
	/// </summary>
	public class ContinueConnecting2 : TerrariaPacket
	{
		/// <summary>
		/// Creates a new Continue Connecting 2 packet by reading data from <paramref name="reader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="BinaryReader"/> object with the data to be read.</param>
		public ContinueConnecting2(BinaryReader reader)
			: base(reader)
		{

		}
	}
}
