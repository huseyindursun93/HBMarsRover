using HBMarsRover.Abstractions;

namespace HBMarsRover
{
    public class Plateau : PlateauBase
    {
        public override bool IsInArea(int x, int y)
        {
            return x >= 0 && x <= UpperRightX && y >= 0 && y <= UpperRightY;
        }
    }
}
