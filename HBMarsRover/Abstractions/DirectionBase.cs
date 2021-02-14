using System;
using System.Linq;

namespace HBMarsRover.Abstractions
{
    [Serializable]
    public abstract class DirectionBase
    {
        public override string ToString()
        {
            return $"{this.GetType().Name.First()}";
        }
    }
}
