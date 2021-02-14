using HBMarsRover.Parsers;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace HBMarsRover.Tests
{
    public class CommandParserTests
    {
        #region ExceptionTests

        [Test]
        public void Parse_WhenCommandSetIsNull_ShouldReturnException()
        {
            var commandParser = new CommandParser(null);

            Assert.Throws(Is.TypeOf<Exception>()
                .And.Message.EqualTo("Your input doesn't contain valid command sets!"),
              () => commandParser.Parse());
        }

        [Test]
        public void Parse_WhenCommandSetCountLessThanThree_ShouldReturnException()
        {
            var commandSets = new List<string>()
            {
                "55", ""
            };

            var commandParser = new CommandParser(commandSets);

            Assert.Throws(Is.TypeOf<Exception>()
                .And.Message.EqualTo("Your input doesn't contain valid command sets!"),
              () => commandParser.Parse());
        }

        [Test]
        public void Parse_WhenCommandSetCountIsEven_ShouldReturnException()
        {
            var commandSets = new List<string>()
            {
                "55", "", "", ""
            };

            var commandParser = new CommandParser(commandSets);

            Assert.Throws(Is.TypeOf<Exception>()
                .And.Message.EqualTo("Your command sets are lacked!"),
              () => commandParser.Parse());
        }

        [Test]
        public void Parse_WhenSplittedBorderCommandLengthNotEqualToTwo_ShouldReturnException()
        {
            var commandSets = new List<string>()
            {
                "55", "", ""
            };

            var commandParser = new CommandParser(commandSets);

            Assert.Throws(Is.TypeOf<Exception>()
                .And.Message.EqualTo("Upper-right coordinates are invalid!"),
              () => commandParser.Parse());
        }

        [Test]
        public void Parse_WhenUpperRightXNotAnInteger_ShouldReturnException()
        {
            var commandSets = new List<string>()
            {
                "A 5", "", ""
            };

            var commandParser = new CommandParser(commandSets);

            Assert.Throws(Is.TypeOf<Exception>()
                .And.Message.EqualTo("Upper-right X coordinate is invalid!"),
              () => commandParser.Parse());
        }

        [Test]
        public void Parse_WhenUpperRightXLessThanOrEqualToZero_ShouldReturnException()
        {
            var commandSets = new List<string>()
            {
                "0 5", "", ""
            };

            var commandParser = new CommandParser(commandSets);

            Assert.Throws(Is.TypeOf<Exception>()
                .And.Message.EqualTo("The lower-left X coordinate is limited by 0. It's not valid."),
              () => commandParser.Parse());
        }

        [Test]
        public void Parse_WhenUpperRightYNotAnInteger_ShouldReturnException()
        {
            var commandSets = new List<string>()
            {
                "5 .", "", ""
            };

            var commandParser = new CommandParser(commandSets);

            Assert.Throws(Is.TypeOf<Exception>()
                .And.Message.EqualTo("Upper-right Y coordinate is invalid!"),
              () => commandParser.Parse());
        }

        [Test]
        public void Parse_WhenUpperRightYLessThanOrEqualToZero_ShouldReturnException()
        {
            var commandSets = new List<string>()
            {
                "5 -1", "", ""
            };

            var commandParser = new CommandParser(commandSets);

            Assert.Throws(Is.TypeOf<Exception>()
                .And.Message.EqualTo("The lower-left Y coordinate is limited by 0. It's not valid."),
              () => commandParser.Parse());
        }

        [Test]
        public void Parse_WhenSplittedPositionCommandLengthNotEqualToThree_ShouldReturnException()
        {
            var commandSets = new List<string>()
            {
                "5 5", "1 2N", ""
            };

            var commandParser = new CommandParser(commandSets);

            Assert.Throws(Is.TypeOf<Exception>()
                .And.Message.EqualTo("Rover's position information is invalid!"),
              () => commandParser.Parse());
        }

        [Test]
        public void Parse_WhenLatitudeOfPositionCommandNotAnInteger_ShouldReturnException()
        {
            var commandSets = new List<string>()
            {
                "5 5", "A 2 N", ""
            };

            var commandParser = new CommandParser(commandSets);

            Assert.Throws(Is.TypeOf<Exception>()
                .And.Message.EqualTo("Rover's latitude coordinate is invalid!"),
              () => commandParser.Parse());
        }

        [Test]
        public void Parse_WhenLongitudeOfPositionCommandNotAnInteger_ShouldReturnException()
        {
            var commandSets = new List<string>()
            {
                "5 5", "1 . N", ""
            };

            var commandParser = new CommandParser(commandSets);

            Assert.Throws(Is.TypeOf<Exception>()
                .And.Message.EqualTo("Rover's longitude coordinate is invalid!"),
              () => commandParser.Parse());
        }

        [Test]
        public void Parse_WhenDirectionCommandNotValid_ShouldReturnException()
        {
            string direction = ".";
            var commandSets = new List<string>()
            {
                "5 5", $"1 2 {direction}", "LMLM"
            };

            var commandParser = new CommandParser(commandSets);

            Assert.Throws(Is.TypeOf<Exception>()
                .And.Message.EqualTo($"Direction {direction} couldn't be found!"),
              () => commandParser.Parse());
        }

        [Test]
        public void Parse_WhenMovementCommandStrContainsNotValidCommand_ShouldReturnException()
        {
            char notValidChar = 'X';
            var commandSets = new List<string>()
            {
                "5 5", "1 2 N", $"LM{notValidChar}LMM"
            };

            var commandParser = new CommandParser(commandSets);

            Assert.Throws(Is.TypeOf<Exception>()
                .And.Message.EqualTo($"Command {notValidChar} couldn't be found!"),
              () => commandParser.Parse());
        }

        #endregion ExceptionTests

        [Test]
        public void Parse_WhenCommandsAreAvailableForParsingRules_CouldBeParsed()
        {
            var commandSets = new List<string>()
            {
                "5 5", "1 2 N", "LMLMLMLMMMMMMMMMMMM"
            };

            var commandParser = new CommandParser(commandSets);
            commandParser.Parse();

            Assert.IsTrue(true);
        }

        [TestCase("5 5", "1 2 N", "LMLMLMLMM")]
        [TestCase("5 5", "1 2 E", "LMLMLMLMMMMM")]
        public void Parse_WhenCommandsAreAvailableForOnlyOneRover_CouldBeParsed(string plateauLimitCoordinates, string positionCommand, string movementCommand)
        {
            var commandSets = new List<string>()
            {
                plateauLimitCoordinates, positionCommand, movementCommand
            };

            var commandParser = new CommandParser(commandSets);
            commandParser.Parse();

            Assert.IsTrue(true);
        }

        [Test]
        public void Parse_WhenCommandsAreAvailableForMultipleRovers_CouldBeParsed()
        {
            var commandSets = new List<string>()
            {
                "5 5", "1 2 N", "LMLMLMLMM", "3 3 E", "MMRMMRMRRM"
            };

            var commandParser = new CommandParser(commandSets);
            commandParser.Parse();

            Assert.IsTrue(true);
        }

        [Test]
        public void Parse_WhenCommandCountIsEqualToFive_ShouldContainTwoRovers()
        {
            var commandSets = new List<string>()
            {
                "5 5", "1 2 N", "LMLMLMLMM", "3 3 E", "MMRMMRMRRM"
            };

            var commandParser = new CommandParser(commandSets);
            var plateau = commandParser.Parse();

            Assert.IsTrue(plateau.RoverList.Count == 2);
        }
    }
}
