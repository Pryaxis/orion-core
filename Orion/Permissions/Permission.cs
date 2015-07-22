namespace Orion.Permissions
{
	public class Permission
	{
		public bool Negate { get; set; }
		public string Name { get; private set; }

		public Permission(string name)
		{
			if (name.StartsWith("!"))
			{
				Negate = true;
				name = name.Remove(0, 1);
			}
			Name = name;
		}
	}
}
