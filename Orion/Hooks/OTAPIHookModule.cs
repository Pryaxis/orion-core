using Orion.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orion.Framework.Events;
using OTA.Plugin;
using OTA.Logging;

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

        public override void Initialize()
        {
            base.Initialize();

            OTA.Callbacks.MainCallback.UpdateServer += MainCallback_UpdateServer;
        }

        #region On* Internals

        internal void OnGameUpdate()
        {
            if (GameUpdate == null)
            {
                return;
            }

            try
            {
                GameUpdate(Core, new OrionEventArgs());
            }
            catch (Exception ex)
            {
                ProgramLog.Log(ex);
            }
        }

        #endregion

        #region OTA Callbacks and Hookpoints

        private void MainCallback_UpdateServer(object sender, EventArgs e)
        {
            OnGameUpdate();
        }

        #endregion

        public override void Run()
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //release Orion hooks
                GamePostUpdate = null;
                GameUpdate = null;

                //Remove all OTAPI callbacks and hooks
                OTA.Callbacks.MainCallback.UpdateServer -= MainCallback_UpdateServer;
            }

            base.Dispose(disposing);
        }
    }
}
