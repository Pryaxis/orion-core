using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orion;
using Orion.Commands;
using Orion.UserAccounts;

namespace Unit_Tests
{
    [TestClass]
    public class CommandTests
    {
        private CommandManager Commands;
        private OrionPlayer TestPlayer;

        [TestInitialize]
        public void SetUp()
        {
            Commands = new CommandManager();
            TestPlayer = new OrionPlayer();
            var user = new UserAccount();
            user.Name = "TestPlayer";
            TestPlayer.User = user;
        }

        [TestMethod]
        public void TestRegisterCommand()
        {
            Assert.IsTrue(Commands.Commands.Count == 0);
            Commands.AddCommand("test", TestCommand);
            Assert.IsTrue(Commands.Commands.Count == 1);
        }

        [TestMethod]
        public void TestRunCommand()
        {
            Commands.AddCommand("test", TestCommand);
            Commands.ParseAndCallCommand(TestPlayer, "/test");
        }

        private void TestCommand()
        {
            Console.WriteLine("Test Success.");
        }
    }
}