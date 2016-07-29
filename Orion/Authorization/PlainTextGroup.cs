using System.Collections.Generic;
using Orion.Entities.Player;

namespace Orion.Authorization
{
	public class PlainTextGroup : IGroup
	{
		public string Name { get; }
		public string Description { get; }
		public IEnumerable<IUserAccount> Members { get; set; }
		public IEnumerable<IPermission> Permissions { get; set; }
		public bool HasMember(IPlayer player)
		{
			throw new System.NotImplementedException();
		}
	}
}