
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

        /// <summary>
        /// Gets or sets the bcrypt password hash on this account.
        /// </summary>
        /// <remarks>
        /// This is a hidden property.  Password hashes must not be leaked outside of this instance.
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
        /// Initializes a new instance of a plain text user account
        /// </summary>
		public PlainTextUserAccount(PlainTextAccountService service)
		{
			this._iniData = new IniData();
			this._iniData.Sections.AddSection("User");
		}

        /// <summary>
        /// Initializes a new instance of a plain text user account with the provided account name, which will
        /// load the account name from disk.
        /// </summary>
        /// <param name="service">
        /// A reference to the plain text account service which owns this user account.
        /// </param>
        /// <param name="accountName">
        /// A string containing the account name to load from disk.
        /// </param>
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
        /// Initializes a new instance of a plain text user account from the specified I/O stream.
        /// </summary>
	    public PlainTextUserAccount(PlainTextAccountService service, Stream stream)
            : this(service)
	    {
			StreamIniDataParser parser = new StreamIniDataParser();

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

        /// <inheritdoc />
	    public bool Authenticate(string password, bool? ignoreExpiry = false)
	    {
	        if (string.IsNullOrEmpty(password))
	        {
	            throw new ArgumentNullException(nameof(password));
	        }

	        if (string.IsNullOrEmpty(PasswordHash) == true)
	        {
                /*
                 * Authentication cannot succeed if there is no password at all.
                 */
	            return false;
	        }

	        return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
	    }

        /// <inheritdoc />
	    public void SetPassword(string password)
	    {
	        if (string.IsNullOrEmpty(password))
	        {
	            throw new ArgumentNullException(nameof(password));
	        }

	        PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
	        Save();
	    }

        /// <inheritdoc />
	    public void ChangePassword(string currentPassword, string newPassword)
	    {
	        if (string.IsNullOrEmpty(currentPassword)
                || string.IsNullOrEmpty(newPassword))
	        {
	            throw new ArgumentNullException("currentPassword or newPassword");
	        }

	        if (Authenticate(currentPassword, ignoreExpiry: false) == false)
	        {
	            throw new AuthenticationException("Authentication failed: password was incorrect.");
	        }

            SetPassword(newPassword);
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
		}

		protected string GetAccountPath(IUserAccount account)
		{
			return Path.Combine(UserPathPrefix, account.AccountName.Slugify());
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
