using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Interfaces.Authorization
{
    /// <summary>
    /// An authorization object which controls player access to Orion features, such as commands and
    /// custom plugin functionality.
    /// </summary>
    public interface IPermission
    {
        /// <summary>
        /// Gets the permission name.  Must not contain any period (".") characters
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets a human-readable description of the permission.
        /// </summary>
        /// <remarks>
        /// It is helpful to include what the permission is for in your description.
        /// </remarks>
        /// <example>
        /// "Enables access to the teleport command in tshock".
        /// </example>
        string Description { get; } 

        /// <summary>
        /// Gets the parent permission in the permission tree
        /// </summary>
        IPermission Parent { get; }

        /// <summary>
        /// Gets all the permission objects that are listed as children of this permission
        /// </summary>
        IEnumerable<IPermission> ChildPermissions { get; }

        /// <summary>
        /// Determines whether the specified player has this permission, or optionally any parent permission.
        /// </summary>
        /// <param name="player">
        /// A reference to a player object to check for permissions
        /// </param>
        /// <param name="inherit">
        /// (optional) A flag indicating whether to check all parents of this permission for authorization
        /// </param>
        /// <returns>
        /// true if the player has this permission, or optionally any parent permission, false otherwise.
        /// </returns>
        bool HasPermission(IPlayer player, bool inherit = true);
    }
}
