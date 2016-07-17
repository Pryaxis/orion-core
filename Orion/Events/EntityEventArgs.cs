using System;

namespace Orion.Events
{
	/// <summary>
	/// Provides data for all entity-related events.
	/// </summary>
	public class EntityEventArgs<TEntity> : EventArgs
		where TEntity : Terraria.Entity
	{
		/// <summary>
		/// Gets the relevant entity.
		/// </summary>
		public TEntity Entity { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="EntityEventArgs{TEntity}"/> class with the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public EntityEventArgs(TEntity entity)
		{
			Entity = entity;
		}
	}
}
