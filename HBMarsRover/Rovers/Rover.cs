using HBMarsRover.Abstractions;
using System;
using System.Collections.Generic;

namespace HBMarsRover.Rovers
{
    public class Rover : IRover
    {
        public Rover(Position position)
        {
            Position = position;
            CommandList = new List<CommandBase>();
        }

        public Position Position { get; set; }
        public List<CommandBase> CommandList { get; set; }

        public void AddCommand(CommandBase command)
        {
            CommandList.Add(command);
        }

        public void Work(PlateauBase plateau)
        {
            foreach (var command in CommandList)
            {
                var newPosition = command.Execute(Position);

                if (!plateau.IsInArea(newPosition.Coordinate.X, newPosition.Coordinate.Y))
                {
                    throw new Exception("Rover has exited from border of the plateau!");
                }

                Position = newPosition;
            }
        }
    }
}
