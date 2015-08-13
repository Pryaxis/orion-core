using Newtonsoft.Json;
using System;
using System.IO;

namespace Orion.Configuration
{
	/// <summary>
	/// Reads and writes config files in JSON format
	/// </summary>
	public class ConfigCreator
	{
		private Orion _core;

		public ConfigCreator(Orion core)
		{
			_core = core;
		}

		/// <summary>
		/// Creates a new config if one is not found at name.json, otherwise reads an existing one
		/// </summary>
		/// <typeparam name="T">MUST inherit from <see cref="BaseConfig"></see></typeparam>
		/// <param name="name">File name</param>
		/// <param name="config"></param>
		public void Create<T>(string name, out T config) where T : BaseConfig
		{
			EnsureFilePathValidity(ref name);

			if (!Directory.Exists(_core.ConfigPath))
			{
				Directory.CreateDirectory(_core.ConfigPath);
			}

			//Calls write if file does not exist
			Read(Path.Combine(_core.ConfigPath, name), out config);
		}

		/// <summary>
		/// Write a config file to disk.
		/// </summary>
		/// <typeparam name="T">MUST inherit from <see cref="BaseConfig"></see></typeparam>
		/// <param name="name">File name</param>
		/// <param name="config"></param>
		public void Write<T>(string name, T config) where T : BaseConfig
		{
			EnsureFilePathValidity(ref name);

			using (FileStream fs = new FileStream(name, FileMode.Create, FileAccess.Write, FileShare.Write))
			{
				string str = JsonConvert.SerializeObject(config, Formatting.Indented);
				using (StreamWriter sw = new StreamWriter(fs))
				{
					sw.Write(str);
				}
			}
		}

		/// <summary>
		/// Read a config file from disk.
		/// </summary>
		/// <typeparam name="T">MUST inherit from <see cref="BaseConfig"></see></typeparam>
		/// <param name="name">File name</param>
		/// <param name="config"></param>
		public void Read<T>(string name, out T config) where T : BaseConfig
		{
			EnsureFilePathValidity(ref name);

			if (!File.Exists(name))
			{
				//No point reading again if the file didn't exist previously.
				//Create an instance of the config, write it, then return.
				config = Activator.CreateInstance<T>();
				Write(name, config);
				return;
			}

			using (FileStream fs = new FileStream(name, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
			{
				using (StreamReader sr = new StreamReader(fs))
				{
					config = JsonConvert.DeserializeObject<T>(sr.ReadToEnd());
					config.OnRead(config);
				}
			}
		}

		/// <summary>
		/// Ensures that a given path is valid for a config context.
		/// I.e., no directory characters or invalid filename characters.
		/// </summary>
		/// <param name="path"></param>
		private void EnsureFilePathValidity(ref string path)
		{
			//Determine the last index of an invalid path or directory character
			int index =
				Math.Max(path.LastIndexOfAny(Path.GetInvalidFileNameChars()), path.LastIndexOfAny(Path.GetInvalidPathChars()));

			//if there is an invalid character, remove everything up to and including it
			if (index != -1)
			{
				path = path.Substring(index + 1, path.Length - (index + 1));
			}

			path = Path.ChangeExtension(path, ".json");
		}
	}
}
