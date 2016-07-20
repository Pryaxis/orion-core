using Microsoft.Xna.Framework;
using NUnit.Framework;
using Orion.Interfaces.Implementations;

namespace Orion.Tests.Interfaces.Implementations
{
	[TestFixture]
	public abstract class EntityTests
	{
		protected abstract void GetEntities(out Terraria.Entity terrariaEntity, out Entity entity);

		[Test]
		public void GetId_IsCorrect()
		{
			Terraria.Entity terrariaEntity;
			Entity entity;
			GetEntities(out terrariaEntity, out entity);

			terrariaEntity.whoAmI = 1;

			Assert.AreEqual(1, entity.Id);
		}

		[Test]
		public void GetName_IsCorrect()
		{
			Terraria.Entity terrariaEntity;
			Entity entity;
			GetEntities(out terrariaEntity, out entity);

			terrariaEntity.name = "Name";

			Assert.AreEqual("Name", entity.Name);
		}

		[Test]
		public void GetPosition_IsCorrect()
		{
			Terraria.Entity terrariaEntity;
			Entity entity;
			GetEntities(out terrariaEntity, out entity);

			terrariaEntity.position = Vector2.One;

			Assert.AreEqual(Vector2.One, entity.Position);
		}

		[Test]
		public void SetPosition_Updates()
		{
			Terraria.Entity terrariaEntity;
			Entity entity;
			GetEntities(out terrariaEntity, out entity);

			entity.Position = Vector2.One;

			Assert.AreEqual(Vector2.One, terrariaEntity.position);
		}

		[Test]
		public void GetVelocity_IsCorrect()
		{
			Terraria.Entity terrariaEntity;
			Entity entity;
			GetEntities(out terrariaEntity, out entity);

			terrariaEntity.velocity = Vector2.One;

			Assert.AreEqual(Vector2.One, entity.Velocity);
		}

		[Test]
		public void SetVelocity_Updates()
		{
			Terraria.Entity terrariaEntity;
			Entity entity;
			GetEntities(out terrariaEntity, out entity);

			entity.Velocity = Vector2.One;

			Assert.AreEqual(Vector2.One, terrariaEntity.velocity);
		}
	}
}
