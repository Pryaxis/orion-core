using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Orion.Framework;

namespace Orion.World.Chests {
    /// <summary>
    /// Orion's implementation of <see cref="IChestService"/>.
    /// </summary>
    internal sealed class OrionChestService : OrionService, IChestService {
        private readonly IList<Terraria.Chest> _terrariaChests;
        private readonly IList<OrionChest> _chests;

        public override string Author => "Pryaxis";
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
                if (_chests[index] == null || _chests[index].WrappedChest != _terrariaChests[index]) {
                    if (_terrariaChests[index] == null) {
                        return null;
                    } else {
                        _chests[index] = new OrionChest(_terrariaChests[index]);
                    }
                }

                var chest = _chests[index];
                Debug.Assert(chest != null, $"{nameof(chest)} should not be null.");
                Debug.Assert(chest.WrappedChest != null, $"{nameof(chest.WrappedChest)} should not be null.");
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

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
