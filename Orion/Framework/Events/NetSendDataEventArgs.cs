using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Framework.Events
{
    /// <summary>
    /// Describes an outgoing Terraria packet for the NetSendData event.
    /// </summary>
    public class NetSendDataEventArgs : OrionEventArgs
    {
        /// <summary>
        /// Gets or sets the Terraria message type (packet type)
        /// </summary>
        public int MsgType { get; set; }

        /// <summary>
        /// Gets or sets the remote client ID this message was intended
        /// for.  -1 means the message is a broadcast intended for all
        /// clients sans the IgnoreClient.
        /// </summary>
        public int RemoteClient { get; set; }

        /// <summary>
        /// Gets or sets the ignore client this message will not be sent
        /// to.
        /// </summary>
        public int IgnoreClient { get; set; }

        /// <summary>
        /// Gets the text equivalent of this packet.  This variable means
        /// different things according to the MessageType in this packet.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the Number1 field of this packet.  This variable
        /// means different things according to the MessageType in this 
        /// packet.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets the Number2 field of this packet.  This variable
        /// means different things according to the MessageType in this
        /// packet.
        /// </summary>
        public float Number2 { get; set; }

        /// <summary>
        /// Gets or sets the Number3 field of this packet.  This variable
        /// means different things according to the MessageType in this
        /// packet.
        /// </summary>
        public float Number3 { get; set; }

        /// <summary>
        /// Gets or sets the Number4 field of this packet.  This variable
        /// means different things according to the MessageType in this
        /// packet.
        /// </summary>
        public float Number4 { get; set; }

        /// <summary>
        /// Gets or sets the Number5 field of this packet.  This variable
        /// means different things according to the MessageType in this
        /// packet.
        /// </summary>
        public int Number5 { get; set; }


        public override string ToString()
        {
            return $"[SendData ID={MsgType} RemoteClient={RemoteClient} IgnoreClient={IgnoreClient}]";
        }
    }
}
