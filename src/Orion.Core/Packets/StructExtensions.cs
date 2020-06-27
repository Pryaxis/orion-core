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
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace Orion.Core.Packets
{
    /// <summary>
    /// Provides extensions for structures.
    /// </summary>
    internal static class StructExtensions
    {
        /// <summary>
        /// Reinterprets the value reference as a reference to a byte.
        /// </summary>
        /// <typeparam name="T">The type of value.</typeparam>
        /// <param name="value">The value reference.</param>
        /// <returns>The value reference as a reference to a byte.</returns>
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref byte AsByte<T>(ref this T value) where T : struct => ref Unsafe.As<T, byte>(ref value);

        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref byte AsRefByte<T>(ref this T value, int byteOffset) where T : struct =>
            ref Unsafe.As<T, byte>(ref Unsafe.AddByteOffset(ref value, new IntPtr(byteOffset)));
    }
}
