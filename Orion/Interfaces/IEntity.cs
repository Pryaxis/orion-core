using Microsoft.Xna.Framework;

namespace Orion.Interfaces
{
	/// <summary>
	/// Represents a Terraria entity.
	/// </summary>
	public interface IEntity
	{
		/// <summary>
		/// Gets the backing Terraria entity.
		/// </summary>
		Terraria.Entity TEntity { get; }

		/// <summary>
		/// Gets the name.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets or sets the position.
		/// </summary>
		Vector2 Position { get; set; }

		/// <summary>
		/// Gets or sets the velocity.
		/// </summary>
		Vector2 Velocity { get; set; }
	}
}
