using Microsoft.Xna.Framework;

namespace Orion.Interfaces
{
	/// <summary>
	/// Encapsulates a Terraria entity.
	/// </summary>
	public interface IEntity
	{
		/// <summary>
		/// Gets the backing Terraria entity.
		/// </summary>
		Terraria.Entity Backing { get; }

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
