using System;

namespace Orion.Extensions
{
	public static class StringEx
	{
		public static Color ToColor(this string str)
		{
			byte r, g, b;
			string[] pieces = str.Split(',');
			if (pieces.Length < 3 || pieces.Length > 3)
			{
				throw new ArgumentException("Provided string must be in the format rrr,ggg,bbb", "str");
			}

			if (!Byte.TryParse(pieces[0], out r))
			{
				throw new ArgumentException("Provided red value is not a byte value.", "str");
			}

			if (!Byte.TryParse(pieces[1], out g))
			{
				throw new ArgumentException("Provided green value is not a byte value.", "str");
			}

			if (!Byte.TryParse(pieces[2], out b))
			{
				throw new ArgumentException("Provided blue value is not a byte value.", "str");
			}

			return new Color(r, g, b);
		}
	}
}