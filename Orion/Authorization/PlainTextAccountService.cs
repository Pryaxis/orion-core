
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Security.Authentication;
using System.Text;
using IniParser;
using IniParser.Model;
using Orion.Extensions;
using Orion.Framework;

namespace Orion.Authorization
{
	/// <summary>
	/// Plain text account and group service which sources its account information from flat
	/// files in Orion's data subdirectory.
	/// </summary>
	public class PlainTextAccountService : ServiceBase, IUserAccountService, IGroupService
	{
		public static readonly string UserPathPrefix = $"data{Path.DirectorySeparatorChar}users";
		public static readonly string UserGroupPrefix = $"data{Path.DirectorySeparatorChar}groups";

		/// <inheritdoc />
		public PlainTextAccountService(Orion orion) : base(orion)
		{
			Directory.CreateDirectory(UserPathPrefix);
			Directory.CreateDirectory(UserGroupPrefix);
		}

		/// <inheritdoc />
		public IEnumerable<IUserAccount> Find(Predicate<IUserAccount> predicate = null)
		{
			foreach (var filePath in Directory.GetFiles(UserPathPrefix, "*.ini"))
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
			string accountPath = Path.Combine(UserPathPrefix, $"{accountName.Slugify()}.ini");

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
		public IUserAccount AddAccount(string accountName)
		{
			PlainTextUserAccount userAccount;
			string accountPath;

			if (string.IsNullOrEmpty(accountName) == true)
			{
				throw new ArgumentNullException(nameof(accountName));
			}

			accountPath = Path.Combine(UserPathPrefix, $"{accountName.Slugify()}.ini");

			if (File.Exists(accountPath) == true)
			{
				throw new InvalidOperationException($"Account by the name of {accountName} already exists.");
			}

			userAccount = new PlainTextUserAccount(this)
			{
				AccountName = accountName
			};

			using (FileStream fs = new FileStream(accountPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
			{
				userAccount.ToStream(fs);
			}

			return userAccount;
		}

		/// <inheritdoc />
		public void DeleteAccount(string accountName)
		{
			string accountPath;

			if (string.IsNullOrEmpty(accountName) == true)
			{
				throw new ArgumentNullException(nameof(accountName));
			}

			accountPath = Path.Combine(UserPathPrefix, $"{accountName.Slugify()}.ini");

			File.Delete(accountPath);
		}

		/// <inheritdoc />
		public void SetPassword(IUserAccount userAccount, string password)
		{
			userAccount.SetPassword(password);
		}

		/// <inheritdoc />
		public void ChangePassword(IUserAccount userAccount, string currentPassword, string newPassword)
		{
			userAccount.ChangePassword(currentPassword, newPassword);
		}

		/// <inheritdoc />
		public bool Authenticate(IUserAccount userAccount, string password)
		{
			return userAccount.Authenticate(password);
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
		public IGroup AddGroup(string groupName, IEnumerable<IUserAccount> initialMembers = null)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public void DeleteGroup(IGroup @group)
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
