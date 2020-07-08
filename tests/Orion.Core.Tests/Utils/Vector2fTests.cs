// Copyright (c) 2020 Pryaxis & Orion Contributors
// 
// This file is part of Orion.
// 
// Orion is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Orion is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Orion.  If not, see <https://www.gnu.org/licenses/>.

using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Orion.Core.Utils
{
    public class Vector2fTests
    {
        [Fact]
        public void Cctor()
        {
            _ = Vector2f.Zero;
        }

        [Fact]
        public void X_Get()
        {
            var vector = new Vector2f(1.23f, 4.56f);

            Assert.Equal(1.23f, vector.X);
        }

        [Fact]
        public void Y_Get()
        {
            var vector = new Vector2f(1.23f, 4.56f);

            Assert.Equal(4.56f, vector.Y);
        }


        [Fact]
        public void Equals_ReturnsTrue()
        {
            var vector = new Vector2f(1.23f, 4.56f);
            var vector2 = new Vector2f(1.23f, 4.56f);

            Assert.True(vector.Equals((object)vector2));
            Assert.True(vector.Equals(vector2));
        }

        [Fact]
        public void Equals_WrongType_ReturnsFalse()
        {
            var vector = new Vector2f(1.23f, 4.56f);

            Assert.False(vector.Equals(0));
        }

        [Fact]
        public void Equals_XNotEqual_ReturnsFalse()
        {
            var vector = new Vector2f(1.23f, 4.56f);
            var vector2 = new Vector2f(0, 4.56f);

            Assert.False(vector.Equals((object)vector2));
            Assert.False(vector.Equals(vector2));
        }

        [Fact]
        public void Equals_YNotEqual_ReturnsFalse()
        {
            var vector = new Vector2f(1.23f, 4.56f);
            var vector2 = new Vector2f(1.23f, 0);

            Assert.False(vector.Equals((object)vector2));
            Assert.False(vector.Equals(vector2));
        }

        [Fact]
        public void GetHashCode_Equals_AreEqual()
        {
            var vector = new Vector2f(1.23f, 4.56f);
            var vector2 = new Vector2f(1.23f, 4.56f);

            Assert.Equal(vector.GetHashCode(), vector2.GetHashCode());
        }

        [Fact]
        public void Deconstruct()
        {
            var vector = new Vector2f(1.23f, 4.56f);

            var (x, y) = vector;

            Assert.Equal(1.23f, x);
            Assert.Equal(4.56f, y);
        }

        [Fact]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Operator name")]
        public void op_Equality_ReturnsTrue()
        {
            var vector = new Vector2f(1.23f, 4.56f);
            var vector2 = new Vector2f(1.23f, 4.56f);

            Assert.True(vector == vector2);
        }

        [Fact]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Operator name")]
        public void op_Equality_ReturnsFalse()
        {
            var vector = new Vector2f(1.23f, 4.56f);
            var vector2 = new Vector2f(1.23f, 0);
            var vector3 = new Vector2f(0, 4.56f);

            Assert.False(vector == vector2);
            Assert.False(vector == vector3);
        }

        [Fact]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Operator name")]
        public void op_Inequality_ReturnsTrue()
        {
            var vector = new Vector2f(1.23f, 4.56f);
            var vector2 = new Vector2f(1.23f, 0);
            var vector3 = new Vector2f(0, 4.56f);

            Assert.True(vector != vector2);
            Assert.True(vector != vector3);
        }

        [Fact]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Operator name")]
        public void op_Inequality_ReturnsFalse()
        {
            var vector = new Vector2f(1.23f, 4.56f);
            var vector2 = new Vector2f(1.23f, 4.56f);

            Assert.False(vector != vector2);
        }

        [Fact]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Operator name")]
        public void op_Addition()
        {
            var vector = new Vector2f(1f, 2f);
            var vector2 = new Vector2f(3f, 5f);

            Assert.Equal(new Vector2f(4f, 7f), vector + vector2);
        }

        [Fact]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Operator name")]
        public void op_Subtraction()
        {
            var vector = new Vector2f(1f, 2f);
            var vector2 = new Vector2f(3f, 5f);

            Assert.Equal(new Vector2f(-2f, -3f), vector - vector2);
        }

        [Fact]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Operator name")]
        public void op_Multiplication()
        {
            var vector = new Vector2f(1f, 2f);

            Assert.Equal(new Vector2f(2f, 4f), 2 * vector);
        }

        [Fact]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Operator name")]
        public void op_Division()
        {
            var vector = new Vector2f(2f, 4f);

            Assert.Equal(new Vector2f(1f, 2f), vector / 2);
        }
    }
}
