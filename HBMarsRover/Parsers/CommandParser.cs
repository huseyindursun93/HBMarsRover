using HBMarsRover.Abstractions;
using HBMarsRover.Helpers;
using HBMarsRover.Rovers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HBMarsRover.Parsers
{
    public class CommandParser
    {
        private readonly List<string> _commandSets;
        public CommandParser(List<string> commandSets)
        {
            _commandSets = commandSets;
        }

        public PlateauBase Parse()
        {
            if (_commandSets == null || _commandSets.Count < 3)
            {
                throw new Exception("Your input doesn't contain valid command sets!");
            }

            if (_commandSets.Count % 2 == 0)
            {
                throw new Exception("Your command sets are lacked!");
            }

            var firstLine = _commandSets.FirstOrDefault();
            var plateau = CreatePlateau(firstLine);

            for (int counter = 1; counter < _commandSets.Count; counter += 2)
            {
                var positionCommand = _commandSets[counter];
                var movementCommand = _commandSets[counter + 1];

                var rover = CreateRover(positionCommand, movementCommand);

                plateau.RoverList.Add(rover);
            }

            return plateau;
        }

        private PlateauBase CreatePlateau(string command)
        {
            var upperRightCoordinates = command.Split(' ');

            if (upperRightCoordinates.Length != 2)
            {
                throw new Exception("Upper-right coordinates are invalid!");
            }

            if (!int.TryParse(upperRightCoordinates[0], out int upperRightX))
            {
                throw new Exception("Upper-right X coordinate is invalid!");
            }

            if (upperRightX <= 0)
            {
                throw new Exception("The lower-left X coordinate is limited by 0. It's not valid.");
            }

            if (!int.TryParse(upperRightCoordinates[1], out int upperRightY))
            {
                throw new Exception("Upper-right Y coordinate is invalid!");
            }

            if (upperRightY <= 0)
            {
                throw new Exception("The lower-left Y coordinate is limited by 0. It's not valid.");
            }

            return new Plateau
            {
                UpperRightX = upperRightX,
                UpperRightY = upperRightY
            };
        }

        private IRover CreateRover(string positionCommands, string movementCommands)
        {
            var splittedPosition = positionCommands.Split(' ');

            if (splittedPosition.Length != 3)
            {
                throw new Exception("Rover's position information is invalid!");
            }

            if (!int.TryParse(splittedPosition[0], out int latitudeCoordinate))
            {
                throw new Exception("Rover's latitude coordinate is invalid!");
            }

            if (!int.TryParse(splittedPosition[1], out int longitudeCoordinate))
            {
                throw new Exception("Rover's longitude coordinate is invalid!");
            }

            if (string.IsNullOrEmpty(movementCommands))
            {
                throw new Exception("Rover's movement command can't be null or empty!");
            }

            var direction = DirectionHelper.GetDirection(splittedPosition[2]);

            IRover rover = new Rover(
                new Position()
                {
                    Direction = direction,
                    Coordinate = new Coordinate(latitudeCoordinate, longitudeCoordinate)
                });

            foreach (var movementCommand in movementCommands)
            {
                var command = CommandHelper.GetCommand(movementCommand);
                rover.AddCommand(command);
            }

            return rover;
        }
    }
}
