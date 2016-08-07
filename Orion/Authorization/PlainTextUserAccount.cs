using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Authentication;
using System.Text;
using IniParser;
using IniParser.Model;
using Orion.Extensions;

namespace Orion.Authorization
{
	/// <summary>
	/// Plain-text user account used by the <see cref="PlainTextAccountService"/>.
	/// </summary>
	public class PlainTextUserAccount : IUserAccount
	{
		private PlainTextAccountService _service;
		private IniData _iniData;

		/// <inheritdoc/>
		public string AccountName
		{
			get { return _iniData.Sections["User"]["AccountName"]; }
			set { _iniData.Sections["User"]["AccountName"] = value; }
		}

		/// <summary>
		/// Gets or sets the bcrypt password hash on this account.
		/// </summary>
		/// <remarks>
		/// This is a hidden property. Password hashes must not be leaked outside of this instance.
		/// 
		/// See <see cref="Authenticate"/> to authenticate passwords against the stored hash on this
		/// account.
		/// </remarks>
		protected string PasswordHash
		{
			get { return _iniData.Sections["User"]["Password"]; }
			set { _iniData.Sections["User"]["Password"] = value; }
		}

		/// <summary>
		/// Gets the computed account file path on disk according to the normalized account name.
		/// </summary>
		protected string AccountFilePath
			=> Path.Combine(PlainTextAccountService.UserPathPrefix, $"{AccountName.Slugify()}.ini");

		/// <summary>
		/// Initializes a new instance of the <see cref="PlainTextUserAccount"/> class.
		/// </summary>
		/// <param name="service">
		/// A reference to the <see cref="PlainTextAccountService"/> which owns this user account.
		/// </param>
		public PlainTextUserAccount(PlainTextAccountService service)
		{
			_service = service;
			this._iniData = new IniData();
			this._iniData.Sections.AddSection("User");
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PlainTextUserAccount"/> class with the provided account name, which
		/// will load the account data from disk.
		/// </summary>
		/// <param name="service">
		/// A reference to the <see cref="PlainTextAccountService"/> which owns this user account.
		/// </param>
		/// <param name="accountName">A string containing the account name to load from disk.</param>
		public PlainTextUserAccount(PlainTextAccountService service, string accountName)
			: this(service)
		{
			AccountName = accountName;

			StreamIniDataParser parser = new StreamIniDataParser();

			using (FileStream fs = new FileStream(AccountFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				this._iniData = parser.ReadData(new StreamReader(fs));
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PlainTextUserAccount"/> class from the specified I/O stream.
		/// </summary>
		/// <param name="service">
		/// A reference to the <see cref="PlainTextAccountService"/> which owns this user account.
		/// </param>
		/// <param name="stream">The I/O stream to load the <see cref="PlainTextUserAccount"/> data from.</param>
		public PlainTextUserAccount(PlainTextAccountService service, Stream stream)
			: this(service)
		{
			StreamIniDataParser parser = new StreamIniDataParser();

			this._iniData = parser.ReadData(new StreamReader(stream));
		}

		/// <inheritdoc/>
		public bool MemberOf(IGroup group)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc/>
		public IEnumerable<IPermission> Permissions { get; }

		/// <inheritdoc/>
		public bool HasPermission(IPermission permission, bool inherit = true)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc/>
		public bool Authenticate(string password, bool? ignoreExpiry = false)
		{
			if (String.IsNullOrEmpty(password))
			{
				throw new ArgumentNullException(nameof(password));
			}

			if (String.IsNullOrEmpty(PasswordHash))
			{
				// Authentication cannot succeed if there is no password at all.
				return false;
			}

			return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
		}

		/// <inheritdoc/>
		public void SetPassword(string password)
		{
			if (String.IsNullOrEmpty(password))
			{
				throw new ArgumentNullException(nameof(password));
			}

			PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
			Save();
		}

		/// <inheritdoc/>
		public void ChangePassword(string currentPassword, string newPassword)
		{
			if (String.IsNullOrEmpty(currentPassword))
			{
				throw new ArgumentNullException(nameof(currentPassword));
			}
			if (String.IsNullOrEmpty(newPassword))
			{
				throw new ArgumentNullException(nameof(newPassword));
			}

			if (!Authenticate(currentPassword, ignoreExpiry: false))
			{
				throw new AuthenticationException("Authentication failed: password was incorrect.");
			}

			SetPassword(newPassword);
			Save();
		}

		/// <summary>
		/// Saves this plain text account to file in the pre-computed location.
		/// </summary>
		public void Save()
		{
			using (FileStream fs = new FileStream(AccountFilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
			{
				ToStream(fs);
			}
		}

		/// <summary>
		/// Saves this plain text account into the specified stream.
		/// </summary>
		public void ToStream(Stream stream)
		{
			var parser = new StreamIniDataParser();

			using (StreamWriter sw = new StreamWriter(stream, Encoding.UTF8, 1024, leaveOpen: true))
			{
				parser.WriteData(sw, _iniData);
			}
		}
	}
}
