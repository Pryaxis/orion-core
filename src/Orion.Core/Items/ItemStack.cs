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
using System.Runtime.InteropServices;
using Destructurama.Attributed;

namespace Orion.Core.Items
{
    /// <summary>
    /// Represents a stack of items.
    /// </summary>
    /// <remarks>
    /// An item stack instance fully describes an item. It is composed of an <see cref="ItemId"/>,
    /// an <see cref="ItemPrefix"/>, and the item stack size.
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public readonly struct ItemStack : IEquatable<ItemStack>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemStack"/> structure with the specified item
        /// <paramref name="id"/> and optional <paramref name="prefix"/> and <paramref name="stackSize"/>.
        /// </summary>
        /// <param name="id">The item ID.</param>
        /// <param name="prefix">The item prefix.</param>
        /// <param name="stackSize">The item stack size.</param>
        public ItemStack(ItemId id, ItemPrefix prefix = ItemPrefix.None, short stackSize = 1)
        {
            Id = id;
            StackSize = stackSize;
            Prefix = prefix;
        }

        /// <summary>
        /// Gets the item ID.
        /// </summary>
        /// <value>The item ID.</value>
        [field: FieldOffset(0)] public ItemId Id { get; }

        /// <summary>
        /// Gets the item prefix.
        /// </summary>
        /// <value>The item prefix.</value>
        [field: FieldOffset(2)] public ItemPrefix Prefix { get; }

        /// <summary>
        /// Gets the item stack size.
        /// </summary>
        /// <value>The item stack size.</value>
        [field: FieldOffset(3)] public short StackSize { get; }

        /// <summary>
        /// Gets a value indicating whether the item stack is empty.
        /// </summary>
        /// <value><see langword="true"/> if the item stack is empty; otherwise, <see langword="false"/>.</value>
        [NotLogged]
        public bool IsEmpty => Id == ItemId.None || StackSize == 0;

        /// <inheritdoc/>
        public override bool Equals(object? obj) => obj is ItemStack other && Equals(other);

        /// <inheritdoc/>
        public bool Equals(ItemStack other) =>
            IsEmpty ? other.IsEmpty : (Id == other.Id && Prefix == other.Prefix && StackSize == other.StackSize);

        /// <summary>
        /// Returns the hash code of the item stack.
        /// </summary>
        /// <returns>The hash code of the item stack.</returns>
        public override int GetHashCode() => IsEmpty ? 0 : HashCode.Combine(Id, StackSize, Prefix);

        /// <summary>
        /// Returns a string representation of the item stack.
        /// </summary>
        /// <returns>A string representation of the item stack.</returns>
        [ExcludeFromCodeCoverage]
        public override string ToString() => IsEmpty ? "<none>" : $"{Id} x{StackSize} ({Prefix})";

        /// <summary>
        /// Deconstructs the item stack.
        /// </summary>
        /// <param name="id">The item ID.</param>
        /// <param name="prefix">The item prefix.</param>
        /// <param name="stackSize">The item stack size.</param>
        public void Deconstruct(out ItemId id, out ItemPrefix prefix, out int stackSize)
        {
            id = Id;
            stackSize = StackSize;
            prefix = Prefix;
        }

        /// <summary>
        /// Returns a value indicating whether <paramref name="left"/> and <paramref name="right"/> are equal.
        /// </summary>
        /// <param name="left">The left item stack.</param>
        /// <param name="right">The right item stack.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise,
        /// <see langword="false"/>.
        /// </returns>
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
        public static bool operator !=(ItemStack left, ItemStack right) => !(left == right);
    }
}
