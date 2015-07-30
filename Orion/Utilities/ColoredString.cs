using Terraria;

namespace Orion.Utilities
{
	public class ColoredString
	{ 
		public string String;
		public Color Color;
		public string HexColor
		{
			get { return Color.Hex3(); }
		}

		public ColoredString(string str, Color col)
		{
			String = str;
			Color = col;
		}

		public override string ToString()
		{
			if (Color == null)
			{
				return String;
			}

			return $"[c/{HexColor}:{String}]";
		}
	}
}
