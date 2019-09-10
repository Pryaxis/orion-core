using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Orion.Hooks;
using Orion.Networking.Events;
using Orion.Networking.Packets;
using Serilog;

namespace Orion.Networking {
    internal sealed class OrionNetworkService : OrionService, INetworkService {
        private readonly IList<Terraria.RemoteClient> _terrariaClients;
        private readonly IList<OrionClient> _clients;

        [ExcludeFromCodeCoverage] public override string Author => "Pryaxis";
        [ExcludeFromCodeCoverage] public override string Name => "Orion Network Service";

        public int Count => _clients.Count;

        public IClient this[int index] {
            get {
                if (index < 0 || index >= Count) {
                    throw new IndexOutOfRangeException(nameof(index));
                }

                if (_clients[index]?.Wrapped != _terrariaClients[index]) {
                    _clients[index] = new OrionClient(this, _terrariaClients[index]);
                }

                var client = _clients[index];
                Debug.Assert(client != null, $"{nameof(client)} should not be null.");
                Debug.Assert(client.Wrapped != null, $"{nameof(client.Wrapped)} should not be null.");
                return client;
            }
        }

        public HookHandlerCollection<ReceivedPacketEventArgs> ReceivedPacket { get; set; }
        public HookHandlerCollection<ReceivingPacketEventArgs> ReceivingPacket { get; set; }
        public HookHandlerCollection<SentPacketEventArgs> SentPacket { get; set; }
        public HookHandlerCollection<SendingPacketEventArgs> SendingPacket { get; set; }
        public HookHandlerCollection<ClientDisconnectedEventArgs> ClientDisconnected { get; set; }

        public OrionNetworkService() {
            _terrariaClients = Terraria.Netplay.Clients;
            _clients = new OrionClient[_terrariaClients.Count];

            OTAPI.Hooks.Net.ReceiveData = ReceiveDataHandler;
            OTAPI.Hooks.Net.SendBytes = SendBytesHandler;
            OTAPI.Hooks.Net.RemoteClient.PreReset = PreResetHandler;
        }

        protected override void Dispose(bool disposeManaged) {
            if (!disposeManaged) return;

            OTAPI.Hooks.Net.ReceiveData = null;
            OTAPI.Hooks.Net.SendBytes = null;
            OTAPI.Hooks.Net.RemoteClient.PreReset = null;
        }

        public IEnumerator<IClient> GetEnumerator() {
            for (var i = 0; i < Count; ++i) {
                yield return this[i];
            }
        }
        
        [ExcludeFromCodeCoverage]
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void BroadcastPacket(Packet packet, int excludeIndex = -1) {
            if (packet == null) throw new ArgumentNullException(nameof(packet));

            for (var i = 0; i < Terraria.Netplay.MaxConnections; ++i) {
                if (i != excludeIndex) {
                    this[i].SendPacket(packet);
                }
            }
        }

        public void BroadcastPacket(PacketType packetType, int excludeIndex = -1, string text = "", int number = 0,
                                    float number2 = 0, float number3 = 0, float number4 = 0, int number5 = 0,
                                    int number6 = 0, int number7 = 0) {
            Terraria.NetMessage.SendData((int)packetType, -1, excludeIndex,
                                         Terraria.Localization.NetworkText.FromLiteral(text), number, number2, number3,
                                         number4, number5, number6, number7);
        }


        private OTAPI.HookResult ReceiveDataHandler(Terraria.MessageBuffer buffer, ref byte packetId,
                                                    ref int readOffset, ref int start, ref int length) {
            var data = buffer.readBuffer;
            Debug.Assert(buffer.whoAmI >= 0 && buffer.whoAmI < Terraria.Netplay.MaxConnections,
                         $"{nameof(buffer.whoAmI)} should be a valid index.");
            Debug.Assert(start >= 2 && start + length <= data.Length,
                         $"{nameof(start)} and {nameof(length)} should be valid indices into {nameof(data)}.");

            // start and length need to be adjusted by two to account for the packet header's length field.
            using (var stream = new MemoryStream(data, start - 2, length + 2)) {
                var sender = this[buffer.whoAmI];
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);
                var args = new ReceivingPacketEventArgs(sender, packet);
                ReceivingPacket?.Invoke(this, args);
                if (args.Handled) return OTAPI.HookResult.Cancel;

                /*
                 * To properly deal with a dirty packet, we'll need to use a major hack. By using HelperMemoryStream,
                 * we can run an action whenever Terraria.NetMessage sets the stream to the "real" position after
                 * reading the "real" packet.
                 *
                 * This allows us to replace the buffer and restore the old position, acting as if the dirty packet
                 * was never out of the ordinary.
                 */
                if (args.IsPacketDirty) {
                    Log.Debug("[Net] Rcvd {Packet} originally from {Sender}", packet, sender.Name);
                    packet = args.Packet;

                    var targetPosition = start + length;
                    var oldBuffer = data.ToArray();
                    stream.Position = 0;

                    // Since we are faking what the client sent us, use the Client context.
                    packet.WriteToStream(stream, PacketContext.Client);

                    buffer.readerStream = new HelperMemoryStream(
                        data, targetPosition, stream.Position >= targetPosition, buffer, oldBuffer);
                    buffer.reader = new BinaryReader(buffer.readerStream);
                }

                var args2 = new ReceivedPacketEventArgs(sender, packet);
                ReceivedPacket?.Invoke(this, args2);
                Log.Verbose("[Net] Rcvd {Packet} from {Sender}", packet, sender.Name);
                return OTAPI.HookResult.Continue;
            }
        }

        private OTAPI.HookResult SendBytesHandler(ref int remoteId, ref byte[] data, ref int start, ref int length,
                                                  ref Terraria.Net.Sockets.SocketSendCallback callback,
                                                  ref object state) {
            Debug.Assert(remoteId >= 0 && remoteId < Terraria.Netplay.MaxConnections,
                         $"{nameof(remoteId)} should be a valid index.");
            Debug.Assert(start >= 0 && start + length <= data.Length,
                         $"{nameof(start)} and {nameof(length)} should be valid indices into {nameof(data)}.");

            using (var stream = new MemoryStream(data, start, length)) {
                var receiver = this[remoteId];
                var packet = Packet.ReadFromStream(stream, PacketContext.Client);
                var args = new SendingPacketEventArgs(receiver, packet);
                SendingPacket?.Invoke(this, args);
                if (args.Handled) return OTAPI.HookResult.Cancel;

                if (args.IsPacketDirty) {
                    Log.Debug("[Net] Sent {Packet} originally to {Receiver}", packet, receiver.Name);
                    packet = args.Packet;

                    var bytes = new byte[ushort.MaxValue];
                    using (var stream2 = new MemoryStream(bytes)) {
                        packet.WriteToStream(stream2, PacketContext.Server);

                        data = bytes;
                        start = 0;
                        length = (int)stream2.Position;
                    }
                }

                var args2 = new SentPacketEventArgs(receiver, packet);
                SentPacket?.Invoke(this, args2);
                Log.Verbose("[Net] Sent {Packet} to {Receiver}", packet, receiver.Name);
                return OTAPI.HookResult.Continue;
            }
        }

        private OTAPI.HookResult PreResetHandler(Terraria.RemoteClient remoteClient) {
            var client = new OrionClient(this, remoteClient);
            var args = new ClientDisconnectedEventArgs(client);
            ClientDisconnected?.Invoke(this, args);
            return OTAPI.HookResult.Continue;
        }


        private class HelperMemoryStream : MemoryStream {
            private readonly Terraria.MessageBuffer _messageBuffer;
            private readonly byte[] _oldBuffer;
            private readonly long _targetPosition;
            private bool _ignoreFirstTime;

            public override long Position {
                get => base.Position;
                set {
                    base.Position = value;

                    if (value >= _targetPosition && _ignoreFirstTime) {
                        _ignoreFirstTime = false;
                    } else if (value == _targetPosition) {
                        _messageBuffer.readBuffer = _oldBuffer;
                        _messageBuffer.readerStream = new MemoryStream(_messageBuffer.readBuffer) {
                            Position = _targetPosition
                        };
                    }
                }
            }

            public HelperMemoryStream(byte[] buffer, long targetPosition, bool ignoreFirstTime,
                                      Terraria.MessageBuffer messageBuffer, byte[] oldBuffer) : base(buffer) {
                _targetPosition = targetPosition;
                _ignoreFirstTime = ignoreFirstTime;
                _messageBuffer = messageBuffer;
                _oldBuffer = oldBuffer;
            }
        }
    }
}
