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
using System.Runtime.CompilerServices;

namespace Orion.Packets {
    internal static class StructExtensions {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref byte AsRefByte<T>(ref this T value, int byteOffset) where T : struct =>
            ref Unsafe.As<T, byte>(ref Unsafe.AddByteOffset(ref value, new IntPtr(byteOffset)));
    }
}
