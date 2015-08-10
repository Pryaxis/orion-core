using Orion.Permissions;
using System.Collections.Generic;

namespace Orion.Collections
{
	public class ParentCollection : IntCollection
	{
		public ParentCollection()
			:base()
		{

		}

		public ParentCollection(string parents)
			:base(parents)
		{

		}

		public ParentCollection(List<int> parents)
			:base(parents)
		{

		}
	}
}
