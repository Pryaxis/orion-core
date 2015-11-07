using Orion.Framework;
using Orion.Extensions;
using OTA.DebugFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using OTA.Logging;
using System.IO;

using Newtonsoft.Json;

namespace Orion.Modules.Configuration
{
    [OrionModule("Orion Configuration Module", "Nyx Studios", Description = "Provides an automatic configuration interface for Orion modules")]
    public class ConfigurationModule : OrionModuleBase, IConfigurationProvider
    {
        protected readonly List<ConfigurationRegistration> configurationRegistrations;

        public ConfigurationModule(Orion core)
            : base(core)
        {
            configurationRegistrations = new List<ConfigurationRegistration>();
        }


        public ConfigurationRegistration RegisterProperty<TModule, TConfigurationClass>(TModule target, Expression<Func<TModule, TConfigurationClass>> configurationPropertySelector)
            where TConfigurationClass : class, new()
            where TModule : OrionModuleBase
            {
                ConfigurationRegistration registration;
                PropertyInfo targetProperty;
                MemberExpression body;
                
                Assert.Expression(() => target == null);
                body = configurationPropertySelector.Body as MemberExpression;
                Assert.Expression(() => body == null);
                targetProperty = body.Member as PropertyInfo;
                Assert.Expression(() => body.Member == null);
    
                registration = GetConfigurationRegistration(target.GetType());
                if (registration == null)
                {
                    registration = new ConfigurationRegistration(target, targetProperty);
                    configurationRegistrations.Add(registration);
                }
                else
                {
                    /*
                    * If RegisterProperty is called again on a registration that means the
                    * WeakReference now points to an invalid target, Update the registration
                    * instead of creating a new one.
                    */
                    registration.UpdateWeakReference(target);
                }
                
                return registration;
            }

        public ConfigurationRegistration RegisterProperty<TConfigurationClass>(object target, Expression<Func<TConfigurationClass>> configurationPropertySelector)
            where TConfigurationClass : class, new()
        {
            ConfigurationRegistration registration;
            PropertyInfo targetProperty;
            MemberExpression body;
            
            Assert.Expression(() => target == null);
            body = configurationPropertySelector.Body as MemberExpression;
            Assert.Expression(() => body == null);
            targetProperty = body.Member as PropertyInfo;
            Assert.Expression(() => body.Member == null);

            registration = GetConfigurationRegistration(target.GetType());
            if (registration == null)
            {
                registration = new ConfigurationRegistration(target, targetProperty);
                configurationRegistrations.Add(registration);
            }
            else
            {
                /*
                 * If RegisterProperty is called again on a registration that means the
                 * WeakReference now points to an invalid target, Update the registration
                 * instead of creating a new one.
                 */
                registration.UpdateWeakReference(target);
            }
            
            return registration;
        }



        public object Load(Type moduleType)
        {
            string configPath = GetModuleFilePath(moduleType);

            ProgramLog.Debug.Log($"orion config: Loading configuration for type {moduleType.Name} from {Path.GetFileName(configPath)}.json");

            object deserializedConfig = null;

            if (File.Exists(configPath) == false)
            {
                ProgramLog.Debug.Log($"orion config: No configuration file exists for {moduleType.Name}, creating one");
                deserializedConfig = LoadDefaultConfiguration(moduleType);
                Save(moduleType);
            }

            try
            {
                deserializedConfig = JsonConvert.DeserializeObject(File.ReadAllText(configPath));
            }
            catch (Exception ex)
            {
                ProgramLog.Error.Log(ex.Message);
            }

            AssignConfigurationProperty(moduleType, deserializedConfig);

            return deserializedConfig;
        }

        protected string GetModuleFilePath(Type moduleType)
        {
            string configTypeName = moduleType.Name.Split(',')[0];
            return Path.Combine(Core.OrionConfigurationPath, $"{configTypeName.GenerateSlug()}.json");
        }

        public TConfigurationObject Load<TConfigurationObject>(Type moduleType)
            where TConfigurationObject : class, new()
        {
            object configurationObject = Load(moduleType);
            return configurationObject as TConfigurationObject;
        }

        public void Save(Type moduleType)
        {
            ConfigurationRegistration registration = GetConfigurationRegistration(moduleType);
            object configValue = registration.GetPropertyValue();
            string serializedValue;

            if (configValue == null)
            {
                ProgramLog.Debug.Log($"orion config: Registered property value on {moduleType.Name} was null and cannot be saved");
            }

            try
            {
                serializedValue = JsonConvert.SerializeObject(configValue);
            }
            catch (Exception ex)
            {
                ProgramLog.Error.Log($"orion config: error serializing configuration property for {moduleType.Name}: {ex.Message}");
                throw;
            }

            WriteObjectSafe(moduleType, serializedValue);
        }

        protected object LoadDefaultConfiguration(Type moduleType)
        {
            ConfigurationRegistration registration = GetConfigurationRegistration(moduleType);

            /*
             * Creates a new object of whatever PropertyType the registration points to.
             * This assumes that Activator.CreateInstance() has access to a public
             * parameterless constructor to make a new configuration object to pass to
             * the serialization engine.
             *
             * In order for an object to be serializable it has to have a new() anyway
             * so these calls should be safe.
             */

            return Activator.CreateInstance(registration.ConfigurationPropertyType);
        }

        /// <summary>
        /// Writes an orion configuration property out to a json file using a temporary
        /// buffer, eliminating it from corruption.
        /// </summary>
        protected void WriteObjectSafe(Type moduleType, string contents)
        {
            string configPath = GetModuleFilePath(moduleType);

            WriteObjectSafe(configPath, contents);
        }

        protected void WriteObjectSafe(string filePath, string contents)
        {
            string tmpFilePath = Path.GetTempFileName();

            using (StreamWriter sw = new StreamWriter(tmpFilePath))
            {
                sw.WriteLine(contents); //ensures LF at end of file
            }

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            File.Move(tmpFilePath, filePath);
        }

        protected ConfigurationRegistration GetConfigurationRegistration(Type moduleType)
        {
            return configurationRegistrations.FirstOrDefault(i => i.ModuleType == moduleType);
        }

        protected void AssignConfigurationProperty(Type moduleType, object deserializedConfig)
        {
            ConfigurationRegistration configReg = GetConfigurationRegistration(moduleType);

            if (configReg == null)
            {
                return;
            }

            configReg.AssignProperty(deserializedConfig);
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
