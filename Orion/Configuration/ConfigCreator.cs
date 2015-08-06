using Newtonsoft.Json;
using System.IO;

namespace Orion.Configuration
{
	/// <summary>
	/// Reads and writes config files in JSON format
	/// </summary>
	public class ConfigCreator
	{
		/// <summary>
		/// Creates a new config if one is not found at path/name.json, or reads an existing one
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="folder"></param>
		/// <param name="name"></param>
		/// <param name="config"></param>
		public void Create<T>(string folder, string name, out T config) where T : BaseConfig
		{
			config = default(T);
			if (!name.EndsWith(".json"))
			{
				name += ".json";
			}

			if (!Directory.Exists(folder))
			{
				Directory.CreateDirectory(folder);
			}

			if (!File.Exists(name))
			{
				Write(Path.Combine(folder, name), config);
			}

			Read(Path.Combine(folder, name), out config);
		}

		public void Create<T>(string path, out T config)
		{
			config = default(T);
			if (!File.Exists(path))
			{
				Write(path, config);
			}

			Read(path, out config);
		}


		public void Write<T>(string path, T config)
		{
			using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write))
			{
				Write(fs, config);
			}
		}

		public void Write<T>(Stream stream, T config)
		{
			string str = JsonConvert.SerializeObject(config, Formatting.Indented);
			using (StreamWriter sw = new StreamWriter(stream))
			{
				sw.Write(str);
			}
		}

		public void Read<T>(string path, out T config)
		{
			if (!File.Exists(path))
				config = default(T);
			using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				Read(fs, out config);
			}
		}

		public void Read<T>(Stream stream, out T config)
		{
			using (StreamReader sr = new StreamReader(stream))
			{
				config = JsonConvert.DeserializeObject<T>(sr.ReadToEnd());
			}
		}
	}
}
