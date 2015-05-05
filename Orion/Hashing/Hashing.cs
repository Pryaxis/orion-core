using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;

namespace Orion.Hashing
{
	public sealed class Hashing
	{
		/// <summary>
		/// Verifies that the given input matches the given hash. 
		/// If the given hash is not BCrypt, it is updated to be a BCrypt hash
		/// </summary>
		/// <param name="input">Unhashed string</param>
		/// <param name="hash">Hashed string</param>
		/// <returns>true if the input matches the hash</returns>
		public static bool VerifyHash(string input, ref string hash)
		{
			try
			{
				if (BCrypt.Net.BCrypt.Verify(input, hash))
				{
					// If necessary, perform an upgrade to the highest work factor.
					UpgradeHashWorkFactor(input, ref hash);
					return true;
				}
			}
			catch (SaltParseException)
			{
				if (String.Equals(HashString(input, hash), hash, StringComparison.CurrentCulture))
				{
					// Return true to keep blank passwords working but don't convert them to bcrypt.
					if (hash == "non-existant password")
					{
						return true;
					}
					// The password is not stored using BCrypt; upgrade it to BCrypt immediately
					UpgradeInputToBCrypt(input, ref hash);
					return true;
				}
				return false;
			}
			return false;
		}

		/// <summary>Creates a BCrypt hash from a string and returns it</summary>
		/// <param name="text">The plain text string to hash</param>
		/// <param name="minLength">The minimum required length of the input text</param>
		/// <param name="workFactor">BCrypt work factor</param>
		public static string CreateBCryptHash(string text, int minLength, int workFactor = 7)
		{
			string ret;

			if (text.Trim().Length < Math.Max(4, minLength))
			{
				throw new ArgumentOutOfRangeException("text", "String must be > " + minLength + " characters.");
			}
			try
			{
				ret = BCrypt.Net.BCrypt.HashPassword(text.Trim(), workFactor);
			}
			catch (ArgumentOutOfRangeException)
			{
				//TShock.Log.ConsoleError("Invalid BCrypt work factor in config file! Creating new hash using default work factor.");
				ret = BCrypt.Net.BCrypt.HashPassword(text.Trim());
			}

			return ret;
		}

		/// <summary>
		/// Upgrades a given hash to BCrypt
		/// </summary>
		/// <param name="input">input text</param>
		/// <param name="hash">old hash to be updated</param>
		private static void UpgradeInputToBCrypt(string input, ref string hash)
		{
			string oldPassword = hash;

			try
			{
				hash = BCrypt.Net.BCrypt.HashPassword(input, Orion.Config.BCryptWorkFactor);
			}
			catch (ArgumentOutOfRangeException)
			{
				//TShock.Log.ConsoleError("Invalid BCrypt work factor in config file! Upgrading user password to BCrypt using default work factor.");
				hash = BCrypt.Net.BCrypt.HashPassword(input);
			}

			//TODO: fix this
			try
			{
				//TShock.Users.SetUserPassword(this, Password);
			}
			catch /*(UserManagerException e)*/
			{
				//TShock.Log.ConsoleError(e.ToString());
				hash = oldPassword; // Revert changes
			}
		}

		/// <summary>Upgrades a password to the highest work factor available in the config.</summary>
		/// <param name="password">The raw user password (unhashed) to upgrade</param>
		/// <param name="hash">Currently hashed password to upgrade</param>
		private static void UpgradeHashWorkFactor(string password, ref string hash)
		{
			// If the destination work factor is not greater, we won't upgrade it or re-hash it
			int currentWorkFactor;
			try
			{
				currentWorkFactor = Int32.Parse((hash.Split('$')[2]));
			}
			catch (FormatException)
			{
				//TShock.Log.ConsoleError("Warning: Not upgrading work factor because bcrypt hash in an invalid format.");
				return;
			}

			if (currentWorkFactor < Orion.Config.BCryptWorkFactor)
			{
				try
				{
					hash = BCrypt.Net.BCrypt.HashPassword(password, Orion.Config.BCryptWorkFactor);
				}
				catch (ArgumentOutOfRangeException)
				{
					//TShock.Log.ConsoleError("Invalid BCrypt work factor in config file! Refusing to change work-factor on exsting password.");
				}

				//TODO: fix this
				try
				{
					//TShock.Users.SetUserPassword(this, Password);
				}
				catch /*(UserManagerException e)*/
				{
					//TShock.Log.ConsoleError(e.ToString());
				}
			}
		}

		/// <summary>
		/// A dictionary of hashing algorithms and an implementation object.
		/// </summary>
		private static readonly Dictionary<string, Func<HashAlgorithm>> HashTypes = new Dictionary
			<string, Func<HashAlgorithm>>
		{
			{"sha512", () => new SHA512Managed()},
			{"sha256", () => new SHA256Managed()},
			{"md5", () => new MD5Cng()},
			{"sha512-xp", SHA512.Create},
			{"sha256-xp", SHA256.Create},
			{"md5-xp", MD5.Create}
		};

		/// <summary>
		/// Returns a hashed string for a given string based on the config file's hash algo
		/// </summary>
		/// <param name="bytes">bytes to hash</param>
		/// <returns>string hash</returns>
		private static string HashString(byte[] bytes)
		{
			if (bytes == null)
				throw new NullReferenceException("bytes");
			Func<HashAlgorithm> func;
			if (!HashTypes.TryGetValue(Orion.Config.HashAlgorithm.ToLower(), out func))
				throw new NotSupportedException(String.Format("Hashing algorithm {0} is not supported", Orion.Config.HashAlgorithm.ToLower()));

			using (HashAlgorithm hash = func())
			{
				byte[] ret = hash.ComputeHash(bytes);
				return ret.Aggregate("", (s, b) => s + b.ToString("X2"));
			}
		}

		/// <summary>
		/// Returns a hashed string for a given string based on the config file's hash algo
		/// </summary>
		/// <param name="input">string to hash</param>
		/// <param name="hashed">current hashed password</param>
		/// <returns>string hash</returns>
		private static string HashString(string input, string hashed)
		{
			if (string.IsNullOrEmpty(input) && hashed == "non-existant password")
				return "non-existant password";
			return HashString(Encoding.UTF8.GetBytes(input));
		}
	}
}