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
using System.Collections.Generic;
using System.Text;
using Destructurama.Attributed;
using Orion.Entities;

namespace Orion.Npcs {
    [LogAsScalar]
    internal sealed class OrionNpc : OrionEntity<Terraria.NPC>, INpc {
        public override string Name {
            get => Wrapped.GivenOrTypeName;
            set => Wrapped._givenName = value ?? throw new ArgumentNullException(nameof(value));
        }

        public NpcId Id => (NpcId)Wrapped.netID;

        public OrionNpc(int npcIndex, Terraria.NPC terrariaNpc) : base(npcIndex, terrariaNpc) { }
        public OrionNpc(Terraria.NPC terrariaNpc) : this(-1, terrariaNpc) { }

        public void SetId(NpcId id) {
            Wrapped.SetDefaults((int)id, Wrapped.GetMatchingSpawnParams());
        }
    }
}
