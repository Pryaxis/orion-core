using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Orion.Networking.Events;
using Orion.Networking.Packets;
using OTAPI;

namespace Orion.Networking {
    internal sealed class OrionNetworkService : OrionService, INetworkService {
        [ExcludeFromCodeCoverage]
        public override string Author => "Pryaxis";

        [ExcludeFromCodeCoverage]
        public override string Name => "Orion Network Service";

        public event EventHandler<ReceivedPacketEventArgs> ReceivedPacket;
        public event EventHandler<ReceivingPacketEventArgs> ReceivingPacket;
        public event EventHandler<SentPacketEventArgs> SentPacket;
        public event EventHandler<SendingPacketEventArgs> SendingPacket;
        public event EventHandler<ClientDisconnectedEventArgs> ClientDisconnected;

        public OrionNetworkService() {
            Hooks.Net.ReceiveData = ReceiveDataHandler;
            Hooks.Net.SendBytes = SendBytesHandler;
            Hooks.Net.RemoteClient.PreReset = PreResetHandler;
        }

        protected override void Dispose(bool disposeManaged) {
            if (!disposeManaged) {
                return;
            }

            Hooks.Net.ReceiveData = null;
            Hooks.Net.SendBytes = null;
            Hooks.Net.RemoteClient.PreReset = null;
        }

        public void SendPacket(TerrariaPacketType packetType, int targetIndex = -1, int exceptIndex = -1,
                               string text = "", int number = 0, float number2 = 0, float number3 = 0,
                               float number4 = 0, int number5 = 0, int number6 = 0, int number7 = 0) {
            Terraria.NetMessage.SendData((int)packetType, targetIndex, exceptIndex,
                                         Terraria.Localization.NetworkText.FromLiteral(text), number, number2, number3,
                                         number4, number5, number6, number7);
        }


        private HookResult ReceiveDataHandler(Terraria.MessageBuffer buffer, ref byte packetId, ref int readOffset,
                                              ref int start, ref int length) {
            var data = buffer.readBuffer;
            Debug.Assert(buffer.whoAmI >= 0 && buffer.whoAmI < Terraria.Netplay.MaxConnections,
                         $"{nameof(buffer.whoAmI)} should be a valid index.");
            Debug.Assert(start >= 2 && start + length <= data.Length,
                         $"{nameof(start)} and {nameof(length)} should be valid indices into {nameof(data)}.");

            /*
             * start and length need to be adjusted by two to account for the packet header's length field.
             */
            using (var stream = new MemoryStream(data, start - 2, length + 2)) {
                var sender = Terraria.Netplay.Clients[buffer.whoAmI];
                var packet = TerrariaPacket.ReadFromStream(stream);
                var args = new ReceivingPacketEventArgs(sender, packet);
                ReceivingPacket?.Invoke(this, args);

                if (args.Handled) {
                    return HookResult.Cancel;
                }

                packet = args.Packet;

                /*
                 * To properly deal with a dirty packet, we'll need to use a major hack. By using HelperMemoryStream,
                 * we can run an action whenever Terraria.NetMessage sets the stream to the "real" position after
                 * reading the "real" packet.
                 *
                 * This allows us to replace the buffer and restore the old position, acting as if the dirty packet
                 * was never out of the ordinary.
                 */
                if (args.IsPacketDirty) {
                    var targetPosition = start + length;
                    var oldBuffer = data.ToArray();
                    stream.Position = 0;
                    packet.WriteToStream(stream);

                    buffer.readerStream = new HelperMemoryStream(
                        data, targetPosition, stream.Position >= targetPosition, buffer, oldBuffer);
                    buffer.reader = new BinaryReader(buffer.readerStream);
                }

                var args2 = new ReceivedPacketEventArgs(sender, packet);
                ReceivedPacket?.Invoke(this, args2);

                return HookResult.Continue;
            }
        }

        private HookResult SendBytesHandler(ref int remoteId, ref byte[] data, ref int start, ref int length,
                                            ref Terraria.Net.Sockets.SocketSendCallback callback, ref object state) {
            Debug.Assert(remoteId >= 0 && remoteId < Terraria.Netplay.MaxConnections,
                         $"{nameof(remoteId)} should be a valid index.");
            Debug.Assert(start >= 0 && start + length <= data.Length,
                         $"{nameof(start)} and {nameof(length)} should be valid indices into {nameof(data)}.");

            using (var stream = new MemoryStream(data, start, length)) {
                var receiver = Terraria.Netplay.Clients[remoteId];
                var packet = TerrariaPacket.ReadFromStream(stream);
                var args = new SendingPacketEventArgs(receiver, packet);
                SendingPacket?.Invoke(this, args);

                if (args.Handled) {
                    return HookResult.Cancel;
                }

                packet = args.Packet;
                if (args.IsPacketDirty) {
                    var bytes = new byte[ushort.MaxValue];
                    using (var stream2 = new MemoryStream(bytes)) {
                        packet.WriteToStream(stream2);

                        data = bytes;
                        start = 0;
                        length = (int)stream2.Position;
                    }
                }

                var args2 = new SentPacketEventArgs(receiver, packet);
                SentPacket?.Invoke(this, args2);

                return HookResult.Continue;
            }
        }

        private HookResult PreResetHandler(Terraria.RemoteClient remoteClient) {
            var args = new ClientDisconnectedEventArgs(remoteClient);
            ClientDisconnected?.Invoke(this, args);

            return HookResult.Continue;
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
