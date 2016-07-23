using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Interfaces.Authorization
{
    /// <summary>
    /// Describes an Orion user account.
    /// </summary>
    public interface IUserAccount
    {
        /// <summary>
        /// Gets or sets the Orion account name
        /// </summary>
        string AccountName { get; set; }

        /// <summary>
        /// Determines if the user account is a member of the specified group thus inheriting all the group's permissions
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
        /// Determines whether this user has the specified permission, 
        /// </summary>
        /// <param name="permission"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        bool HasPermission(IPermission permission, bool inherit = true);
        
    }
}
