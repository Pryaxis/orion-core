using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Orion.Commands.Commands.Exceptions;

namespace Orion.Commands.Commands
{
    public class Command
    {
        public string CommandName { get; private set; }
        public List<Type> ExpectedTypes { get; private set; }
        public bool AllowServer { get; set; }
        public string HelpText { get; set;}
        public string LongDescription { get; set; }
        //public List<Permission> Permissions { get; } = new List<Permission>();
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
            //Permissions.AddRange(permissions.Select(x => new Permission(x)));
            if (parent != null)
                Parent = new WeakReference<object>(parent);
        }

        /// <summary>
        /// Attempt to call the callback function behind this command.
        /// </summary>
        /// <param name="args">The arguments to be passed into the callback method.</param>
        /// <exception cref="CommandException">Thrown if the callback function throws an exception or if the target of the callback has been disposed.</exception>
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
                {
                    try
                    {
                        CommandMethod.Invoke(parent, args.ToArray());
                    }
                    catch (Exception e)
                    {
                        throw new CommandException("Command failed.", e);
                    }
                }
                else
                {
                    throw new CommandException("Parent object has been disposed since registration of this command.");
                }
            }
        }
    }
}