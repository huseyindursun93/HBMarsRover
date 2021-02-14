using HBMarsRover.Abstractions;
using HBMarsRover.Directions;
using HBMarsRover.Rovers;

namespace HBMarsRover.Commands
{
    public class Move : CommandBase
    {
        protected override Position OperateBusiness(Position position)
        {
            if (position.Direction is West)
            {
                position.Coordinate.X--;
            }
            else if (position.Direction is North)
            {
                position.Coordinate.Y++;
            }
            else if (position.Direction is East)
            {
                position.Coordinate.X++;
            }
            else if (position.Direction is South)
            {
                position.Coordinate.Y--;
            }

            return position;
        }
    }
}
