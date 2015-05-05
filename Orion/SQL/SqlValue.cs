namespace Orion.SQL
{
	public class SqlValue
	{
		public string Name { get; set; }
		public object Value { get; set; }

		public SqlValue(string name, object value)
		{
			Name = name;
			Value = value;
		}
	}
}