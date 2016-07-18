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
		public Terraria.Entity TEntity { get; }

		/// <summary>
		/// Gets the name.
		/// </summary>
		public string Name => TEntity.name;

		/// <summary>
		/// Gets or sets the position.
		/// </summary>
		public Vector2 Position
		{
			get { return TEntity.position; }
			set { TEntity.position = value; }
		}

		/// <summary>
		/// Gets or sets the velocity.
		/// </summary>
		public Vector2 Velocity
		{
			get { return TEntity.velocity; }
			set { TEntity.velocity = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Entity"/> class with the specified backing Terraria entity.
		/// </summary>
		/// <param name="entity">The Terraria entity.</param>
		public Entity(Terraria.Entity entity)
		{
			TEntity = entity;
		}
	}
}