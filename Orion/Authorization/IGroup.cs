using System;
using System.Collections.Generic;
using Orion.Players;

namespace Orion.Authorization
{
	/// <summary>
	/// Organises players together into a single entity with a list of permissions that
	/// will apply to all players in that group.
	/// </summary>
	public interface IGroup
	{
		/// <summary>
		/// Gets the group name.
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// Gets a human-readable description of the group.
		/// </summary>
		/// <example>
		/// "Enables access into a specific region".
		/// </example>
		string Description { get; set; }

		/// <summary>
		/// Gets all users that are members of this group.
		/// </summary>
		IEnumerable<IUserAccount> Members { get; }

		/// <summary>
		/// Gets all permissions that members of this group inherit.
		/// </summary>
		IEnumerable<IPermission> Permissions { get; }

		/// <summary>
		/// Adds an <see cref="IUserAccount"/> to this group's list of members.
		/// </summary>
		/// <param name="userAccount">The user account to add.</param>
		/// <exception cref="InvalidOperationException">
		/// Thrown when the <paramref name="userAccount"/> already exists.
		/// </exception>
		IUserAccount AddMember(IUserAccount userAccount);

		/// <summary>
		/// Removes an <see cref="IUserAccount"/> from this group's list of members.
		/// </summary>
		/// <param name="userAccount">A reference to the user account to be removed.</param>
		void RemoveMember(IUserAccount userAccount);

		/// <summary>
		/// Determines whether this group contains the specified user account.
		/// </summary>
		/// <param name="userAccount">The user account to check.</param>
		/// <returns>true if the group contains the <paramref name="userAccount"/>, false otherwise.</returns>
		bool HasMember(IUserAccount userAccount);
	}
}
