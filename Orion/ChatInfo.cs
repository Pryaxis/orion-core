using System;

namespace Orion
{
	/// <summary>
	/// Contains prefix, suffix, and color data used for formatting chat messages
	/// </summary>
	public class ChatInfo
	{
		public Color Color { get; set; }
		public string Prefix { get; set; }
		public string Suffix { get; set; }

		public ChatInfo()
		{
			Color = Color.White;
			Prefix = String.Empty;
			Suffix = String.Empty;
		}

		public ChatInfo(string prefix, string suffix, Color color)
		{
			Color = color;
			Prefix = prefix;
			Suffix = suffix;
		}
	}
}
