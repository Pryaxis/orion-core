using Orion.Extensions;
using Orion.Framework;
using Orion.Framework.Events;
using OTAPI.Core;
using OTAPI.Core.Debug;
using System;
using Terraria;

namespace Orion.Modules.Hooks
{
    [OrionModule("OTAPI Hook Provider", "Nyx Studios", Description = "Provides hooks from the OTAPI server to Orion")]
    public class OTAPIHookModule : OrionModuleBase, Framework.IHookProvider
    {
        public OTAPIHookModule(Orion core)
            : base(core)
        {
        }

        public event OrionEventHandler GamePostUpdate;
        public event OrionEventHandler GameUpdate;
        public event OrionEventHandler<NetSendDataEventArgs> NetSendData;
        public event OrionEventHandler<NetGetDataEventArgs> NetGetData;
        public event OrionEventHandler<DefaultsEventArgs<Terraria.Item, int>> ItemNetDefaults;
        public event OrionEventHandler<ServerChatEventArgs> ServerChat;
        public event OrionEventHandler ServerCommandThreadStarting;

        public override void Initialize()
        {
            base.Initialize();

            Assert.Expression(() => Core == null);

            OTAPI.Core.Hooks.Command.StartCommandThread = OnStartCommandProcessing;
            OTAPI.Core.Hooks.Game.PreUpdate = OnGameUpdate;
            OTAPI.Core.Hooks.Game.PostUpdate = OnGamePostUpdate;
            OTAPI.Core.Hooks.Net.SendData = OnNetSendData;
            OTAPI.Core.Hooks.Net.ReceiveData = OnReceiveData;
            OTAPI.Core.Hooks.Item.PreNetDefaults = OnItemNetDefaults;
        }

        #region On* Internals

        private HookResult OnStartCommandProcessing()
        {
            if (ServerCommandThreadStarting != null)
            {
                ServerCommandThreadStarting(Core, new OrionEventArgs());
            }

            //The new OTAPI will expect false to be returned in order to cancel the execution of vanilla code.
            return HookResult.Cancel;
        }

        //TODO reminder: this will now need to be done in orion.
        //private void OnConsoleMessageReceived(ref HookContext context, ref HookArgs.ConsoleMessageReceived argument)
        //{
        //    ServerChatEventArgs e;

        //    try
        //    {
        //        if (ServerChat != null)
        //        {
        //            ServerChat(Core, (e = new ServerChatEventArgs()
        //            {
        //                Message = argument.Message
        //            }));

        //            context.Conclude = e.Cancelled;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ProgramLog.Log(ex);
        //    }
        //}

        internal HookResult OnReceiveData
        (
            MessageBuffer buffer,
            ref byte packetId,
            ref int readOffset,
            ref int start,
            ref int length,
            ref int messageType
        )
        {
            NetGetDataEventArgs e;

            try
            {
                ArraySegment<byte> packetSegment = new ArraySegment<byte>(Netplay.Clients[buffer.whoAmI].ReadBuffer, readOffset, length);
                short packetLength = BitConverter.ToInt16(packetSegment.Array, packetSegment.Offset);
                byte type = packetSegment.Array[packetSegment.Offset + 2];

                /*
                 * Packet sanity checks
                 */
                Assert.Expression(() => Enum.IsDefined(typeof(PacketTypes), type));
                Assert.Expression(() => packetLength > 0);

                if (NetGetData != null)
                {
                    NetGetData(Core, (e = new NetGetDataEventArgs((byte)buffer.whoAmI, type, packetLength)));

                    if (e.Cancelled)
                        return HookResult.Cancel;
                }
            }
            catch (Exception ex)
            {
                this.Core.Log.LogError(LogOutputFlag.All, ex, $"Exception in {nameof(OnReceiveData)}");
            }

            return HookResult.Continue;
        }

        internal HookResult OnItemNetDefaults(Item item, ref int type)
        {
            DefaultsEventArgs<Terraria.Item, int> e;

            Assert.Expression(() => item == null);

            try
            {
                if (ItemNetDefaults != null)
                {
                    ItemNetDefaults(Core, (e = new DefaultsEventArgs<Terraria.Item, int>()
                    {
                        Object = item,
                        Info = type
                    }));

                    if (e.Cancelled)
                        return HookResult.Cancel;
                }
            }
            catch (Exception ex)
            {
                this.Core.Log.LogError(LogOutputFlag.All, ex, $"Exception in {nameof(OnItemNetDefaults)}");
            }

            return HookResult.Continue;
        }

        internal void OnGameUpdate(ref Microsoft.Xna.Framework.GameTime gameTime)
        {
            OrionEventArgs e;

            try
            {
                if (GameUpdate != null)
                {
                    GameUpdate(Core, (e = new OrionEventArgs()));
                }
            }
            catch (Exception ex)
            {
                this.Core.Log.LogError(LogOutputFlag.All, ex, $"Exception in {nameof(OnGameUpdate)}");
            }
        }

        internal void OnGamePostUpdate(ref Microsoft.Xna.Framework.GameTime gameTime)
        {
            OrionEventArgs e;

            try
            {
                if (GamePostUpdate != null)
                {
                    GamePostUpdate(Core, (e = new OrionEventArgs()));
                }
            }
            catch (Exception ex)
            {
                this.Core.Log.LogError(LogOutputFlag.All, ex, $"Exception in {nameof(GamePostUpdate)}");
            }
        }

        internal HookResult OnNetSendData
        (
            ref int bufferIndex,
            ref int msgType,
            ref int remoteClient,
            ref int ignoreClient,
            ref string text,
            ref int number,
            ref float number2,
            ref float number3,
            ref float number4,
            ref int number5,
            ref int number6,
            ref int number7
        )
        {
            NetSendDataEventArgs e;

            try
            {
                if (NetSendData != null)
                {
                    e = new NetSendDataEventArgs()
                    {
                        RemoteClient = remoteClient,
                        Text = text,
                        MsgType = msgType,
                        IgnoreClient = ignoreClient,
                        Number = number,
                        Number2 = number2,
                        Number3 = number3,
                        Number4 = number4,
                        Number5 = number5
                    };

                    NetSendData(Core, e);

                    remoteClient = e.RemoteClient;
                    text = e.Text;
                    msgType = e.MsgType;
                    ignoreClient = e.IgnoreClient;
                    number = e.Number;
                    number2 = e.Number2;
                    number3 = e.Number3;
                    number4 = e.Number4;
                    number5 = e.Number5;
                    
                    if (e.Cancelled)
                        return HookResult.Cancel;
                }
            }
            catch (Exception ex)
            {
                this.Core.Log.LogError(LogOutputFlag.All, ex, $"Exception in {nameof(OnNetSendData)}");
            }

            return HookResult.Continue;
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //release Orion hooks
                GamePostUpdate = null;
                GameUpdate = null;
                NetSendData = null;
                NetGetData = null;
                ItemNetDefaults = null;
                ServerChat = null;
                ServerCommandThreadStarting = null;

                //Remove all OTAPI hooks
                OTAPI.Core.Hooks.Net.ReceiveData = null;
                OTAPI.Core.Hooks.Net.SendData = null;
                OTAPI.Core.Hooks.Game.PostUpdate = null;
                OTAPI.Core.Hooks.Game.PreUpdate = null;
                OTAPI.Core.Hooks.Command.StartCommandThread = null;
            }

            base.Dispose(disposing);
        }
    }
}
