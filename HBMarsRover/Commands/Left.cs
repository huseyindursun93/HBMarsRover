using HBMarsRover.Abstractions;
using HBMarsRover.Directions;
using HBMarsRover.Rovers;

namespace HBMarsRover.Commands
{
    public class Left : CommandBase
    {
        protected override Position OperateBusiness(Position position)
        {
            if (position.Direction is West)
            {
                position.Direction = new South();
            }
            else if (position.Direction is North)
            {
                position.Direction = new West();
            }
            else if (position.Direction is East)
            {
                position.Direction = new North();
            }
            else if (position.Direction is South)
            {
                position.Direction = new East();
            }

            return position;
        }
    }
}
