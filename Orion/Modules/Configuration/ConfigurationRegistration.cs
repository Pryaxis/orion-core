

using System;
using System.Reflection;
using Orion.Framework;
using OTA.DebugFramework;
using OTA.Logging;

namespace Orion.Modules.Configuration
{

    /// <summary>
    /// Houses an Orion module's registration into the Orion automatic configuration
    /// system, and provides methods for updating the registered property on the Orion
    /// module that registered it.
    /// </summary>
    public class ConfigurationRegistration
    {
        protected WeakReference weakRef;

        public Type ModuleType { get; protected set; }

        public Type ConfigurationPropertyType => ConfigurationProperty.PropertyType;

        public PropertyInfo ConfigurationProperty { get; protected set; }

        public bool AutoReload { get; set; } = true;

        public ConfigurationRegistration(object targetObject, PropertyInfo prop)
        {
            this.ModuleType = targetObject.GetType();
            this.weakRef = new WeakReference(targetObject);
            this.ConfigurationProperty = prop;
        }

        public ConfigurationRegistration(OrionModuleBase moduleInstance, PropertyInfo prop)
            : this((object)moduleInstance, prop)
        {
        }

        /// <summary>
        /// Sets the property inside the target module by reflection if the WeakReference
        /// still contains a valid reference to it.
        /// </summary>
        public void AssignProperty(object configurationObj)
        {
            Assert.Expression(() => configurationObj == null);
            Assert.Expression(() => weakRef == null);

            if (weakRef.IsAlive == false)
            {
                ProgramLog.Error.Log($"orion config: Module {ModuleType.Name} instance is dead!");
            }

            ConfigurationProperty.SetValue(weakRef.Target, configurationObj);
        }

        public object GetPropertyValue()
        {
            Assert.Expression(() => weakRef.IsAlive);
            return ConfigurationProperty.GetValue(weakRef.Target);
        }

        public TObjectValue GetPropertyValue<TObjectValue>()
            where TObjectValue : class, new()
        {
			return GetPropertyValue() as TObjectValue;
        }

        internal void UpdateWeakReference(object instance)
        {
            this.weakRef = new WeakReference(instance);
        }
    }

}