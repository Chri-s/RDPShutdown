using System;
using System.Collections.Generic;
using System.Text;

namespace RemoteDesktopShutdown
{
    internal class ShutdownAction
    {
        public ShutdownAction(string name, MethodAction action)
        {
            Name = name;
            Action = action;
        }

        public string Name { get; private set; }

        public MethodAction Action { get; private set; }

        public override string ToString()
        {
            return Name;
        }
    }
}