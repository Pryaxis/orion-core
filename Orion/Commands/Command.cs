using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Orion.Commands
{
    public class Command
    {
        public string CommandName { get; private set; }
        public List<Type> ExpectedTypes { get; private set; }
        private MethodInfo CommandMethod;

        public Command(string name, MethodInfo commMethod)
        {
            CommandName = name;
            CommandMethod = commMethod;
            ExpectedTypes = commMethod.GetParameters().Select(x => x.ParameterType).ToList();
        }

        public void Call(List<ArgumentObject> args)
        {
            CommandMethod.Invoke(null, args.Select(x => x.Value).ToArray());
        }
    }
}