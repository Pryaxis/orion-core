using Orion.Framework.Events;

namespace Orion.NetData.Packets
{
	public class SectionTileFrame : TerrariaPacketBase
	{
		public short StartX { get; set; }
		public short StartY { get; set; }
		public short EndX { get; set; }
		public short EndY { get; set; }

		internal SectionTileFrame(NetSendDataEventArgs e) : base(PacketTypes.TileFrameSection)
		{
			StartX = (short)e.Number;
			StartY = (short)e.Number2;
			EndX = (short)e.Number3;
			EndY = (short)e.Number4;
		}

		internal override void SetNewData(ref NetSendDataEventArgs e)
		{
			e.Number = StartX;
			e.Number2 = StartY;
			e.Number3 = EndX;
			e.Number4 = EndY;
		}
	}
}
