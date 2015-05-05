using System;
using System.Text;

namespace Orion.Extensions
{
	public static class RandomEx
	{
		/// <summary>
		/// Returns a random string of length <see cref="length"/>, containing a-z, A-Z, 0-9
		/// </summary>
		/// <param name="random"></param>
		/// <param name="length">string length</param>
		/// <returns>string of length <see cref="length"/> containing a-z, A-Z, 0-9</returns>
		public static string NextString(this Random random, int length)
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < length; i++)
			{
				switch (random.Next(0, 3))
				{
					case 0:
						sb.Append((char) random.Next('a', 'z' + 1));
						break;
					case 1:
						sb.Append((char) random.Next('A', 'Z' + 1));
						break;
					case 2:
						sb.Append((char) random.Next('0', '9' + 1));
						break;
				}
			}
			return sb.ToString();
		}
	}
}