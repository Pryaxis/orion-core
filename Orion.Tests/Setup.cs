using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Tests
{
    [SetUpFixture]
    public class Setup
    {
        [OneTimeSetUp]
        public void TestSetup()
        {
            Environment.CurrentDirectory = Path.GetDirectoryName(this.GetType().Assembly.Location);
        }
    }
}
