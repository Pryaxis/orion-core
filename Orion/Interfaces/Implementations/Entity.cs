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
		public Terraria.Entity BackingEntity { get; }

		/// <summary>
		/// Gets the name.
		/// </summary>
		public string Name => BackingEntity.name;

		/// <summary>
		/// Gets or sets the position.
		/// </summary>
		public Vector2 Position
		{
			get { return BackingEntity.position; }
			set { BackingEntity.position = value; }
		}

		/// <summary>
		/// Gets or sets the velocity.
		/// </summary>
		public Vector2 Velocity
		{
			get { return BackingEntity.velocity; }
			set { BackingEntity.velocity = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Entity"/> class with the specified backing Terraria entity.
		/// </summary>
		/// <param name="backingEntity">The backing Terraria entity.</param>
		public Entity(Terraria.Entity backingEntity)
		{
			BackingEntity = backingEntity;
		}
	}
}
