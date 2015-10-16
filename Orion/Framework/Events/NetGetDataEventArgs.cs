using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace Orion.Framework.Events
{
    public class NetGetDataEventArgs : OrionEventArgs
    {
        public byte ClientID { get; set; }

        public byte MessageID { get; set; }

        public ArraySegment<byte> BufferSegment { get; set; }

        public int PacketStart => BufferSegment.Offset;

        public int PacketLength => BufferSegment.Count;

        public NetGetDataEventArgs(byte clientId, int start, int length)
        {
            this.BufferSegment = new ArraySegment<byte>(Netplay.Clients[clientId].ReadBuffer, start, length);
            this.ClientID = clientId;
            this.MessageID = BufferSegment.Array[BufferSegment.Offset + 2];
        }

        /// <summary>
        /// Gets a BinaryReader that is capable of reading the binary data that is
        /// the contents of the packet.
        /// </summary>
        /// <returns></returns>
        public BinaryReader CreateBinaryReader()
        {
            return new BinaryReader(new MemoryStream(BufferSegment.Array, BufferSegment.Offset, BufferSegment.Count, writable: false));
        }
    }
}
