using Microsoft.Xna.Framework;

namespace Orion.Interfaces.Implementations
{
	/// <summary>
	/// Encapsulates a Terraria entity.
	/// </summary>
	public class Entity : IEntity
	{
		/// <summary>
		/// Gets the backing Terraria entity.
		/// </summary>
		public Terraria.Entity Backing { get; }

		/// <summary>
		/// Gets the ID.
		/// </summary>
		public int Id => Backing.whoAmI;

		/// <summary>
		/// Gets the name.
		/// </summary>
		public string Name => Backing.name;

		/// <summary>
		/// Gets or sets the position.
		/// </summary>
		public Vector2 Position
		{
			get { return Backing.position; }
			set { Backing.position = value; }
		}

		/// <summary>
		/// Gets or sets the velocity.
		/// </summary>
		public Vector2 Velocity
		{
			get { return Backing.velocity; }
			set { Backing.velocity = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Entity"/> class encapsulating the specified Terraria entity.
		/// </summary>
		/// <param name="entity">The Terraria entity.</param>
		public Entity(Terraria.Entity entity)
		{
			Backing = entity;
		}
	}
}
