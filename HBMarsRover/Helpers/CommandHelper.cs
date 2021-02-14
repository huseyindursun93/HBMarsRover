using HBMarsRover.Abstractions;
using System;
using System.Linq;

namespace HBMarsRover.Helpers
{
    public static class CommandHelper
    {
        public static CommandBase GetCommand(char shorteningOfCommand)
        {
            var commandType = AppDomain.CurrentDomain.GetAssemblies()
                .First(asm => asm.FullName.Contains("HBMarsRover") && !asm.FullName.Contains("Tests"))
                .GetTypes().FirstOrDefault(t => t.BaseType == typeof(CommandBase) && t.Name.StartsWith(shorteningOfCommand));

            if (commandType == null)
            {
                throw new Exception($"Command {shorteningOfCommand} couldn't be found!");
            }

            var commandInstance = (CommandBase)Activator.CreateInstance(commandType);

            return commandInstance;
        }
    }
}
