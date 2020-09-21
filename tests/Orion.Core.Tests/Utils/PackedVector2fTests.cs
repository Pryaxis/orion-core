using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Xunit;

namespace Orion.Core.Utils
{
    public sealed class PackedVector2fTests
    {
        [Fact]
        public void Default_Get()
        {
            Assert.Equal((uint) 0, default(PackedVector2f).PackedValue);
            Assert.Equal(Vector2f.Zero, default(PackedVector2f).Vector2);
        }

        [Fact]
        public void Vector2_Set_Get()
        {
            var packedVector = new PackedVector2f();
            
            packedVector.Vector2 = new Vector2f(2, 2);

            Assert.Equal(new Vector2f(2, 2), packedVector.Vector2);
            Assert.Equal((uint)1073758208, packedVector.PackedValue);
        }

        [Fact]
        public void Equals_ReturnsTrue()
        {
            var leftVector = new PackedVector2f(1, 2);
            var rightVector = new PackedVector2f(1, 2);

            Assert.True(leftVector.Equals(rightVector));
        }
    }
}
