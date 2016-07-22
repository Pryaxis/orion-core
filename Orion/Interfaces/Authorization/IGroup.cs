using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Interfaces.Authorization
{
    /// <summary>
    /// Organises players together into a single entity with a list of permissions that
    /// will apply to all players in that group.
    /// </summary>
    public interface IGroup
    {
        /// <summary>
        /// Gets the group name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets a human-readable description of the group.
        /// </summary>
        /// <example>
        /// "Enables access into a specific region".
        /// </example>
        string Description { get; }

        /// <summary>
        /// Gets all users that are members of this group.
        /// </summary>
        IEnumerable<IUserAccount> Members { get; set; }

        /// <summary>
        /// Gets all permissions that members of this group inherit.
        /// </summary>
        IEnumerable<IPermission> Permissions { get; set; }

        /// <summary>
        /// Determines whether this group contains the specified player
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        bool HasMember(IPlayer player);
    }
}
