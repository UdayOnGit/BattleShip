using System.Collections.Generic;
using System.Linq;
using Battleship.Models;
using System;

namespace Battleship
{
    public class Player
    {
        public string PlayerName { get; set; }
        private Board _board;
        private List<Ship> _ships;
        private ShipDirection _playersShipDirection;
        private IConsole _console;

        public Player(string name, IConsole console)
        {
            PlayerName = name;
            _ships = new List<Ship>();
            _board = new Board();
            _console = console;
        }

        public void PlaceShipsOnBoard(int shipCount)
        {
            _console.WriteLine("How do you want to align ships on board (Vertical/Horizontal)");
            var direction = _console.ReadLine();
            if (!Enum.TryParse(direction, out _playersShipDirection))
            {
                throw new InvalidOperationException("Invalid ship direction");
            }

            for (int nIndex = 0; nIndex < shipCount; nIndex++)
            {
                _console.WriteLine($"Enter the co-ordinates(x,y) to place ship number: {nIndex + 1}");
                var input = _console.ReadLine();
                var coordinate = input.Split(',');

                if (coordinate.Length < 2
                    || !int.TryParse(coordinate[0], out var x)
                    || !int.TryParse(coordinate[1], out var y))
                {
                    throw new InvalidOperationException("Invalid input, please try again!");
                }
                var boardCoordinate = new Coordinate(x, y);

                System.Console.WriteLine("Enter ship length (1-10)");
                input = _console.ReadLine();
                if (!int.TryParse(input, out var shipLength))
                {
                    throw new InvalidOperationException("Invalid ship length");
                }
                System.Console.WriteLine("Enter ship name");
                var shipName = _console.ReadLine();
                var ship = new Ship(shipLength, shipName);

                _board.PlaceShipOnBoard(boardCoordinate, _playersShipDirection, ship);
                _ships.Add(ship);
            }
        }

        public (bool isItAHit, bool isSunk) TakeHit(Coordinate coordinate)
        {
            var cell = _board.GetBoardCell(coordinate);
            if (cell.IsOccupied)
            {
                cell.IsOccupied = false;
                var ship = _ships
                    .First(s => s.Name.Equals(cell.ShipName, StringComparison.OrdinalIgnoreCase));
                ship.Hit++;
                return (true, ship.IsSunk);
            }
            else
            {
                return (false, false);
            }
        }

        public bool DidILoose() => _ships.All(x => x.IsSunk);
    }
}