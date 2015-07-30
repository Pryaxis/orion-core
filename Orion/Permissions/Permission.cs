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

		public bool Equals(Permission permission)
		{
			return permission.Negate = Negate && permission.Name == Name;
		}

		public bool Equals(string permission)
		{
			if (Negate && !permission.StartsWith("!"))
			{
				return false;
			}
			
			if (Negate && permission.StartsWith("!"))
			{
				return permission.Remove(0, 1) == Name;
			}

			if (!Negate && permission.StartsWith("!"))
			{
				return false;
			}

			return permission == Name;
		}

		public override string ToString()
		{
			return (Negate ? "!" : "") + Name;
		}
	}
}
