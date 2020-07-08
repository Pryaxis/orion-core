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

namespace Orion.Core.World.Tiles
{
    public class LiquidTests
    {
        [Fact]
        public void Type_Get()
        {
            var liquid = new Liquid(LiquidType.Water, 100);

            Assert.Equal(LiquidType.Water, liquid.Type);
        }

        [Fact]
        public void Amount_Get()
        {
            var liquid = new Liquid(LiquidType.Water, 100);

            Assert.Equal(100, liquid.Amount);
        }

        [Fact]
        public void IsEmpty_Get_ReturnsTrue()
        {
            var liquid = new Liquid(LiquidType.Water, 0);

            Assert.True(liquid.IsEmpty);
        }

        [Fact]
        public void IsEmpty_Get_ReturnsFalse()
        {
            var liquid = new Liquid(LiquidType.Water, 100);

            Assert.False(liquid.IsEmpty);
        }

        [Fact]
        public void Equals_ReturnsTrue()
        {
            var liquid = new Liquid(LiquidType.Water, 255);
            var liquid2 = new Liquid(LiquidType.Water, 255);

            Assert.True(liquid.Equals((object)liquid2));
            Assert.True(liquid.Equals(liquid2));
        }

        [Fact]
        public void Equals_AreEmpty_ReturnsTrue()
        {
            var liquid = new Liquid(LiquidType.Water, 0);
            var liquid2 = new Liquid(LiquidType.Lava, 0);

            Assert.True(liquid.Equals((object)liquid2));
            Assert.True(liquid.Equals(liquid2));
        }

        [Fact]
        public void Equals_WrongType_ReturnsFalse()
        {
            var liquid = new Liquid(LiquidType.Water, 255);

            Assert.False(liquid.Equals(0));
        }

        [Fact]
        public void Equals_DifferentType_ReturnsFalse()
        {
            var liquid = new Liquid(LiquidType.Water, 255);
            var liquid2 = new Liquid(LiquidType.Lava, 255);

            Assert.False(liquid.Equals((object)liquid2));
            Assert.False(liquid.Equals(liquid2));
        }

        [Fact]
        public void Equals_DifferentAmount_ReturnsFalse()
        {
            var liquid = new Liquid(LiquidType.Water, 255);
            var liquid2 = new Liquid(LiquidType.Water, 100);

            Assert.False(liquid.Equals((object)liquid2));
            Assert.False(liquid.Equals(liquid2));
        }

        [Fact]
        public void GetHashCode_Equals_AreEqual()
        {
            var liquid = new Liquid(LiquidType.Water, 255);
            var liquid2 = new Liquid(LiquidType.Water, 255);

            Assert.Equal(liquid.GetHashCode(), liquid2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_Equals_AreEmpty_AreEqual()
        {
            var liquid = new Liquid(LiquidType.Water, 0);
            var liquid2 = new Liquid(LiquidType.Lava, 0);

            Assert.Equal(liquid.GetHashCode(), liquid2.GetHashCode());
        }

        [Fact]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Operator name")]
        public void op_Equality_ReturnsTrue()
        {
            var liquid = new Liquid(LiquidType.Water, 255);
            var liquid2 = new Liquid(LiquidType.Water, 255);
            var liquid3 = new Liquid(LiquidType.Water, 0);
            var liquid4 = new Liquid(LiquidType.Lava, 0);

            Assert.True(liquid == liquid2);
            Assert.True(liquid3 == liquid4);
        }

        [Fact]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Operator name")]
        public void op_Equality_ReturnsFalse()
        {
            var liquid = new Liquid(LiquidType.Water, 255);
            var liquid2 = new Liquid(LiquidType.Water, 100);
            var liquid3 = new Liquid(LiquidType.Lava, 255);

            Assert.False(liquid == liquid2);
            Assert.False(liquid == liquid3);
        }

        [Fact]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Operator name")]
        public void op_Inequality_ReturnsTrue()
        {
            var liquid = new Liquid(LiquidType.Water, 255);
            var liquid2 = new Liquid(LiquidType.Water, 100);
            var liquid3 = new Liquid(LiquidType.Lava, 255);

            Assert.True(liquid != liquid2);
            Assert.True(liquid != liquid3);
        }

        [Fact]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Operator name")]
        public void op_Inequality_ReturnsFalse()
        {
            var liquid = new Liquid(LiquidType.Water, 255);
            var liquid2 = new Liquid(LiquidType.Water, 255);
            var liquid3 = new Liquid(LiquidType.Water, 0);
            var liquid4 = new Liquid(LiquidType.Lava, 0);

            Assert.False(liquid != liquid2);
            Assert.False(liquid3 != liquid4);
        }
    }
}
