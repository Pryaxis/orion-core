// Copyright (c) 2019 Pryaxis & Orion Contributors
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
using Orion.Items;
using Orion.Utils;
using TerrariaItem = Terraria.Item;
using TerrariaPlayer = Terraria.Player;

namespace Orion.Players {
    internal sealed class OrionPlayerInventory : IPlayerInventory {
        private readonly TerrariaPlayer _wrapped;

        private OrionItem? _trashCan;

        public IReadOnlyArray<IItem> Main { get; }
        public IReadOnlyArray<IItem> Equips { get; }
        public IReadOnlyArray<IItem> Dyes { get; }
        public IReadOnlyArray<IItem> MiscEquips { get; }
        public IReadOnlyArray<IItem> MiscDyes { get; }
        public IReadOnlyArray<IItem> PiggyBank { get; }
        public IReadOnlyArray<IItem> Safe { get; }
        public IReadOnlyArray<IItem> DefendersForge { get; }

        public IItem TrashCan {
            get {
                if (_trashCan == null || !ReferenceEquals(_trashCan.Wrapped, _wrapped.trashItem)) {
                    return _trashCan = new OrionItem(_wrapped.trashItem);
                }

                return _trashCan;
            }
        }

        public OrionPlayerInventory(TerrariaPlayer terrariaPlayer) {
            Debug.Assert(terrariaPlayer != null, "Terraria player should not be null");

            _wrapped = terrariaPlayer;

            Main = new WrappedReadOnlyArray<OrionItem, TerrariaItem>(
                terrariaPlayer.inventory, (_, terrariaItem) => new OrionItem(terrariaItem));
            Equips = new WrappedReadOnlyArray<OrionItem, TerrariaItem>(
                terrariaPlayer.armor, (_, terrariaItem) => new OrionItem(terrariaItem));
            Dyes = new WrappedReadOnlyArray<OrionItem, TerrariaItem>(
                terrariaPlayer.dye, (_, terrariaItem) => new OrionItem(terrariaItem));
            MiscEquips = new WrappedReadOnlyArray<OrionItem, TerrariaItem>(
                terrariaPlayer.miscEquips, (_, terrariaItem) => new OrionItem(terrariaItem));
            MiscDyes = new WrappedReadOnlyArray<OrionItem, TerrariaItem>(
                terrariaPlayer.miscDyes, (_, terrariaItem) => new OrionItem(terrariaItem));
            PiggyBank = new WrappedReadOnlyArray<OrionItem, TerrariaItem>(
                terrariaPlayer.bank.item, (_, terrariaItem) => new OrionItem(terrariaItem));
            Safe = new WrappedReadOnlyArray<OrionItem, TerrariaItem>(
                terrariaPlayer.bank2.item, (_, terrariaItem) => new OrionItem(terrariaItem));
            DefendersForge = new WrappedReadOnlyArray<OrionItem, TerrariaItem>(
                terrariaPlayer.bank3.item, (_, terrariaItem) => new OrionItem(terrariaItem));
        }
    }
}
