namespace Orion.Net
{
	/// <summary>
	/// Represents data sent in a packet
	/// </summary>
	public class PacketData
	{
		/// <summary>
		/// Packet type
		/// </summary>
		public PacketTypes packet;
		public int remoteClient;
		public int ignoreClient;
		public string text;
		public int num;
		public float num2;
		public float num3;
		public float num4;
		public int num5;

		/// <summary>
		/// Creates an empty instance of the PacketData class
		/// </summary>
		public PacketData() { }

		/// <summary>
		/// Creates an instance of the PacketData class using the given values
		/// </summary>
		/// <param name="packet">Packet being represented</param>
		/// <param name="remoteClient"></param>
		/// <param name="ignoreClient">Client who should not receive this packet. -1 to ignore no one</param>
		/// <param name="text">Default ""</param>
		/// <param name="num">Default 0</param>
		/// <param name="num2">Default -0f</param>
		/// <param name="num3">Default -0f</param>
		/// <param name="num4">Default -0f</param>
		/// <param name="num5">Default -0</param>
		public PacketData(int packet, int remoteClient = -1, int ignoreClient = -1, string text = "",
			int num = 0, float num2 = 0f, float num3 = 0f, float num4 = 0f, int num5 = 0)
			: this((PacketTypes) packet, remoteClient, ignoreClient, text, num, num2, num3, num4, num5)
		{
			
		}

		/// <summary>
		/// Creates an instance of the PacketData class using the given values
		/// </summary>
		/// <param name="packet">Packet being represented</param>
		/// <param name="remoteClient">Default -1</param>
		/// <param name="ignoreClient">Client who should not receive this packet. -1 to ignore no one</param>
		/// <param name="text">Default ""</param>
		/// <param name="num">Default 0</param>
		/// <param name="num2">Default -0f</param>
		/// <param name="num3">Default -0f</param>
		/// <param name="num4">Default -0f</param>
		/// <param name="num5">Default -0</param>
		public PacketData(PacketTypes packet, int remoteClient = -1, int ignoreClient = -1, string text = "",
			int num = 0, float num2 = 0f, float num3 = 0f, float num4 = 0f, int num5 = 0)
		{
			this.packet = packet;
			this.remoteClient = remoteClient;
			this.ignoreClient = ignoreClient;
			this.text = text;
			this.num = num;
			this.num2 = num2;
			this.num3 = num3;
			this.num4 = num4;
			this.num5 = num5;
		}
	}
}
