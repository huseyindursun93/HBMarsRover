using HBMarsRover.Rovers;

namespace HBMarsRover.Abstractions
{
    public interface IRover
    {
        Position Position { get; }
        void AddCommand(CommandBase command);
        void Work(PlateauBase plateau);
    }
}
