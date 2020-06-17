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

using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Orion.Core.Items
{
    /// <summary>
    /// Represents a stack of items in an inventory of some sort.
    /// </summary>
    /// <remarks>
    /// An item stack instance fully describes an inventory slot. It is composed of an <see cref="ItemId"/>, the item
    /// stack size, and an <see cref="ItemPrefix"/>.
    /// </remarks>
    public readonly struct ItemStack : IEquatable<ItemStack>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemStack"/> structure with the specified item
        /// <paramref name="id"/> and optional <paramref name="stackSize"/> and <paramref name="prefix"/>.
        /// </summary>
        /// <param name="id">The item ID.</param>
        /// <param name="stackSize">The item stack size.</param>
        /// <param name="prefix">The item prefix.</param>
        public ItemStack(ItemId id, int stackSize = 1, ItemPrefix prefix = ItemPrefix.None)
        {
            Id = id;
            StackSize = stackSize;
            Prefix = prefix;
        }

        /// <summary>
        /// Gets the item ID.
        /// </summary>
        /// <value>The item ID.</value>
        public ItemId Id { get; }

        /// <summary>
        /// Gets the item stack size.
        /// </summary>
        /// <value>The item stack size.</value>
        public int StackSize { get; }

        /// <summary>
        /// Gets the item prefix.
        /// </summary>
        /// <value>The item prefix.</value>
        public ItemPrefix Prefix { get; }

        /// <inheritdoc/>
        [Pure]
        public override bool Equals(object obj) => obj is ItemStack other && Equals(other);

        /// <inheritdoc/>
        [Pure]
        public bool Equals(ItemStack other) => Id == other.Id && StackSize == other.StackSize && Prefix == other.Prefix;

        /// <summary>
        /// Returns the hash code of the item stack.
        /// </summary>
        /// <returns>The hash code of the item stack.</returns>
        [Pure]
        public override int GetHashCode() => HashCode.Combine(Id, StackSize, Prefix);

        /// <summary>
        /// Returns a string representation of the item stack.
        /// </summary>
        /// <returns>A string representation of the item stack.</returns>
        [Pure, ExcludeFromCodeCoverage]
        public override string ToString() => $"{Id} x{StackSize} ({Prefix})";

        /// <summary>
        /// Returns a value indicating whether <paramref name="left"/> and <paramref name="right"/> are equal.
        /// </summary>
        /// <param name="left">The left item stack.</param>
        /// <param name="right">The right item stack.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        [Pure]
        public static bool operator ==(ItemStack left, ItemStack right) => left.Equals(right);

        /// <summary>
        /// Returns a value indicating whether <paramref name="left"/> and <paramref name="right"/> are not equal.
        /// </summary>
        /// <param name="left">The left item stack.</param>
        /// <param name="right">The right item stack.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is not equal to <paramref name="right"/>; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        [Pure]
        public static bool operator !=(ItemStack left, ItemStack right) => !(left == right);
    }
}
