using HBMarsRover.Abstractions;
using System;
using System.Linq;

namespace HBMarsRover.Helpers
{
    public static class DirectionHelper
    {
        public static DirectionBase GetDirection(string shorteningOfDirection)
        {
            var commandType = AppDomain.CurrentDomain.GetAssemblies()
                .First(asm => asm.FullName.Contains("HBMarsRover") && !asm.FullName.Contains("Tests"))
                .GetTypes().FirstOrDefault(t => t.BaseType == typeof(DirectionBase) && t.Name.StartsWith(shorteningOfDirection));

            if (commandType == null)
            {
                throw new Exception($"Direction {shorteningOfDirection} couldn't be found!");
            }

            var commandInstance = (DirectionBase)Activator.CreateInstance(commandType);

            return commandInstance;
        }
    }
}
