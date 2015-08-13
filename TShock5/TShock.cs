using Orion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Terraria;

namespace TShock5
{
	[OrionVersion(1, 0)]
	public class TShock : OrionPlugin
	{
		/// <summary>
		/// TShock's configuration
		/// </summary>
		public ConfigFile Config = new ConfigFile();
		/// <summary>
		/// Path to TShock's save folder
		/// </summary>
		public string SavePath { get { return Path.Combine(Core.SavePath, "TShock"); } }

		public override string Author
		{
			get
			{
				return "Nyx Studios";
			}
		}

		public override string Name
		{
			get
			{
				return "TShock 5";
			}
		}

		public override int Order
		{
			get
			{
				return 0;
			}
		}

		public override Version Version
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName().Version;
			}
		}

		public TShock(Orion.Orion instance) :
			base(instance)
		{
			//nothing yet
		}

		public override void Initialize()
		{
			try
			{
				//Creates the config file
				Core.ConfigCreator.Create("TShock.json", out Config);

				AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
			}
			catch (Exception) //Temp
			{

			}
		}

		/// <summary>
		/// Catches exceptions that we don't handle
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{

			if (e.ExceptionObject.ToString().Contains("Terraria.Netplay.ListenForClients") ||
				e.ExceptionObject.ToString().Contains("Terraria.Netplay.ServerLoop"))
			{
				var sb = new List<string>();
				for (int i = 0; i < Netplay.Clients.Length; i++)
				{
					if (Netplay.Clients[i] == null)
					{
						sb.Add("Client[" + i + "]");
					}
					else if (Netplay.Clients[i].Socket == null)
					{
						sb.Add("Tcp[" + i + "]");
					}
				}

				Console.WriteLine(sb);
				//Orion's logging is broken for now
				//Log.Error(string.Join(", ", sb));
			}

			if (e.IsTerminating)
			{
				//Save world
				/*if (Main.worldPathName != null && Config.SaveWorldOnCrash)
				{
					Main.worldPathName += ".crash";
					SaveManager.Instance.SaveWorld();
				}*/
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				AppDomain.CurrentDomain.UnhandledException -= CurrentDomain_UnhandledException;
			}
		}
	}
}