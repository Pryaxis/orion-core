using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Orion.Permissions;

namespace Orion.Commands
{
    public class Command
    {
        public string CommandName { get; private set; }
        public List<Type> ExpectedTypes { get; private set; }
        public List<Permission> Permissions { get; } = new List<Permission>();
        private MethodInfo CommandMethod;
        private WeakReference<object> Parent; 

        public Command(string name, MethodInfo commMethod, object parent)
        { 
            CommandName = name;
            CommandMethod = commMethod;
            ExpectedTypes = commMethod.GetParameters().Select(x => x.ParameterType).ToList();
            if (parent != null)
                Parent = new WeakReference<object>(parent);
        }

        public Command(string name, MethodInfo commMethod, object parent, params string[] permissions)
        {
            CommandName = name;
            CommandMethod = commMethod;
            ExpectedTypes = commMethod.GetParameters().Select(x => x.ParameterType).ToList();
            Permissions.AddRange(permissions.Select(x => new Permission(x)));
            if (parent != null)
                Parent = new WeakReference<object>(parent);
        }

        public void Call(List<object> args)
        {
            object parent = null;
            if (CommandMethod.IsStatic)
            {
                CommandMethod.Invoke(null, args.ToArray());
            }
            else
            {
                if (Parent.TryGetTarget(out parent))
                    CommandMethod.Invoke(parent, args.ToArray());
                else
                    throw new CommandException("Parent object has been disposed since registration of this command.");
            }
        }
    }
}