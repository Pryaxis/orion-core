using Microsoft.Xna.Framework;

namespace Orion.Interfaces.Implementations
{
	/// <summary>
	/// Wraps a <see cref="Terraria.Entity"/>.
	/// </summary>
	public abstract class Entity : IEntity
	{
		/// <summary>
		/// Gets the ID.
		/// </summary>
		public int Id => WrappedEntity.whoAmI;

		/// <summary>
		/// Gets the name.
		/// </summary>
		public string Name => WrappedEntity.name;

		/// <summary>
		/// Gets or sets the position.
		/// </summary>
		public Vector2 Position
		{
			get { return WrappedEntity.position; }
			set { WrappedEntity.position = value; }
		}

		/// <summary>
		/// Gets or sets the velocity.
		/// </summary>
		public Vector2 Velocity
		{
			get { return WrappedEntity.velocity; }
			set { WrappedEntity.velocity = value; }
		}

		/// <summary>
		/// Gets the wrapped <see cref="Terraria.Entity"/>.
		/// </summary>
		public abstract Terraria.Entity WrappedEntity { get; }
	}
}
