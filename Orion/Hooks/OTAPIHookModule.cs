using Orion.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orion.Framework.Events;

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
        }

        public override void Run()
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //TODO: release hooks
                GamePostUpdate = null;
                GameUpdate = null;
            }

            base.Dispose(disposing);
        }
    }
}
