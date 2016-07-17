using System;
using System.Collections.Generic;

namespace Orion.Interfaces
{
	/// <summary>
	/// Service definition: IEntityService&lt;TEntity>
	///
	/// Provides a mechanism for dealing with entities of a certain type.
	/// </summary>
	public interface IEntityService<TEntity>
		where TEntity : Terraria.Entity
	{
		/// <summary>
		/// Finds all entities matching a predicate.
		/// </summary>
		/// <param name="predicate">The predicate to match with.</param>
		/// <returns>An enumerable collection of entities matching the predicate.</returns>
		IEnumerable<TEntity> Find(Predicate<TEntity> predicate);

		/// <summary>
		/// Gets all entities.
		/// </summary>
		/// <returns>An enumerable collection of entities.</returns>
		IEnumerable<TEntity> GetAll();
	}
}
