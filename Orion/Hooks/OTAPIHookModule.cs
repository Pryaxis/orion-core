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

        public override void Initialize()
        {

            base.Initialize();

            Assert.Expression(() => Core == null);

            Core.Plugin.Hook(HookPoints.ServerUpdate, OnGameUpdate);
            Core.Plugin.Hook(HookPoints.SendNetMessage, OnNetSendData);
        }



        #region On* Internals

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
            }

            base.Dispose(disposing);
        }
    }
}
