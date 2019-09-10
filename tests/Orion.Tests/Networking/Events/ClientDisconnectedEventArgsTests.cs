using System;
using FluentAssertions;
using Moq;
using Orion.Networking;
using Orion.Networking.Events;
using Xunit;

namespace Orion.Tests.Networking.Events {
    public class ClientDisconnectedEventArgsTests {
        [Fact]
        public void Ctor_NullClient_ThrowsArgumentNullException() {
            Func<ClientDisconnectedEventArgs> func = () => new ClientDisconnectedEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetClient_IsCorrect() {
            var client = new Mock<IClient>().Object;
            var args = new ClientDisconnectedEventArgs(client);

            args.Client.Should().BeSameAs(client);
        }
    }
}
