using System;
using FluentAssertions;
using Orion.Networking.Events;
using Xunit;

namespace Orion.Tests.Networking.Events {
    public class ClientDisconnectedEventArgsTests {
        [Fact]
        public void Ctor_NullClient_ThrowsArgumentNullException() {
            Func<ClientDisconnectedEventArgs> func = () => new ClientDisconnectedEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
