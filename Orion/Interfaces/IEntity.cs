﻿using Microsoft.Xna.Framework;

namespace Orion.Interfaces
{
	/// <summary>
	/// Wraps a <see cref="Terraria.Entity"/>.
	/// </summary>
	public interface IEntity
	{
		/// <summary>
		/// Gets the ID.
		/// </summary>
		int Id { get; }

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

		/// <summary>
		/// Gets the wrapped <see cref="Terraria.Entity"/>.
		/// </summary>
		Terraria.Entity WrappedEntity { get; }
	}
}
