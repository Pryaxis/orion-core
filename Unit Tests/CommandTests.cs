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
            Commands.Parser.AddConverter<OrionPlayer>(x =>
            {
                if (x == "TestPlayer")
                    return TestPlayer;
                throw new ArgumentParsingException("Could not convert argument to Player.");
            });
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

        [TestMethod]
        public void TestRegisterStaticCommand()
        {
            Assert.IsTrue(Commands.Commands.Count == 0);
            Commands.AddCommand("test", StaticTestCommand);
            Assert.IsTrue(Commands.Commands.Count == 1);
        }

        [TestMethod]
        public void TestRunStaticCommand()
        {
            Commands.AddCommand("test", StaticTestCommand);
            Commands.ParseAndCallCommand(TestPlayer, "/test");
        }

        [TestMethod]
        public void TestRegisterCommandWithManyArgs()
        {
            Assert.IsTrue(Commands.Commands.Count == 0);
            Commands.AddCommand<OrionPlayer, int, string, TimeSpan>("test", TestCommandWithArgs);
            Assert.IsTrue(Commands.Commands.Count == 1);
        }

        [TestMethod]
        public void TestRunCommandWithManyArgs()
        {
            Commands.AddCommand<OrionPlayer, int, string, TimeSpan>("test", TestCommandWithArgs);
            Commands.ParseAndCallCommand(TestPlayer, "/test TestPlayer 1 \"testing strings\" 3:2:1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentParsingException))]
        public void TestRunCommandWithBadArgs()
        {
            Commands.AddCommand<OrionPlayer, int, string, TimeSpan>("test", TestCommandWithArgs);
            Commands.ParseAndCallCommand(TestPlayer, "/test playerdoesntexist 1 2 3:0:0");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentParsingException))]
        public void TestRunCommandWithNonConvertibleArgs()
        {
            Commands.AddCommand<OrionPlayer>("test", TestCommandWithNonConveritbleArgs);
            Commands.ParseAndCallCommand(TestPlayer, "/test no-converter");
        }

        private void TestCommand()
        {
            Console.WriteLine("Test Success.");
        }

        private static void StaticTestCommand()
        {
            Console.WriteLine("Static Test Success.");
        }

        private void TestCommandWithArgs(OrionPlayer ply, int arg1, string arg2, TimeSpan arg3)
        {
            Assert.AreEqual(TestPlayer, ply);   
            Assert.AreEqual(1, arg1);
            Assert.AreEqual("testing strings", arg2);
            Assert.AreEqual(new TimeSpan(0,3,2,1), arg3);
        }

        private void TestCommandWithNonConveritbleArgs(OrionPlayer ply)
        {
            Assert.Fail("Should have thrown an exception and not reached here.");
        }
    }
}