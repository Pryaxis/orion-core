namespace Orion
{
	/// <summary>
	/// Represents a Terraria buff
	/// </summary>
	public sealed class Buff
	{
		public string Name { get; private set; }
		public int Id { get; private set; }
		public string Description { get; private set; }

		public Buff(string name, int id, string description)
		{
			Name = name;
			Id = id;
			Description = description;
		}
	}
}