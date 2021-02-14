using HBMarsRover.Abstractions;
using HBMarsRover.Directions;
using HBMarsRover.Rovers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace HBMarsRover.Tests
{
    public class RoverTests
    {
        [TestCase(1, 2)]
        public void Work_WhenCommandListEmpty_ReturnSamePositionAndDirection(int positionX, int positionY)
        {
            Plateau plateauBase = new Plateau()
            {
                UpperRightX = 5,
                UpperRightY = 5,
            };

            var position = new Position
            {
                Coordinate = new Coordinate(positionX, positionY),
                Direction = new North()
            };

            var rover = new Rover(position)
            {
                CommandList = new List<CommandBase>()
            };

            rover.Work(plateauBase);

            Assert.IsTrue(rover.Position.Coordinate.X == positionX && rover.Position.Coordinate.Y == positionY);
            Assert.AreEqual(rover.Position.Direction.GetType(), typeof(North));
        }

        [Test]
        public void Work_WhenIsInArea_ShouldSetNewPosition()
        {
            Plateau plateauBase = new Plateau()
            {
                UpperRightX = 5,
                UpperRightY = 5,
            };

            var position = new Position
            {
                Coordinate = new Coordinate(1, 2),
                Direction = new North()
            };

            var resultPosition = new Position
            {
                Coordinate = new Coordinate(1, 3),
                Direction = new North()
            };

            Mock<CommandBase> commandBase = new Mock<CommandBase>();
            commandBase.Setup(m => m.Execute(position)).Returns(resultPosition);

            var rover = new Rover(position)
            {
                CommandList = new List<CommandBase>() { commandBase.Object }
            };

            rover.Work(plateauBase);

            Assert.IsTrue(rover.Position.Coordinate.X == resultPosition.Coordinate.X && rover.Position.Coordinate.Y == resultPosition.Coordinate.Y);
            Assert.AreEqual(rover.Position.Direction.GetType(), resultPosition.Direction.GetType());
        }

        [Test]
        public void Work_WhenIsNotInArea_ShouldReturnException()
        {
            Plateau plateauBase = new Plateau()
            {
                UpperRightX = 5,
                UpperRightY = 5,
            };

            var position = new Position
            {
                Coordinate = new Coordinate(4, 5),
                Direction = new North()
            };

            var resultPosition = new Position
            {
                Coordinate = new Coordinate(4, 6),
                Direction = new North()
            };

            Mock<CommandBase> commandBase = new Mock<CommandBase>();
            commandBase.Setup(m => m.Execute(position)).Returns(resultPosition);

            var rover = new Rover(position)
            {
                CommandList = new List<CommandBase>() { commandBase.Object }
            };

            Assert.Throws(Is.TypeOf<Exception>()
                .And.Message.EqualTo("Rover has exited from border of the plateau!"),
              () => rover.Work(plateauBase));
        }
    }
}
