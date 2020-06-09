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
using System.Linq;
using Xunit;

namespace Orion.Core.Packets.Modules {
    public class ModuleIdTests {
        private static readonly ISet<Type> _excludedTypes =
            new HashSet<Type> { typeof(UnknownModule), typeof(IModule) };

        [Fact]
        public void Type() {
            for (var i = 0; i < 65535; ++i) {
                var id = (ModuleId)i;

                // Scan the Orion assembly for a packet type (not of type `UnknownModule`) which has the correct ID.
                var expectedType = typeof(ModuleId).Assembly.ExportedTypes
                    .Where(t => !_excludedTypes.Contains(t) && typeof(IModule).IsAssignableFrom(t))
                    .Where(t => ((IModule)Activator.CreateInstance(t)!).Id == id).FirstOrDefault()
                        ?? typeof(UnknownModule);

                Assert.Equal(expectedType, id.Type());
            }
        }
    }
}
