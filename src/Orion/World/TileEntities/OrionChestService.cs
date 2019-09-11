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
    internal sealed class OrionChestService : OrionService, IChestService {
        private readonly IList<Chest> _terrariaChests;
        private readonly IList<OrionChest> _chests;

        [ExcludeFromCodeCoverage] public override string Author => "Pryaxis";

        public int Count => _chests.Count;

        public IChest this[int index] {
            get {
                if (index < 0 || index >= Count) throw new IndexOutOfRangeException();

                // Some chests in _terrariaChests may be null, so we need to handle this properly by also returning
                // null.
                if (_chests[index] == null || _chests[index].Wrapped != _terrariaChests[index]) {
                    if (_terrariaChests[index] == null) {
                        return null;
                    }

                    _chests[index] = new OrionChest(index, _terrariaChests[index]);
                }

                var chest = _chests[index];
                Debug.Assert(chest != null, $"{nameof(chest)} should not be null.");
                Debug.Assert(chest.Wrapped != null, $"{nameof(chest.Wrapped)} should not be null.");
                return chest;
            }
        }

        public OrionChestService() {
            _terrariaChests = Main.chest;
            _chests = new OrionChest[_terrariaChests.Count];
        }

        public IEnumerator<IChest> GetEnumerator() {
            for (var i = 0; i < Count; ++i) {
                yield return this[i];
            }
        }

        [ExcludeFromCodeCoverage]
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IChest AddChest(int x, int y) {
            if (GetChest(x, y) != null) return null;

            for (var i = 0; i < Count; ++i) {
                var terrariaChest = _terrariaChests[i];
                if (terrariaChest == null) {
                    terrariaChest = _terrariaChests[i] = new Chest {
                        x = x,
                        y = y
                    };

                    for (var j = 0; j < Chest.maxItems; ++j) {
                        terrariaChest.item[j] = new Item();
                    }

                    return this[i];
                }
            }

            return null;
        }

        public IChest GetChest(int x, int y) {
            for (var i = 0; i < Count; ++i) {
                var terrariaChest = _terrariaChests[i];
                if (terrariaChest != null && terrariaChest.x == x && terrariaChest.y == y) {
                    return this[i];
                }
            }

            return null;
        }

        public bool RemoveChest(IChest chest) {
            var maybeChest = _terrariaChests[chest.Index];
            if (maybeChest == null || maybeChest.x != chest.X || maybeChest.y != chest.Y) {
                return false;
            }

            _terrariaChests[chest.Index] = null;
            return true;
        }
    }
}
