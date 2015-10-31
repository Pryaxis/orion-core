using Orion.Framework;
using OTA.DebugFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Configuration
{
    [OrionModule("Orion Configuration Module", "Nyx Studios", order: 0, Description = "Provides an automatic configuration interface for Orion modules")]
    public class OrionConfigurationModule : OrionModuleBase, IConfigurationProvider
    {
        protected readonly Dictionary<WeakReference<OrionModuleBase>, PropertyInfo> configurationRegistrations;


        public OrionConfigurationModule(Orion core)
            : base(core)
        {
            configurationRegistrations = new Dictionary<WeakReference<OrionModuleBase>, PropertyInfo>();
        }

        public void Register<TConfigurationClass>(OrionModuleBase module, Expression<Func<TConfigurationClass>> configurationPropertySelector)
            where TConfigurationClass : class, new()
        {
            Assert.Expression(() => module == null);

            MemberExpression body = configurationPropertySelector.Body as MemberExpression;
            PropertyInfo targetProperty;

            Assert.Expression(() => body == null);

            targetProperty = body.Member as PropertyInfo;

            Assert.Expression(() => body.Member == null);

            lock (Core.syncRoot)
            {
                configurationRegistrations.Add(new WeakReference<OrionModuleBase>(module), body.Member as PropertyInfo);
            }
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                configurationRegistrations.Clear();
            }
            base.Dispose(disposing);
        }
    }
}
