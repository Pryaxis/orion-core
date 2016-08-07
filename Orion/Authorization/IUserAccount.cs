using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Authentication;

namespace Orion.Authorization
{
	/// <summary>
	/// Describes an Orion user account.
	/// </summary>
	public interface IUserAccount
	{
		/// <summary>
		/// Gets or sets the Orion account name.
		/// </summary>
		string AccountName { get; set; }

		/// <summary>
		/// Determines if the user account is a member of the specified group.
		/// </summary>
		/// <param name="group">
		/// A reference to an Orion group to check if the user is a member of.
		/// </param>
		/// <returns>
		/// true if the user is found to be a part of the specified group, false otherwise.
		/// </returns>
		bool MemberOf(IGroup group);

		/// <summary>
		/// Gets all permissions in all groups this user is a member of.
		/// </summary>
		IEnumerable<IPermission> Permissions { get; }

		/// <summary>
		/// Determines whether this user has the specified permission.
		/// </summary>
		/// <param name="permission">The <see cref="IPermission"/> to check.</param>
		/// <param name="inherit">Whether or not to include permissions inherited from parent groups.</param>
		/// <returns>true if the user has the permission, false otherwise.</returns>
		bool HasPermission(IPermission permission, bool inherit = true);

		/// <summary>
		/// Authenticates a login attempt to this account by the specified clear-text <paramref name="password"/>.
		/// </summary>
		/// <param name="password">
		/// A string containing the clear-text password for this user account.
		/// </param>
		/// <param name="ignoreExpiry">
		/// (optional) A flag indicating whether to ignore the password expiry on this account and authenticate anyway.
		/// </param>
		/// <returns>
		/// true if authentication succeeded, false otherwise.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// Thrown when <paramref name="password"/> is null or empty.
		/// </exception>
		bool Authenticate(string password, bool? ignoreExpiry = false);

		/// <summary>
		/// Sets the password on this account to the clear-text password specified.
		/// </summary>
		/// <param name="password">
		/// A clear-text password to set the account password to.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// Thrown when <paramref name="password"/> is null or empty.
		/// </exception>
		void SetPassword(string password);

		/// <summary>
		/// Sets the password on this account to the clear-text password specified, authenticating the current user first.
		/// </summary>
		/// <param name="currentPassword">
		/// A string containing the current password in clear-text for authenticating.
		/// </param>
		/// <param name="newPassword">
		/// A string containing the new password to set on the account, if authentication succeeds.
		/// </param>
		/// <exception cref="AuthenticationException">
		/// Thrown when <paramref name="currentPassword"/> does not match the current password on the account.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// Thrown when <paramref name="currentPassword"/> or <paramref name="newPassword"/> are null or empty.
		/// </exception>
		void ChangePassword(string currentPassword, string newPassword);
	}
}
