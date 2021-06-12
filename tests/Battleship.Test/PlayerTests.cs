using System;
using System.Collections.Generic;
using Battleship.Models;
using Xunit;
using FluentAssertions;

namespace Battleship.Test
{
    public class PlayerTests
    {
        [Fact]
        public void Should_Take_Hit_And_Return_Correct_Coordinate_Status()
        {
            // Arrange
            var console = new ConsoleTestWrapper();
            var player = new Player("Player1", console);
            var coordinate = new Coordinate(1, 2);

            // Act
            player.PlaceShipsOnBoard(1);

            // Assert
            var (isHit, isSunk) = player.TakeHit(coordinate);
            isHit.Should().BeTrue();
            isSunk.Should().BeFalse();

            coordinate = new Coordinate(1, 1);
            (isHit, isSunk) = player.TakeHit(coordinate);
            isHit.Should().BeFalse();
            isSunk.Should().BeFalse();

            coordinate = new Coordinate(2, 2);
            (isHit, isSunk) = player.TakeHit(coordinate);
            isHit.Should().BeTrue();
            isSunk.Should().BeTrue();
        }

        [Fact]
        public void Should_Take_Hit_And_Return_If_Player_Looses()
        {
            // Arrange
            var console = new ConsoleTestWrapper();
            var player = new Player("Player1", console);
            var coordinate = new Coordinate(1, 2);

            // Act
            player.PlaceShipsOnBoard(1);

            // Assert
            var (isHit, isSunk) = player.TakeHit(coordinate);
            isHit.Should().BeTrue();
            isSunk.Should().BeFalse();

            coordinate = new Coordinate(2, 2);
            (isHit, isSunk) = player.TakeHit(coordinate);
            isHit.Should().BeTrue();
            isSunk.Should().BeTrue();

            player.DidILoose().Should().BeTrue();
        }
    }

    public class ConsoleTestWrapper : IConsole
    {
        public List<string> LinesToRead = new List<String>();

        public ConsoleTestWrapper()
        {
            LinesToRead.Add("Horizontal");
            LinesToRead.Add("1,2");
            LinesToRead.Add("2");
            LinesToRead.Add("Ship1");
        }

        public string ReadLine()
        {
            string result = LinesToRead[0];
            LinesToRead.RemoveAt(0);
            return result;
        }

        public void WriteLine(string message)
        {
        }
    }
}