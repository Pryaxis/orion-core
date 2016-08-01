using System;
using System.Collections;
using System.Collections.Generic;
using Orion.Framework;

namespace Orion.Authorization
{
	/// <summary>
	/// Service Definition: UserAccountService.
	/// 
	/// Controls access to user accounts, groups and permissions.
	/// </summary>
	public interface IUserAccountService : IService
	{
		/// <summary>
		/// Returns a list of user accounts in the system, optionally filtered by a predicate expression.
		/// </summary>
		/// <param name="predicate">
		/// (optional) A predicate expression for filtering user accounts.
		/// </param>
		/// <returns>
		/// An enumerable of <see cref="IUserAccount"/> objects satisfying the supplied predicate.  If no predicate
		/// is specified, an enumerable of all user accounts are returned.
		/// </returns>
		IEnumerable<IUserAccount> Find(Predicate<IUserAccount> predicate = null);

		/// <summary>
		/// Returns a user account by the specified account name, or a default value if one cannot be found.
		/// </summary>
		/// <param name="accountName">
		/// A string containing the account name to retrieve from the service.
		/// </param>
		/// <returns>
		/// A user account object if one was found by the specified <paramref name="accountName"/>, or the compiler.
		/// default if one cannot be found.
		/// </returns>
		IUserAccount GetUserAccountOrDefault(string accountName);

		/// <summary>
		/// Adds an account with the specified account name to the account service in the default group.
		/// </summary>
		/// <param name="accountName">
		/// A string referring to the account name.
		/// </param>
		/// <exception cref="InvalidOperationException">
		/// Thrown when the account name already exists.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// Thrown if <paramref name="accountName"/> is null or empty.
		/// </exception>
		IUserAccount AddAccount(string accountName);

		/// <summary>
		/// Deletes the user account by the account name specified.
		/// </summary>
		/// <param name="accountName">
		/// A string containing the account name to delete.
		/// </param>
		void DeleteAccount(string accountName);

		/// <summary>
		/// Sets the account password to the specified password in clear-text.
		/// </summary>
		/// <param name="userAccount">
		/// A reference to the user account to set the password.
		/// </param>
		/// <param name="password">
		/// A string containing the clear text password of the account to be changed to.
		/// </param>
		/// <remarks>
		/// This method force-updates the password on this account without re-authenticating, and should be considered
		/// an admin-only function.
		/// </remarks>
		void SetPassword(IUserAccount userAccount, string password);

		/// <summary>
		/// Updates the account password with a clear-text password specified by <paramref name="newPassword"/>,
		/// if the current password on the account matches the clear-text password specified by
		/// <paramref name="currentPassword"/>.
		/// </summary>
		/// <param name="userAccount">
		/// A user account to change the password.
		/// </param>
		/// <param name="currentPassword">
		/// A string containing the clear-text password currently on the account.
		/// </param>
		/// <param name="newPassword">
		/// A string containing the new clear-text password for the account, if the current password matches.
		/// </param>
		/// <exception cref="InvalidOperationException">
		/// Thrown if authentication failed on the account specifying <paramref name="currentPassword"/>, or
		/// <paramref name="newPassword"/> does not meet the password complexity requirements.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// Thrown if <paramref name="currentPassword"/> or <paramref name="newPassword"/> is null or empty.
		/// </exception>
		void ChangePassword(IUserAccount userAccount, string currentPassword, string newPassword);

		/// <summary>
		/// Authenticates this user account with the specified clear-text password.
		/// </summary>
		/// <param name="userAccount">
		/// A reference to a user account object to authenticate with.
		/// </param>
		/// <param name="password">
		/// A string containing the clear-text password to authenticate this account with.
		/// </param>
		/// <returns>
		/// true if the account authentication succeeded, false otherwise.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// Thrown when <paramref name="password"/> is null or empty.
		/// </exception>
		bool Authenticate(IUserAccount userAccount, string password);
	}
}
