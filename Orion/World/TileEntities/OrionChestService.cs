using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Orion.World.TileEntities {
    internal sealed class OrionChestService : OrionService, IChestService {
        private readonly IList<Terraria.Chest> _terrariaChests;
        private readonly IList<OrionChest> _chests;
        
        [ExcludeFromCodeCoverage]
        public override string Author => "Pryaxis";

        [ExcludeFromCodeCoverage]
        public override string Name => "Orion Chest Service";

        public int Count => _chests.Count;

        public IChest this[int index] {
            get {
                if (index < 0 || index >= Count) {
                    throw new IndexOutOfRangeException(nameof(index));
                }

                /*
                 * Some chests in _terrariaChests may be null, so we need to handle this properly by also returning
                 * null.
                 */
                if (_chests[index] == null || _chests[index].Wrapped != _terrariaChests[index]) {
                    if (_terrariaChests[index] == null) {
                        return null;
                    } else {
                        _chests[index] = new OrionChest(index, _terrariaChests[index]);
                    }
                }

                var chest = _chests[index];
                Debug.Assert(chest != null, $"{nameof(chest)} should not be null.");
                Debug.Assert(chest.Wrapped != null, $"{nameof(chest.Wrapped)} should not be null.");
                return chest;
            }
        }

        public OrionChestService() {
            _terrariaChests = Terraria.Main.chest;
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
            if (GetChest(x, y) != null) {
                return null;
            }

            for (var i = 0; i < Count; ++i) {
                var terrariaChest = _terrariaChests[i];
                if (terrariaChest == null) {
                    terrariaChest = _terrariaChests[i] = new Terraria.Chest {
                        x = x,
                        y = y,
                    };

                    for (var j = 0; j < Terraria.Chest.maxItems; ++j) {
                        terrariaChest.item[j] = new Terraria.Item();
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
