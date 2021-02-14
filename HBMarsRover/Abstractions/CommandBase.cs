using HBMarsRover.Helpers;
using HBMarsRover.Rovers;

namespace HBMarsRover.Abstractions
{
    public abstract class CommandBase
    {
        public virtual Position Execute(Position position)
        {
            var newPosition = position.DeepClone();
            newPosition = OperateBusiness(newPosition);

            return newPosition;
        }
        protected abstract Position OperateBusiness(Position position);
    }
}
