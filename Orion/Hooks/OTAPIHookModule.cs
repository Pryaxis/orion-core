using Orion.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orion.Framework.Events;
using OTA.Plugin;
using OTA.Logging;
using OTA.DebugFramework;
using Terraria;

namespace Orion.Hooks
{
    [OrionModule("OTAPI Hook Provider", "Nyx Studios", 0, Description = "Provides hooks from the OTAPI server to Orion")]
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

            Core.Plugin.Hook(HookPoints.ServerUpdate, OnGameUpdate);
            Core.Plugin.Hook(HookPoints.SendNetMessage, OnNetSendData);
            Core.Plugin.Hook(HookPoints.ItemNetDefaults, OnItemNetDefaults);
            Core.Plugin.Hook(HookPoints.ReceiveNetMessage, OnNetGetData);
            Core.Plugin.Hook(HookPoints.ConsoleMessageReceived, OnConsoleMessageReceived);
            Core.Plugin.Hook(HookPoints.StartCommandProcessing, OnStartCommandProcessing);
        }

        #region On* Internals

        private void OnStartCommandProcessing(ref HookContext context, ref HookArgs.StartCommandProcessing argument)
        {
            context.SetResult(HookResult.IGNORE);

            if (ServerCommandThreadStarting != null)
            {
                ServerCommandThreadStarting(Core, new OrionEventArgs());
            }
        }

        private void OnConsoleMessageReceived(ref HookContext context, ref HookArgs.ConsoleMessageReceived argument)
        {
            ServerChatEventArgs e;

            try
            {
                if (ServerChat != null)
                {
                    ServerChat(Core, (e = new ServerChatEventArgs()
                    {
                        Message = argument.Message
                    }));

                    context.Conclude = e.Cancelled;
                }
            }
            catch (Exception ex)
            {
                ProgramLog.Log(ex);
            }
        }

        internal void OnNetGetData(ref HookContext context, ref HookArgs.ReceiveNetMessage argument)
        {
            HookArgs.ReceiveNetMessage msg = argument; //causes a copy
            NetGetDataEventArgs e;

            try
            {
                ArraySegment<byte> packetSegment = new ArraySegment<byte>(Netplay.Clients[argument.BufferId].ReadBuffer, argument.Start, argument.Length);
                short packetLength = BitConverter.ToInt16(packetSegment.Array, packetSegment.Offset);
                byte type = packetSegment.Array[packetSegment.Offset + 2];

                /*
                 * Packet sanity checks
                 */
                Assert.Expression(() => Enum.IsDefined(typeof(PacketTypes), type));
                Assert.Expression(() => packetLength > 0);

                if (NetGetData != null)
                {
                    NetGetData(Core, (e = new NetGetDataEventArgs((byte)argument.BufferId, type, packetLength)));
                    context.Conclude = e.Cancelled;
                }
            }
            catch (Exception ex)
            {
                ProgramLog.Log(ex);
            }
        }

        internal void OnItemNetDefaults(ref HookContext context, ref HookArgs.ItemNetDefaults argument)
        {
            DefaultsEventArgs<Terraria.Item, int> e;
            HookArgs.ItemNetDefaults defaults = argument; // causes a copy

            Assert.Expression(() => defaults.Item == null);

            try
            {
                if (ItemNetDefaults != null)
                {
                    ItemNetDefaults(Core, (e = new DefaultsEventArgs<Terraria.Item, int>()
                    {
                        Object = argument.Item,
                        Info = argument.Type
                    }));

                    context.Conclude = e.Cancelled;
                }
            }
            catch (Exception ex)
            {
                ProgramLog.Log(ex);
            }
        }

        internal void OnGameUpdate(ref HookContext context, ref HookArgs.ServerUpdate args)
        {
            OrionEventArgs e;

            try
            {
                if (GameUpdate != null && args.State == MethodState.Begin)
                {
                    GameUpdate(Core, (e = new OrionEventArgs()));
                    context.Conclude = e.Cancelled;
                }
                else if (GamePostUpdate != null && args.State == MethodState.End)
                {
                    GameUpdate(Core, (e = new OrionEventArgs()));
                    context.Conclude = e.Cancelled;
                }
            }
            catch (Exception ex)
            {
                ProgramLog.Log(ex);
            }
        }

        internal void OnNetSendData(ref HookContext context, ref HookArgs.SendNetMessage args)
        {
            NetSendDataEventArgs e;

            try
            {
                if (NetSendData != null)
                {
                    e = new NetSendDataEventArgs()
                    {
                        RemoteClient = args.RemoteClient,
                        Text = args.Text,
                        MsgType = args.MsgType,
                        IgnoreClient = args.IgnoreClient,
                        Number = args.Number,
                        Number2 = args.Number2,
                        Number3 = args.Number3,
                        Number4 = args.Number4,
                        Number5 = args.Number5
                    };

                    NetSendData(Core, e);

                    args.RemoteClient = e.RemoteClient;
                    args.Text = e.Text;
                    args.MsgType = e.MsgType;
                    args.IgnoreClient = e.IgnoreClient;
                    args.Number = e.Number;
                    args.Number2 = e.Number2;
                    args.Number3 = e.Number3;
                    args.Number4 = e.Number4;
                    args.Number5 = e.Number5;

                    context.Conclude = e.Cancelled;
                }
            }
            catch (Exception ex)
            {
                ProgramLog.Log(ex);
            }
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

                //Remove all OTAPI callbacks and hooks
                Core.Plugin.Unhook(HookPoints.ServerUpdate);
                Core.Plugin.Unhook(HookPoints.SendNetMessage);
                Core.Plugin.Unhook(HookPoints.ItemNetDefaults);
                Core.Plugin.Unhook(HookPoints.ReceiveNetMessage);
                Core.Plugin.Unhook(HookPoints.ConsoleMessageReceived);
                Core.Plugin.Unhook(HookPoints.StartCommandProcessing);
            }

            base.Dispose(disposing);
        }
    }
}
