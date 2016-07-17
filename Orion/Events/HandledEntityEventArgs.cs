namespace Orion.Events
{
	/// <summary>
	/// Provides data for all entity-related events that can be handled.
	/// </summary>
	public class HandledEntityEventArgs<TEntity> : EntityEventArgs<TEntity>
		where TEntity : Terraria.Entity
	{
		/// <summary>
		/// Gets or sets a value indicating whether the event is handled. This indicates that the server should no
		/// longer process the event.
		/// </summary>
		public bool Handled { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="HandledEntityEventArgs{TEntity}"/> class with the specified
		/// entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public HandledEntityEventArgs(TEntity entity) : base(entity)
		{
		}
	}
}
