using Microsoft.Xna.Framework;

namespace Orion.Interfaces
{
	/// <summary>
	/// Provides a wrapper around a Terraria entity.
	/// </summary>
	public interface IEntity
	{
		/// <summary>
		/// Gets the entity ID.
		/// </summary>
		int Id { get; }

		/// <summary>
		/// Gets the entity name.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets or sets the entity position.
		/// </summary>
		Vector2 Position { get; set; }

		/// <summary>
		/// Gets or sets the entity velocity.
		/// </summary>
		Vector2 Velocity { get; set; }

		/// <summary>
		/// Gets the wrapped Terraria entity.
		/// </summary>
		Terraria.Entity WrappedEntity { get; }
	}
}
