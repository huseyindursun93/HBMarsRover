using HBMarsRover.Abstractions;
using System;

namespace HBMarsRover.Rovers
{
    [Serializable]
    public class Position
    {
        public Coordinate Coordinate { get; set; }
        public DirectionBase Direction { get; set; }

        public override string ToString()
        {
            return $"{Coordinate} {Direction}";
        }
    }
}
