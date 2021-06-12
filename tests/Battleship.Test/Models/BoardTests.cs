using Battleship.Models;
using Xunit;
using FluentAssertions;
using System;

namespace Battleship.Test
{
    public class BoardTests
    {
        [Fact]
        public void Should_Initialize_Board_Properly()
        {
            // Arrange
            var board = new Board();

            // Act and Assert
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    var coordinate = new Coordinate(x, y);
                    var cell = board.GetBoardCell(coordinate);

                    cell.IsOccupied.Should().BeFalse();
                    cell.ShipName.Should().BeNullOrEmpty();
                }
            }
        }

        [Fact]
        public void Should_Throw_InvalidException_For_Ship_Place_OutofBound()
        {
            // Arrange
            var board = new Board();
            var coordinate = new Coordinate(-1, -1);
            var ship = new Ship(2, "Ship1");

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => board.PlaceShipOnBoard(coordinate, ShipDirection.Horizontal, ship));
        }

        [Fact]
        public void Should_Place_Ship_Horizontally_On_Board()
        {
            // Arrange
            var board = new Board();
            var coordinate = new Coordinate(1, 2);
            var ship = new Ship(2, "Ship1");

            // Act
            board.PlaceShipOnBoard(coordinate, ShipDirection.Horizontal, ship);

            // Assert
            var cell = board.GetBoardCell(coordinate);
            cell.IsOccupied.Should().BeTrue();
            cell.ShipName.Should().Be(ship.Name);

            coordinate = new Coordinate(2, 2);
            cell = board.GetBoardCell(coordinate);
            cell.IsOccupied.Should().BeTrue();
            cell.ShipName.Should().Be(ship.Name);

            coordinate = new Coordinate(2, 5);
            cell = board.GetBoardCell(coordinate);
            cell.IsOccupied.Should().BeFalse();
            cell.ShipName.Should().BeNullOrEmpty();
        }

        [Fact]
        public void Should_Place_Ship_Vertically_On_Board()
        {
            // Arrange
            var board = new Board();
            var coordinate = new Coordinate(1, 2);
            var ship = new Ship(2, "Ship1");

            // Act
            board.PlaceShipOnBoard(coordinate, ShipDirection.Vertical, ship);

            // Assert
            var cell = board.GetBoardCell(coordinate);
            cell.IsOccupied.Should().BeTrue();
            cell.ShipName.Should().Be(ship.Name);

            coordinate = new Coordinate(1, 3);
            cell = board.GetBoardCell(coordinate);
            cell.IsOccupied.Should().BeTrue();
            cell.ShipName.Should().Be(ship.Name);

            coordinate = new Coordinate(2, 3);
            cell = board.GetBoardCell(coordinate);
            cell.IsOccupied.Should().BeFalse();
            cell.ShipName.Should().BeNullOrEmpty();
        }

    }
}