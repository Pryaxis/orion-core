using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Orion.Entities
{
	public class OrionEntity : IOrionEntity
	{
		public int Height
		{
			get { return WrappedEntity.height; }
			set { WrappedEntity.height = value; }
		}
		
		/// <inheritdoc/>
		//public string Name
		//{
		//	get { return WrappedEntity.name; }
		//	set { WrappedEntity.name = value; }
		//}

		/// <inheritdoc/>
		public Vector2 Position
		{
			get { return WrappedEntity.position; }
			set { WrappedEntity.position = value; }
		}

		/// <inheritdoc/>
		public Vector2 Velocity
		{
			get { return WrappedEntity.velocity; }
			set { WrappedEntity.velocity = value; }
		}

		/// <inheritdoc/>
		public int Width
		{
			get { return WrappedEntity.width; }
			set { WrappedEntity.width = value; }
		}

		/// <inheritdoc/>
		public Terraria.Entity WrappedEntity { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="OrionEntity"/> class wrapping the specified Terraria Entity instance.
		/// </summary>
		/// <param name="entity">The Terraria Entity instance to wrap.</param>
		/// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
		public OrionEntity(Terraria.Entity entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			WrappedEntity = entity;
		}

		/// <inheritdoc/>
		public void Kill()
		{
			//Terraria.Entity doesn't contain a Kill() method :c
			//TODO: implement this.
			throw new NotImplementedException();
		}
	}
}
