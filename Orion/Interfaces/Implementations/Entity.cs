using Microsoft.Xna.Framework;

namespace Orion.Interfaces.Implementations
{
	/// <summary>
	/// Wraps a Terraria entity.
	/// </summary>
	public abstract class Entity : IEntity
	{
		/// <summary>
		/// Gets the entity ID.
		/// </summary>
		public int Id => WrappedEntity.whoAmI;

		/// <summary>
		/// Gets the entity name.
		/// </summary>
		public string Name => WrappedEntity.name;

		/// <summary>
		/// Gets or sets the entity position.
		/// </summary>
		public Vector2 Position
		{
			get { return WrappedEntity.position; }
			set { WrappedEntity.position = value; }
		}

		/// <summary>
		/// Gets or sets the entity velocity.
		/// </summary>
		public Vector2 Velocity
		{
			get { return WrappedEntity.velocity; }
			set { WrappedEntity.velocity = value; }
		}

		/// <summary>
		/// Gets the wrapped Terraria entity.
		/// </summary>
		public abstract Terraria.Entity WrappedEntity { get; }
	}
}
