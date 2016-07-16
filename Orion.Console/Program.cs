using System;
using Orion;

namespace ConsoleApplication
{
    public class Program
    {
        static Orion.Orion orion;

        public static void Main(string[] args)
        {
            orion = new Orion.Orion();

            orion.StartServer();

            orion.Dispose();
        }
    }
}
