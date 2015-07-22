using System.IO;

namespace Orion.Net.Packets
{
	/// <summary>
	/// ContinueConnecting2 packet
	/// </summary>
	public class ContinueConnecting2 : TerrariaPacket
	{
		/// <summary>
		/// Used when packet is received
		/// </summary>
		/// <param name="reader"></param>
		public ContinueConnecting2(BinaryReader reader)
			: base(reader)
		{

		}
	}
}
