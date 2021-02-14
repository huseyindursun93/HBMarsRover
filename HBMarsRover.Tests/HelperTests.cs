using FluentAssertions;
using HBMarsRover.Helpers;
using NUnit.Framework;
using System;

namespace HBMarsRover.Tests
{
    public class HelperTests
    {
        [TestFixture]
        public class CommandHelperTests
        {
            [TestCase("L")]
            public void GetCommand_WhenShorteningCommandIsL_ShouldBeOfTypeLeft(char shorteningOfCommand)
            {
                var command = CommandHelper.GetCommand(shorteningOfCommand);

                command.Should().BeOfType(typeof(Commands.Left));
            }

            [TestCase("R")]
            public void GetCommand_WhenShorteningCommandIsR_ShouldBeOfTypeRight(char shorteningOfCommand)
            {
                var command = CommandHelper.GetCommand(shorteningOfCommand);

                command.Should().BeOfType(typeof(Commands.Right));
            }

            [TestCase("M")]
            public void GetCommand_WhenShorteningCommandIsM_ShouldBeOfTypeMove(char shorteningOfCommand)
            {
                var command = CommandHelper.GetCommand(shorteningOfCommand);

                command.Should().BeOfType(typeof(Commands.Move));
            }

            [TestCase("U")]
            [TestCase(".")]
            public void GetCommand_WhenShorteningCommandIsNotValid_ShouldReturnException(char shorteningOfCommand)
            {
                Assert.Throws(Is.TypeOf<Exception>()
                .And.Message.EqualTo($"Command {shorteningOfCommand} couldn't be found!"),
              () => CommandHelper.GetCommand(shorteningOfCommand));
            }
        }

        [TestFixture]
        public class DirectionHelperTests
        {
            [TestCase("N")]
            public void GetDirection_WhenShorteningDirectionIsN_ShouldBeOfTypeNorth(string shorteningOfDirection)
            {
                var direction = DirectionHelper.GetDirection(shorteningOfDirection);

                direction.Should().BeOfType(typeof(Directions.North));
            }

            [TestCase("E")]
            public void GetDirection_WhenShorteningDirectionIsE_ShouldBeOfTypeEast(string shorteningOfDirection)
            {
                var direction = DirectionHelper.GetDirection(shorteningOfDirection);

                direction.Should().BeOfType(typeof(Directions.East));
            }

            [TestCase("S")]
            public void GetDirection_WhenShorteningDirectionIsS_ShouldBeOfTypeSouth(string shorteningOfDirection)
            {
                var direction = DirectionHelper.GetDirection(shorteningOfDirection);

                direction.Should().BeOfType(typeof(Directions.South));
            }

            [TestCase("W")]
            public void GetDirection_WhenShorteningDirectionIsW_ShouldBeOfTypeWest(string shorteningOfDirection)
            {
                var direction = DirectionHelper.GetDirection(shorteningOfDirection);

                direction.Should().BeOfType(typeof(Directions.West));
            }

            [TestCase("U")]
            [TestCase(".")]
            public void GetDirection_WhenShorteningDirectionIsNotValid_ShouldReturnException(string shorteningOfDirection)
            {
                Assert.Throws(Is.TypeOf<Exception>()
                .And.Message.EqualTo($"Direction {shorteningOfDirection} couldn't be found!"),
              () => DirectionHelper.GetDirection(shorteningOfDirection));
            }
        }
    }
}
