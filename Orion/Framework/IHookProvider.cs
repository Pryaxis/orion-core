using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Framework
{
    /// <summary>
    /// Defines an Orion hook provider.  All Orion hooks are featured here.
    /// </summary>
    public interface IHookProvider
    {
        /// <summary>
        /// Occurs each time the game loop starts
        /// </summary>
        event Events.OrionEventHandler GameUpdate;

        /// <summary>
        /// Occurs after the game loop finishes
        /// </summary>
        event Events.OrionEventHandler GamePostUpdate;

        /// <summary>
        /// Occurs when the server will send some data to clients
        /// </summary>
        event Events.OrionEventHandler<Events.NetSendDataEventArgs> NetSendData;

        /// <summary>
        /// Occurs when the client has sent the server some data.
        /// </summary>
        event Events.OrionEventHandler<Events.NetGetDataEventArgs> NetGetData;
        
        /// <summary>
        /// Occurs when ItemNetDefaults has been called on a Terraria item.
        /// </summary>
        event Events.OrionEventHandler<Events.DefaultsEventArgs<Terraria.Item, int>> ItemNetDefaults;
        
        /// <summary>
        /// Occurs when the server has sent a chat message.
        /// </summary>
        event Events.OrionEventHandler<Events.ServerChatEventArgs> ServerChat;
        
        /// <summary>
        /// Occurs when the dedicated server input thread is about to be invoked
        /// inside the Terraria process.
        /// </summary>
        event Events.OrionEventHandler ServerCommandThreadStarting;

    }
}
