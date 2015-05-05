using System.Collections.Generic;

namespace Orion.SQL
{
	public class SqlTable
	{
		public List<SqlColumn> Columns { get; protected set; }
		public string Name { get; protected set; }

		public SqlTable(string name, params SqlColumn[] columns)
			: this(name, new List<SqlColumn>(columns))
		{
		}

		public SqlTable(string name, List<SqlColumn> columns)
		{
			Name = name;
			Columns = columns;
		}
	}
}