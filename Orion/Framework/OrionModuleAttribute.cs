using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion
{
	public class OrionModuleAttribute : Attribute
	{
		public string ModuleName { get; private set; }
		public string Author { get; private set; }
		public int Order { get; private set; }

		public OrionModuleAttribute(string name, string author, int order = 1)
		{
			ModuleName = name;
			Author = author;
			Order = order;
		}
	}
}
