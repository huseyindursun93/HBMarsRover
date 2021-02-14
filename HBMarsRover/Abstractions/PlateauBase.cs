using System.Collections.Generic;

namespace HBMarsRover.Abstractions
{
    public abstract class PlateauBase
    {
        public int UpperRightX { get; set; }
        public int UpperRightY { get; set; }

        public List<IRover> RoverList { get; } = new List<IRover>();

        public abstract bool IsInArea(int x, int y);
    }
}
