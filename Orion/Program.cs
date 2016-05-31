using System;

namespace Orion
{
    /// <summary>
    /// Defines the entry point for orion, and how it interacts with OTAPI
    /// </summary>
    public static class Program
    {
        static Orion orionInstance;

        public static void Main(string[] args)
        {
            orionInstance = new Orion();

            Initialized();

            Console.WriteLine("Initialising server...");
            Terraria.WindowsLaunch.Main(args);

            Disposed();
        }

        /// <summary>
        /// Occurs when the plugin is initialized.
        /// </summary>
        /// <param name="state"></param>
        static void Initialized()
        {
            Version orionVersion = typeof(Program).Assembly.GetName().Version;
            orionInstance.Initialize();

            //ProgramLog.Log($"Orion version {orionVersion.ToString()} initialized.");
        }

        static void Disposed()
        {
            orionInstance.Dispose();
        }
    }
}
