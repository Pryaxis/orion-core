
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Text;
using IniParser;
using IniParser.Model;
using Orion.Extensions;
using Orion.Framework;

namespace Orion.Authorization
{
	/// <summary>
	/// Plain-text user account linked to the plain text account service.
	/// </summary>
	public class PlainTextUserAccount : IUserAccount
	{
		private PlainTextAccountService _service;
		protected IniData _iniData;

		/// <inheritdoc />
		public string AccountName
		{
			get { return _iniData.Sections["User"]["AccountName"]; }
			set { _iniData.Sections["User"]["AccountName"] = value; }
		}

		public PlainTextUserAccount(PlainTextAccountService service)
		{
			this._iniData = new IniData();
			this._iniData.Sections.AddSection("User");
		}

		public PlainTextUserAccount(PlainTextAccountService service, Stream stream)
		{
			this._service = service;

			var parser = new StreamIniDataParser();
			this._iniData = parser.ReadData(new StreamReader(stream));
		}

		/// <inheritdoc />
		public bool MemberOf(IGroup group)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public IEnumerable<IPermission> Permissions { get; }

		/// <inheritdoc />
		public bool HasPermission(IPermission permission, bool inherit = true)
		{
			throw new NotImplementedException();
		}


		public void ToStream(Stream stream)
		{
			var parser = new StreamIniDataParser();

			using (StreamWriter sw = new StreamWriter(stream, Encoding.UTF8, 1024, leaveOpen: true))
			{
				parser.WriteData(sw, _iniData);
			}
		}
	}

	/// <summary>
	/// Plain text account and group service which sources its account information from flat
	/// files in Orion's data subdirectory.
	/// </summary>
	public class PlainTextAccountService : ServiceBase, IUserAccountService, IGroupService
	{
		protected string _userPathPrefix = $"data{Path.DirectorySeparatorChar}users";
		protected string _userGroupPrefix = $"data{Path.DirectorySeparatorChar}groups";

		/// <inheritdoc />
		public PlainTextAccountService(Orion orion) : base(orion)
		{
			Directory.CreateDirectory(_userPathPrefix);
		}

		protected string GetAccountPath(IUserAccount account)
		{
			return Path.Combine(_userPathPrefix, account.AccountName.Slugify());
		}

		/// <inheritdoc />
		public IEnumerable<IUserAccount> Find(Predicate<IUserAccount> predicate = null)
		{
			foreach (var filePath in Directory.GetFiles(_userPathPrefix, "*.ini"))
			{
				using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					PlainTextUserAccount userAccount = new PlainTextUserAccount(this, fs);

					if (predicate != null && predicate(userAccount))
					{
						yield return userAccount;
					}
				}
			}
		}

		/// <inheritdoc />
		public IUserAccount GetUserAccountOrDefault(string accountName)
		{
			string accountPath = Path.Combine(_userPathPrefix, $"{accountName.Slugify()}.ini");

			if (File.Exists(accountPath) == false)
			{
				return default(IUserAccount);
			}

			using (FileStream fs = new FileStream(accountPath, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				return new PlainTextUserAccount(this, fs);
			}
		}

		/// <inheritdoc />
		public void AddAccount(string accountName)
		{
			string accountPath = Path.Combine(_userPathPrefix, $"{accountName.Slugify()}.ini");
			PlainTextUserAccount userAccount;

			if (string.IsNullOrEmpty(accountName) == true)
			{
				throw new ArgumentNullException(nameof(accountName));
			}

			if (File.Exists(accountPath) == true)
			{
				throw new InvalidOperationException($"Account by the name of {accountName} already exists.");
			}

			userAccount = new PlainTextUserAccount(this)
			{
				AccountName = accountName
			};

			using (FileStream fs = new FileStream(accountPath, FileMode.Open, FileAccess.Write, FileShare.None))
			{
				userAccount.ToStream(fs);
			}
		}

		/// <inheritdoc />
		public void SetPassword(IUserAccount userAccount, string password)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public void ChangePassword(IUserAccount userAccount, string currentPassword, string newPassword)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public bool Authenticate(IUserAccount userAccount, string password)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public IGroup AdministratorGroup { get; }

		/// <inheritdoc />
		public IGroup AnonymousGroup { get; }

		/// <inheritdoc />
		public IEnumerable<IGroup> Find(Predicate<IGroup> predicate)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public void AddGroup(string groupName, IEnumerable<IUserAccount> initialMembers = null)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public void AddMembers(IGroup @group, params IUserAccount[] userAccounts)
		{
			throw new NotImplementedException();
		}
	}
}
