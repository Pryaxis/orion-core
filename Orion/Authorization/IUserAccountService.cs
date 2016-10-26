using System;
using System.Collections.Generic;
using Orion.Framework;
using System.Threading.Tasks;

namespace Orion.Authorization
{
	/// <summary>
	/// Controls access to user accounts, groups and permissions.
	/// </summary>
	public interface IUserAccountService : ISharedService
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
		IEnumerable<IUserAccount> FindUsers(Predicate<IUserAccount> predicate = null);

		/// <summary>
		/// Asynchronously returns a list of user accounts in the system, optionally filtered by a predicate
		/// expression.
		/// </summary>
		/// <param name="predicate">
		/// (optional) A predicate expression for filtering user accounts.
		/// </param>
		/// <returns>
		/// An awaitable Task object that provides an enumerable of <see cref="IUserAccount"/> objects satisfying
		/// the supplied predicate. If no predicate is supplied, an awaitable Task object that provides an enumerable
		/// of all user accounts is returned.
		/// </returns>
		Task<IEnumerable<IUserAccount>> FindUsersAsync(Predicate<IUserAccount> predicate = null);

		/// <summary>
		/// Returns a user account by the specified account name, or a default value if one cannot be found.
		/// </summary>
		/// <param name="username">
		/// A string containing the name of the user account to retrieve from the service.
		/// </param>
		/// <returns>
		/// A user account object if one was found by the specified <paramref name="username"/>, or the compiler.
		/// default if one cannot be found.
		/// </returns>
		IUserAccount GetUser(string username);

		/// <summary>
		/// Asynchronously returns a user account by the specified account name, or a default value if one
		/// cannot be found.
		/// </summary>
		/// <param name="username">
		/// A string containing the name of the user account to retrieve from the service.
		/// </param>
		/// <returns>
		/// An awaitable Task object providing a user account object if one was found by the specified
		/// <paramref name="username"/>, or the compiler.
		/// Default if one cannot be found.
		/// </returns>
		Task<IUserAccount> GetUserAsync(string username);

		/// <summary>
		/// Adds an account with the specified account name to the account service in the default group.
		/// </summary>
		/// <param name="username">
		/// A string referring to the user account name.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// Thrown if <paramref name="username"/> is null or empty.
		/// </exception>
		IUserAccount AddUser(string username);

		/// <summary>
		/// Asynchronously adds an account with the specified account name to the account service in the default group.
		/// </summary>
		/// <param name="username">
		/// A string referring to the account name.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// Thrown if <paramref name="username"/> is null or empty.
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown when the account name already exists.
		/// </exception>
		Task<IUserAccount> AddUserAsync(string username);

		/// <summary>
		/// Deletes a user account.
		/// </summary>
		/// <param name="user">
		/// A reference to the <see cref="IUserAccount"/> to delete.
		/// </param>
		void DeleteUser(IUserAccount user);

		/// <summary>
		/// Asynchronously deletes a user account.
		/// </summary>
		/// <param name="user">
		/// A reference to the <see cref="IUserAccount"/> to delete.
		/// </param>
		Task DeleteUserAsync(IUserAccount user);

		/// <summary>
		/// Sets the account password to the specified password in clear-text.
		/// </summary>
		/// <param name="user">
		/// A reference to the <see cref="IUserAccount"/> to set the password.
		/// </param>
		/// <param name="password">
		/// A string containing the clear text password of the account to be changed to.
		/// </param>
		/// <remarks>
		/// This method force-updates the password on this account without re-authenticating, and should be considered
		/// an admin-only function.
		/// </remarks>
		void SetPassword(IUserAccount user, string password);

		/// <summary>
		/// Asynchronously sets the account password to the specified password in clear-text.
		/// </summary>
		/// <param name="user">
		/// A reference to the <see cref="IUserAccount"/> to set the password.
		/// </param>
		/// <param name="password">
		/// A string containing the clear text password of the account to be changed to.
		/// </param>
		/// <remarks>
		/// This method force-updates the password on this account without re-authenticating, and should be considered
		/// an admin-only function.
		/// </remarks>
		Task SetPasswordAsync(IUserAccount user, string password);

		/// <summary>
		/// Updates the account password with a clear-text password specified by <paramref name="newPassword"/>,
		/// if the current password on the account matches the clear-text password specified by
		/// <paramref name="currentPassword"/>.
		/// </summary>
		/// <param name="user">
		/// A reference to the <see cref="IUserAccount"/> to change the password.
		/// </param>
		/// <param name="currentPassword">
		/// A string containing the clear-text password currently on the account.
		/// </param>
		/// <param name="newPassword">
		/// A string containing the new clear-text password for the account, if the current password matches.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// Thrown if <paramref name="currentPassword"/> or <paramref name="newPassword"/> is null or empty.
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown if authentication failed on the account specifying <paramref name="currentPassword"/>, or
		/// <paramref name="newPassword"/> does not meet the password complexity requirements.
		/// </exception>
		void ChangePassword(IUserAccount user, string currentPassword, string newPassword);

		/// <summary>
		/// Asynchronously updates the account password with a clear-text password specified by
		/// <paramref name="newPassword"/>, if the current password on the account matches the clear-text password
		/// specified by <paramref name="currentPassword"/>.
		/// </summary>
		/// <param name="user">
		/// A reference to the <see cref="IUserAccount"/> to change the password.
		/// </param>
		/// <param name="currentPassword">
		/// A string containing the clear-text password currently on the account.
		/// </param>
		/// <param name="newPassword">
		/// A string containing the new clear-text password for the account, if the current password matches.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// Thrown if <paramref name="currentPassword"/> or <paramref name="newPassword"/> is null or empty.
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown if authentication failed on the account specifying <paramref name="currentPassword"/>, or
		/// <paramref name="newPassword"/> does not meet the password complexity requirements.
		/// </exception>
		Task ChangePasswordAsync(IUserAccount user, string currentPassword, string newPassword);

		/// <summary>
		/// Authenticates this user account with the specified clear-text password.
		/// </summary>
		/// <param name="user">
		/// A reference to the <see cref="IUserAccount"/> to authenticate with.
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
		bool Authenticate(IUserAccount user, string password);

		/// <summary>
		/// Asynchronously authenticates this user account with the specified clear-text password.
		/// </summary>
		/// <param name="user">
		/// A reference to the <see cref="IUserAccount"/> to authenticate with.
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
		Task<bool> AuthenticateAsync(IUserAccount user, string password);
	}
}
