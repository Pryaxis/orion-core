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

using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using Orion.Entities;
using TerrariaNpc = Terraria.NPC;

namespace Orion.Npcs {
    internal sealed class OrionNpc : OrionEntity<TerrariaNpc>, INpc {
        public override string Name {
            get => Wrapped.GivenOrTypeName;
            set => Wrapped._givenName = value ?? throw new ArgumentNullException(nameof(value));
        }

        public NpcType Type => (NpcType)Wrapped.netID;

        public OrionNpc(TerrariaNpc terrariaNpc) : this(-1, terrariaNpc) { }
        public OrionNpc(int npcIndex, TerrariaNpc terrariaNpc) : base(npcIndex, terrariaNpc) { }

        // Not localized because this string is developer-facing.
        [Pure, ExcludeFromCodeCoverage]
        public override string ToString() => Index >= 0 ? $"#: {Index}" : "NPC instance";

        public void SetType(NpcType type) => Wrapped.SetDefaults((int)type);
    }
}
