using System;
using System.Linq;

namespace Project.Scripts.ApplicationState
{
    public class CommandLineArgs
    {
        public static bool HasArg(string arg)
        {
            return Environment
                .GetCommandLineArgs()
                .Any(a => a.Equals(arg, StringComparison.OrdinalIgnoreCase));
        }
    }
}