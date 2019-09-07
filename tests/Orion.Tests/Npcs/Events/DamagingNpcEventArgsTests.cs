﻿using System;
using FluentAssertions;
using Moq;
using Orion.Npcs;
using Orion.Npcs.Events;
using Xunit;

namespace Orion.Tests.Npcs.Events {
    public class DamagingNpcEventArgsTests {
        [Fact]
        public void Ctor_NullNpc_ThrowsArgumentNullException() {
            Func<DamagingNpcEventArgs> func = () => new DamagingNpcEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetNpc_IsCorrect() {
            var npc = new Mock<INpc>().Object;
            var args = new DamagingNpcEventArgs(npc);

            args.Npc.Should().BeSameAs(npc);
        }
    }
}