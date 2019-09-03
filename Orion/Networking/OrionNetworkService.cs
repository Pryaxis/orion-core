using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Orion.Framework;
using Orion.Networking.Events;
using Orion.Networking.Packets;
using OTAPI;

namespace Orion.Networking {
    /// <summary>
    /// Orion's implementation of <see cref="INetworkService"/>.
    /// </summary>
    internal sealed class OrionNetworkService : OrionService, INetworkService {
        public override string Author => "Pryaxis";
        public override string Name => "Orion Network Service";

        public event EventHandler<ReceivedPacketEventArgs> ReceivedPacket;
        public event EventHandler<ReceivingPacketEventArgs> ReceivingPacket;
        public event EventHandler<SentPacketEventArgs> SentPacket;
        public event EventHandler<SendingPacketEventArgs> SendingPacket;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrionNetworkService"/> class.
        /// </summary>
        public OrionNetworkService() {
            Hooks.Net.ReceiveData = ReceiveDataHandler;
            Hooks.Net.SendBytes = SendBytesHandler;
        }

        public void SendPacket(TerrariaPacket packet, int targetId = -1, int exceptId = -1) {
            throw new NotImplementedException();
        }

        public void SendPacket(TerrariaPacketType packetType, int targetId = -1, int exceptId = -1, string text = "",
                               int number = default, float number2 = default, float number3 = default,
                               float number4 = default, int number5 = default, int number6 = default,
                               int number7 = default) {
            Terraria.NetMessage.SendData(
                (int)packetType, targetId, exceptId,
                Terraria.Localization.NetworkText.FromLiteral(text), number, number2, number3, number4,
                number5,
                number6, number7);
        }


        private HookResult ReceiveDataHandler(Terraria.MessageBuffer buffer, ref byte packetId, ref int readOffset,
                                              ref int start, ref int length) {
            var data = buffer.readBuffer;
            Debug.Assert(buffer.whoAmI >= 0 && buffer.whoAmI < Terraria.Netplay.MaxConnections,
                         $"{nameof(buffer.whoAmI)} should be a valid index.");
            Debug.Assert(start >= 2 && start + length <= data.Length,
                         $"{nameof(start)} and {nameof(length)} should be valid indices into {nameof(data)}.");

            // start and length need to be adjusted by two to account for the packet header's length field.
            using (var stream = new MemoryStream(data, start - 2, length + 2)) {
                var sender = Terraria.Netplay.Clients[buffer.whoAmI];
                var packet = TerrariaPacket.ReadFromStream(stream);
                var receivingArgs = new ReceivingPacketEventArgs(sender, packet);
                ReceivingPacket?.Invoke(this, receivingArgs);

                if (receivingArgs.Handled) {
                    return HookResult.Cancel;
                }

                packet = receivingArgs.Packet;
                if (receivingArgs.IsPacketDirty) {
                    var targetPosition = start + length;
                    var oldBuffer = data.ToArray();
                    stream.Position = 0;
                    packet.WriteToStream(stream);

                    buffer.readerStream = new HelperMemoryStream(
                        data, targetPosition, stream.Position >= targetPosition, buffer, oldBuffer);
                    buffer.reader = new BinaryReader(buffer.readerStream);
                }

                var receivedArgs = new ReceivedPacketEventArgs(sender, packet);
                ReceivedPacket?.Invoke(this, receivedArgs);

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
                var sendingArgs = new SendingPacketEventArgs(receiver, packet);
                SendingPacket?.Invoke(this, sendingArgs);

                if (sendingArgs.Handled) {
                    return HookResult.Cancel;
                }

                packet = sendingArgs.Packet;
                if (sendingArgs.IsPacketDirty) {
                    var bytes = new byte[ushort.MaxValue];
                    using (var stream2 = new MemoryStream(bytes)) {
                        packet.WriteToStream(stream2);

                        data = bytes;
                        start = 0;
                        length = (int)stream2.Position;
                    }
                }

                var sentArgs = new SentPacketEventArgs(receiver, packet);
                SentPacket?.Invoke(this, sentArgs);

                return HookResult.Continue;
            }
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

            public HelperMemoryStream(
                byte[] buffer, long targetPosition, bool ignoreFirstTime, Terraria.MessageBuffer messageBuffer,
                byte[] oldBuffer)
                : base(buffer) {

                _targetPosition = targetPosition;
                _ignoreFirstTime = ignoreFirstTime;
                _messageBuffer = messageBuffer;
                _oldBuffer = oldBuffer;
            }
        }
    }
}
