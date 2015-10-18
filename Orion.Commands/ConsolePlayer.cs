using OTA;
using System;

namespace Orion.Commands
{
    public class ConsolePlayer : BasePlayer
    {
        public override string Name { get; protected set; } = "Console";

        public override void SendMessage(string message, int sender = 255, byte R = 255, byte G = 255, byte B = 255)
        {
            Console.WriteLine(message);
        }
    }
}