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
using JetBrains.Annotations;

namespace Orion.Entities.Impl {
    internal sealed class OrionItem : OrionEntity<Terraria.Item>, IItem {
        public override string Name {
            get => Wrapped.Name;
            set => Wrapped._nameOverride = value ?? throw new ArgumentNullException(nameof(value));
        }

        public ItemType Type => (ItemType)Wrapped.type;

        public int StackSize {
            get => Wrapped.stack;
            set => Wrapped.stack = value;
        }

        public ItemPrefix Prefix => (ItemPrefix)Wrapped.prefix;
        public IItemStats Stats { get; }

        public OrionItem([NotNull] Terraria.Item terrariaItem) : base(terrariaItem) {
            Stats = new OrionItemStats(terrariaItem);
        }

        public void SetType(ItemType type) {
            Wrapped.SetDefaults((int)type);
        }

        public void SetPrefix(ItemPrefix prefix) {
            Wrapped.Prefix((int)prefix);
        }
    }
}
