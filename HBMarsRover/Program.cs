using HBMarsRover.Parsers;
using System;
using System.Collections.Generic;

namespace HBMarsRover
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                bool isContinue = true;

                Console.WriteLine("Please specify an area to move. For example: 5 5. Plateau: ");
                var upperRightCoordinates = Console.ReadLine();

                List<string> commandLines = new List<string>() { upperRightCoordinates };

                int roverCounter = 1;

                while (isContinue)
                {
                    Console.WriteLine($"Set position of {roverCounter}. rover. For example: 1 2 N. Position: ");
                    var positionCommand = Console.ReadLine();
                    commandLines.Add(positionCommand);

                    Console.WriteLine($"Set movement of {roverCounter}. rover. For example: LMLMLMLMM. Movement: ");
                    var movementCommand = Console.ReadLine();
                    commandLines.Add(movementCommand);

                    Console.WriteLine("Press 0 to exit or others to add a new rover");
                    var continueCommand = Console.ReadLine();
                    if (continueCommand == "0") isContinue = false;

                    roverCounter++;
                    Console.WriteLine("---------------------------------------------");
                }

                CommandParser commandParser = new CommandParser(commandLines);
                var plateau = commandParser.Parse();

                foreach (var rover in plateau.RoverList)
                {
                    rover.Work(plateau);
                    Console.WriteLine(rover.Position);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
