using System;
using System.Collections.Generic;
using Orion.Players;
using System.Threading.Tasks;

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
		IEnumerable<IAccount> Members { get; }

		/// <summary>
		/// Gets all permissions that members of this group inherit.
		/// </summary>
		IEnumerable<IPermission> Permissions { get; }

		/// <summary>
		/// Adds an <see cref="IAccount"/> to this group's list of members.
		/// </summary>
		/// <param name="userAccount">The user account to add.</param>
		/// <exception cref="InvalidOperationException">
		/// Thrown when the <paramref name="userAccount"/> already exists.
		/// </exception>
		IAccount AddMember(IAccount userAccount);

		/// <summary>
		/// Asynchronously adds an <see cref="IAccount"/> to this group's list of members.
		/// </summary>
		/// <param name="userAccount">The user account to add.</param>
		/// <exception cref="InvalidOperationException">
		/// Thrown when the <paramref name="userAccount"/> already exists.
		/// </exception>
		Task<IAccount> AddMemberAsync(IAccount userAccount);

		/// <summary>
		/// Removes an <see cref="IAccount"/> from this group's list of members.
		/// </summary>
		/// <param name="userAccount">A reference to the user account to be removed.</param>
		void RemoveMember(IAccount userAccount);

		/// <summary>
		/// Asynchronously emoves an <see cref="IAccount"/> from this group's list of members.
		/// </summary>
		/// <param name="userAccount">A reference to the user account to be removed.</param>
		Task RemoveMemberAsync(IAccount userAccount);

		/// <summary>
		/// Determines whether this group contains the specified user account.
		/// </summary>
		/// <param name="userAccount">The user account to check.</param>
		/// <returns>true if the group contains the <paramref name="userAccount"/>, false otherwise.</returns>
		bool HasMember(IAccount userAccount);

		/// <summary>
		/// Asynchronously determines whether this group contains the specified user account.
		/// </summary>
		/// <param name="userAccount">The user account to check.</param>
		/// <returns>true if the group contains the <paramref name="userAccount"/>, false otherwise.</returns>
		Task<bool> HasMemberAsync(IAccount userAccount);
	}
}
