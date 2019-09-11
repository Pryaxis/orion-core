// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Terraria;

namespace Orion.World.TileEntities {
    internal sealed class OrionSignService : OrionService, ISignService {
        private readonly IList<Sign> _terrariaSigns;
        private readonly IList<OrionSign> _signs;

        [ExcludeFromCodeCoverage] public override string Author => "Pryaxis";

        public int Count => _signs.Count;

        public ISign this[int index] {
            get {
                if (index < 0 || index >= Count) throw new IndexOutOfRangeException();

                // Some signs in _terrariaSigns may be null, so we need to handle this properly by also returning null.
                if (_signs[index] == null || _signs[index].Wrapped != _terrariaSigns[index]) {
                    if (_terrariaSigns[index] == null) {
                        return null;
                    }

                    _signs[index] = new OrionSign(index, _terrariaSigns[index]);
                }

                var sign = _signs[index];
                Debug.Assert(sign != null, $"{nameof(sign)} should not be null.");
                Debug.Assert(sign.Wrapped != null, $"{nameof(sign.Wrapped)} should not be null.");
                return sign;
            }
        }

        public OrionSignService() {
            _terrariaSigns = Main.sign;
            _signs = new OrionSign[_terrariaSigns.Count];
        }

        public IEnumerator<ISign> GetEnumerator() {
            for (var i = 0; i < Count; ++i) {
                yield return this[i];
            }
        }

        [ExcludeFromCodeCoverage]
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public ISign AddSign(int x, int y) {
            if (GetSign(x, y) != null) return null;

            for (var i = 0; i < Count; ++i) {
                if (_terrariaSigns[i] == null) {
                    _terrariaSigns[i] = new Sign {
                        x = x,
                        y = y,
                        text = ""
                    };
                    return this[i];
                }
            }

            return null;
        }

        public ISign GetSign(int x, int y) {
            for (var i = 0; i < Count; ++i) {
                var terrariaSign = _terrariaSigns[i];
                if (terrariaSign != null && terrariaSign.x == x && terrariaSign.y == y) {
                    return this[i];
                }
            }

            return null;
        }

        public bool RemoveSign(ISign sign) {
            var maybeSign = _terrariaSigns[sign.Index];
            if (maybeSign == null || maybeSign.x != sign.X || maybeSign.y != sign.Y) {
                return false;
            }

            _terrariaSigns[sign.Index] = null;
            return true;
        }
    }
}
