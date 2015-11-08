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
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Orion.Modules.Configuration
{
    [OrionModule("Orion Configuration Module", "Nyx Studios", Description = "Provides an automatic configuration interface for Orion modules")]
    public class ConfigurationModule : OrionModuleBase, IConfigurationProvider
    {
        /// <summary>
        /// Contains a list of all configuration registrations, tieing Orion modules to
        /// configuration files.
        /// </summary>
        protected readonly List<ConfigurationRegistration> configurationRegistrations;

        protected readonly FileSystemWatcher configFileWatcher;


        /// <summary>
        /// Initializes a new instance of the <see cref="Orion.Modules.Configuration.ConfigurationModule"/> class.
        /// </summary>
        public ConfigurationModule(Orion core)
            : base(core)
        {
            configurationRegistrations = new List<ConfigurationRegistration>();
            configFileWatcher = new FileSystemWatcher();

            configFileWatcher.Path = Core.OrionConfigurationPath;
            configFileWatcher.NotifyFilter = NotifyFilters.LastWrite;
            configFileWatcher.EnableRaisingEvents = true;
            configFileWatcher.Changed += ConfigFileWatcher_Changed;
        }

        private async void ConfigFileWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            ConfigurationRegistration registration;
            byte[] shaHash = new byte[0];

            if (e.ChangeType != WatcherChangeTypes.Changed
                || Path.GetFileName(e.FullPath).EndsWith(".json") == false)
            {
                return;
            }

            registration = GetConfigurationRegistration(Path.GetFileName(e.FullPath));

            /*
             * This is a workaround for the general shittyness that is the windows
             * File I/O mechanism.  IF you try to access the file in the event too
             * quickly after the change event fires, you can run into access
             * violations because the other program hasn't fully released the handle 
             * on the file yet.
             *
             * FileSystemWatcher events also get fired twice for single file changes.
             *
             * Just another one of the awesome features of win32 file API.
             */

            await Task.Delay(100);

            try
            {
                using (SHA1Managed shaProvider = new SHA1Managed())
                using (FileStream fs = new FileStream(e.FullPath, FileMode.Open, FileAccess.Read))
                {
                    shaHash = shaProvider.ComputeHash(fs);
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                ProgramLog.Log(ex);
            }

            /*
             * An internal SHA1 hash of the config file from disk is kept
             * and compared to the last access, to prevent the reload
             * procedure from firing more than once cause of the shitty
             * FileSystemWatcher duplicate event issues.
             */
            if (registration == null
                || registration.AutoReload == false
                || registration.sha1Hash.SequenceEqual(shaHash))
            {
                return;
            }

            ProgramLog.Debug.Log($"orion config: Detected config change for {registration.ModuleType.Name} through file {registration.FileName} and was reloaded.");

            Load(registration.ModuleType);
            registration.sha1Hash = shaHash;
        }


        /// <summary>
        /// Registers the specified Orion module into the automatic configuration system along with
        /// a LINQ-style lambda expression pointing to the configuration property that will be updated
        /// when the configuration is reloaded.
        /// </summary>
        /// <typeparam name="TModule">TModule is any Orion module.</typeparam>
        /// <typeparam name="TConfigurationClass">TConfigurationClass is inferred from the type of the property in the LINQ expression</typeparam>
        /// <param name="target">A reference to the object instance instance containing the configuration property</param>
        /// <param name="configurationPropertySelector">A LINQ style lambda expression pointing to the configuration property inside the class that will be updated
        /// with the deserialized configuration on load, and serialized on save</param>
        /// <returns>The property.</returns>
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

            Load(target.GetType());

            return registration;
        }

        /// <summary>
        /// Registers the specified object's property into the automatic configuration system along with
        /// a LINQ-style lambda expression pointing to the configuration property that will be updated
        /// when the configuration is reloaded.
        /// </summary>
        /// <typeparam name="TConfigurationClass">TConfigurationClass is inferred from the type of the property in the LINQ expression</typeparam>
        /// <param name="target">A reference to the object instance instance containing the configuration property</param>
        /// <param name="configurationPropertySelector">A LINQ style lambda expression pointing to the configuration property inside the class that will be updated
        /// with the deserialized configuration on load, and serialized on save</param>
        /// <returns>The property.</returns>
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

            Load(target.GetType());

            return registration;
        }

        /// <summary>
        /// Loads a deserialized configuration object as dictated by the configuration registration
        /// for the specified type. This method causes the property specified in the configuration
        /// registration to also be updated with the return value from this call to Load().
        /// </summary>
        /// <param name="moduleType">Module type.</param>
        public object Load(Type moduleType)
        {
            ConfigurationRegistration registration = GetConfigurationRegistration(moduleType);
            string configPath = Path.Combine(Core.OrionConfigurationPath, registration.FileName);

            ProgramLog.Debug.Log($"orion config: Loading configuration for type {moduleType.Name} from {registration.FileName}");

            object deserializedConfig = null;

            if (File.Exists(configPath) == false)
            {
                ProgramLog.Debug.Log($"orion config: No configuration file exists for {moduleType.Name}, creating one");
                deserializedConfig = LoadDefaultConfiguration(moduleType);
                Save(moduleType);
            }

            try
            {
                deserializedConfig = JsonConvert.DeserializeObject(File.ReadAllText(configPath), registration.ConfigurationPropertyType);
            }
            catch (Exception ex)
            {
                //TODO: Handle corruption of config objects
                deserializedConfig = LoadDefaultConfiguration(moduleType);
                ProgramLog.Error.Log(ex.Message);
            }

            AssignConfigurationProperty(moduleType, deserializedConfig);

            return deserializedConfig;
        }

        /// <summary>
        /// Gets the module file path, derived from the Orion configuration path set in the
        /// orion core and a slugified version of the type of the Orion module.
        /// </summary>
        /// <returns>The module file path.</returns>
        /// <param name="moduleType">Module type.</param>
        protected string GetModuleFilePath(Type moduleType)
        {
            ConfigurationRegistration registration = GetConfigurationRegistration(moduleType);

            return Path.Combine(Core.OrionConfigurationPath, registration.FileName);
        }

        /// <summary>
        /// Loads a deserialized configuration object as dictated by the configuration registration
        /// for the specified type, casted as a <typeparamref>TConfigurationObject</typeparamref>.
        /// This method causes the property specified in the registration to also be updated with the 
        /// return value from this call to Load().
        /// </summary>
        /// <param name="moduleType">Module type.</param>
        /// <typeparam name="TConfigurationObject">The type of the deserialized configuration object</typeparam>
        public TConfigurationObject Load<TConfigurationObject>(Type moduleType)
            where TConfigurationObject : class, new()
        {
            object configurationObject = Load(moduleType);
            return configurationObject as TConfigurationObject;
        }

        /// <summary>
        /// Saves the contents of the registered configuration property to disk as a serialized object.
        /// </summary>
        /// <param name="moduleType">Module type.</param>
        public void Save(Type moduleType)
        {
            ConfigurationRegistration registration = GetConfigurationRegistration(moduleType);
            object configValue = registration.GetPropertyValue();
            string serializedValue;

            if (configValue == null)
            {
                ProgramLog.Error.Log($"orion config: Registered property value on {moduleType.Name} was null and cannot be saved");
                return;
            }

            try
            {
                serializedValue = JsonConvert.SerializeObject(configValue, Formatting.Indented);
            }
            catch (Exception ex)
            {
                ProgramLog.Error.Log($"orion config: error serializing configuration property for {moduleType.Name}: {ex.Message}");
                throw;
            }

            WriteObjectSafe(moduleType, serializedValue);
        }

        /// <summary>
        /// Loads the default configuration for a configuration registration based on
        /// a reflection-based new instance of the registration's property type.
        /// </summary>
        /// <returns>The default configuration.</returns>
        /// <param name="moduleType">Module type.</param>
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

            object instance = Activator.CreateInstance(registration.ConfigurationPropertyType);
            AssignConfigurationProperty(moduleType, instance);

            return instance;
        }

        /// <summary>
        /// Writes an orion configuration property out to a json file using a temporary
        /// buffer, protecting it from disk-based corruption.
        /// </summary>
        protected void WriteObjectSafe(Type moduleType, string contents)
        {
            string configPath = GetModuleFilePath(moduleType);

            WriteObjectSafe(configPath, contents);
        }

        /// <summary>
        /// Writes an orion configuration property out to a json file using a temporary
        /// buffer, eliminating it from corruption.
        /// </summary>
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

        /// <summary>
        /// Gets the configuration registration registered for the specified type.
        /// </summary>
        /// <returns>The configuration registration.</returns>
        /// <param name="moduleType">Module type.</param>
        protected ConfigurationRegistration GetConfigurationRegistration(Type moduleType)
        {
            return configurationRegistrations.FirstOrDefault(i => i.ModuleType == moduleType);
        }

        protected ConfigurationRegistration GetConfigurationRegistration(string fileName)
        {
            return configurationRegistrations.FirstOrDefault(i => i.FileName == fileName);
        }

        /// <summary>
        /// Assigns the configuration property.
        /// </summary>
        /// <param name="moduleType">Module type.</param>
        /// <param name="deserializedConfig">Deserialized config.</param>
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
