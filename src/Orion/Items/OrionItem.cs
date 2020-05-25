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
using Orion.Entities;

namespace Orion.Items {
    internal sealed class OrionItem : OrionEntity<Terraria.Item>, IItem {
        public override string Name {
            get => Wrapped.Name;
            set => Wrapped._nameOverride = value ?? throw new ArgumentNullException(nameof(value));
        }

        public ItemId Id => (ItemId)Wrapped.type;

        public int StackSize {
            get => Wrapped.stack;
            set => Wrapped.stack = value;
        }

        public ItemPrefix Prefix => (ItemPrefix)Wrapped.prefix;

        public OrionItem(int itemIndex, Terraria.Item terrariaItem) : base(itemIndex, terrariaItem) { }
        public OrionItem(Terraria.Item terrariaItem) : this(-1, terrariaItem) { }

        public void SetId(ItemId id) {
            Wrapped.SetDefaults((int)id);
        }

        public void SetPrefix(ItemPrefix prefix) {
            Wrapped.Prefix((int)prefix);
        }
    }
}
