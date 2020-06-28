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

using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Orion.Core.Packets.DataStructures
{
    /// <summary>
    /// Represents a set of 8 flags as a byte.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    internal struct Flags8
    {
        [FieldOffset(0)] private byte _value;

        /// <summary>
        /// Gets or sets the boolean flag at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The boolean flag at the specified <paramref name="index"/>.</returns>
        public bool this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                Debug.Assert(0 <= index && index < 8);

                return (_value & (1 << index)) != 0;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                Debug.Assert(0 <= index && index < 8);

                if (value)
                {
                    _value |= (byte)(1 << index);
                }
                else
                {
                    _value &= (byte)~(1 << index);
                }
            }
        }
    }
}
