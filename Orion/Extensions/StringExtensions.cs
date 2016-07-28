using System.Text.RegularExpressions;

namespace Orion.Extensions
{
	/// <summary>
	/// A collection of extensions for the <see cref="System.String"/> type.
	/// </summary>
	public static class StringExtensions
	{
		/// <summary>
		/// Converts this string instance to its URL slug equivalent
		/// </summary>
		/// <param name="phrase">
		/// The string phrase to return the slug for
		/// </param>
		/// <returns>
		/// The slugified string equivalent of the <paramref name="phrase"/>.
		/// </returns>
		public static string Slugify(this string phrase)
		{
			string str = phrase.RemoveAccent().ToLower();
			// invalid chars           
			str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
			// convert multiple spaces into one space   
			str = Regex.Replace(str, @"\s+", " ").Trim();
			// cut and trim 
			str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
			str = Regex.Replace(str, @"\s", "-"); // hyphens   
			return str;
		}

		private static string RemoveAccent(this string txt)
		{
			byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
			return System.Text.Encoding.ASCII.GetString(bytes);
		}

	}
}
