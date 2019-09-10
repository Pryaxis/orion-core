using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Orion.Networking.Events;
using Orion.Networking.Packets;
using Serilog;

namespace Orion.Networking {
    internal sealed class OrionClient : IClient {
        private readonly INetworkService _networkService;

        public int Index => Wrapped.Id;

        public bool IsConnected => Wrapped.IsConnected();

        public string Name {
            get => Wrapped.Name;
            set => Wrapped.Name = value ?? throw new ArgumentNullException(nameof(value));
        }

        internal Terraria.RemoteClient Wrapped { get; }

        public OrionClient(INetworkService networkService, Terraria.RemoteClient terrariaClient) {
            Debug.Assert(networkService != null, $"{nameof(networkService)} should not be null.");
            Debug.Assert(terrariaClient != null, $"{nameof(terrariaClient)} should not be null.");

            _networkService = networkService;
            Wrapped = terrariaClient;
        }

        public void SendPacket(Packet packet) {
            if (packet == null) throw new ArgumentNullException(nameof(packet));
            if (!IsConnected) return;

            Log.Debug("[Net] Sending {Packet} to {Receiver}", packet, Name);

            // Trigger SendingPacket manually.
            var args = new SendingPacketEventArgs(this, packet);
            _networkService.SendingPacket?.Invoke(this, args);
            if (args.Handled) return;
            packet = args.Packet;

            // Since we are sending to the client, use the Server context.
            var stream = new MemoryStream();
            packet.WriteToStream(stream, PacketContext.Server);
            Wrapped.Socket?.AsyncSend(stream.ToArray(), 0, (int)stream.Length, Wrapped.ServerWriteCallBack);

            // Trigger SentPacket manually.
            var args2 = new SentPacketEventArgs(this, packet);
            _networkService.SentPacket?.Invoke(this, args2);

            Log.Debug("[Net] Sent {Packet} to {Receiver}", packet, Name);
        }

        public void SendPacket(PacketType packetType, string text = "", int number = 0, float number2 = 0,
                               float number3 = 0, float number4 = 0, int number5 = 0, int number6 = 0,
                               int number7 = 0) {
            if (!IsConnected) return;

            Log.Debug("[Net] Sending {Packet} (\"{Text}\", {Number1}, {Number2}, {Number3}, {Number4}, {Number5}, " +
                      "{Number6}, {Number7} to {Receiver}", packetType, Name, text, number, number2, number3, number4,
                      number5, number6, number7);
            Terraria.NetMessage.SendData((int)packetType, Index, -1,
                                         Terraria.Localization.NetworkText.FromLiteral(text), number, number2, number3,
                                         number4, number5, number6, number7);
        }

        [ExcludeFromCodeCoverage]
        public override string ToString() => Name;
    }
}
