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
		/// <summary>
		/// Contains a weak reference to the target that contains the configuration property.
		/// </summary>
        protected WeakReference weakRef;

		/// <summary>
		/// Gets or sets the type of the target of the object which contains the configuration
		/// property.
		/// </summary>
        public Type ModuleType { get; protected set; }

		/// <summary>
		/// Gets the type of the property pointed to by the ConfigurationProperty lambda
		/// expression, which points to the object type to be (de)serialized to and from 
		/// respectively.
		/// </summary>
        public Type ConfigurationPropertyType => ConfigurationProperty.PropertyType;

		/// <summary>
		/// Gets or sets the reflection-based PropertyInfo describing the property to be
		/// get or set in the target object which contains the configuration to be loaded
		/// or saved.
		/// </summary>
        public PropertyInfo ConfigurationProperty { get; protected set; }

		/// <summary>
		/// Gets or sets a value indicating whether the target property for this configuration
		/// registration gets automatically updated when the file is changed externally.
		/// </summary>
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

		/// <summary>
		/// Gets the property value for this configuration registration from the
		/// target object instance.
		/// </summary>
        public object GetPropertyValue()
        {
            Assert.Expression(() => weakRef.IsAlive);
            return ConfigurationProperty.GetValue(weakRef.Target);
        }

		/// <summary>
		/// Gets the property value for this configuration registration from the
		/// target object instance.
		/// </summary>
		/// <typeparam name="TObjectValue">TObjectValue is the type to generically cast the property value as</typeparam>
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