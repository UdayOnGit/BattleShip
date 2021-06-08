using System;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = new Board();
            System.Console.WriteLine("How many battleships you want to play with?");
            var battleShipInput = Console.ReadLine();
            if (!int.TryParse(battleShipInput, out var shipCount))
            {
                System.Console.WriteLine("Invalid input. Please enter a number between 1 and 10");
                // Acceept the input
            }
            Console.WriteLine("Alright then, lets place the ships on board");
            for (int index = 0; index < shipCount; index++)
            {
                System.Console.WriteLine($"Enter the co-ordinate to place the ship at and ship's length in format (x,y,length) for ship no: {index}");
                var input = Console.ReadLine();
                var shipData = input.Split(',');
                if (shipData.Length < 3
                || !int.TryParse(shipData[0], out var x)
                || !int.TryParse(shipData[1], out var y)
                || !int.TryParse(shipData[2], out var length))
                {
                    // Invalid input exit the game or ask again for input
                    throw new InvalidOperationException();
                }
                var coOrdinate = new Coordinate(x, y);
                var ship = new Ship(length);
                if (!board.PlaceShip(coOrdinate, ship))
                {
                    throw new InvalidOperationException("Unable to place ship on board. Coordinate already occupied");
                }
            }
            System.Console.WriteLine($"Player 2, here is your chance to attack the opponent.");
            do
            {
                System.Console.WriteLine("Enter attack coordinates as x,y. Hit Esc key to exit.");
                var input = Console.ReadLine();
                var inputCoordinates = input.Split(',');
                if (inputCoordinates.Length < 2
                    || !int.TryParse(inputCoordinates[0], out var x)
                    || !int.TryParse(inputCoordinates[1], out var y))
                {
                    System.Console.WriteLine("Invalid input, please try again");
                }
                else
                {
                    var attackCoordinates = new Coordinate(x, y);
                    var result = board.IsItAHit(attackCoordinates);
                    System.Console.WriteLine(result ? "Yay! It's a hit" : "Nah missed it");
                }

            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }

    public enum CoordinateState
    {
        Vacant = 0,
        Filled = 1
    }

    public class Coordinate
    {
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }
    }

    public class Board
    {
        private CoordinateState[,] boardCoordinates;

        public Board()
        {
            InitializeBoard();
        }

        public bool PlaceShip(Coordinate cooordinate, Ship ship)
        {
            var cell = boardCoordinates[cooordinate.X, cooordinate.Y];
            if (cell == CoordinateState.Filled)
            {
                return false;
            }
            for (int index = 1; index < ship.Length; index++)
            {
                if (boardCoordinates[cooordinate.X + index, cooordinate.Y] == CoordinateState.Filled)
                {
                    return false;
                }
            }
            for (int index = 0; index < ship.Length; index++)
            {
                boardCoordinates[cooordinate.X + index, cooordinate.Y] = CoordinateState.Filled;
            }
            return true;
        }

        public bool IsItAHit(Coordinate coordinate) => boardCoordinates[coordinate.X, coordinate.Y] == CoordinateState.Filled;

        private void InitializeBoard()
        {
            boardCoordinates = new CoordinateState[10, 10];
            for (int x = 0; x < boardCoordinates.GetLength(0); x++)
            {
                for (int y = 0; y < boardCoordinates.GetLength(1); y++)
                {
                    boardCoordinates[x, y] = CoordinateState.Vacant;
                }
            }
        }
    }

    public class Ship
    {
        public int Length { get; set; }

        public Ship(int length)
        {
            Length = length;
        }
    }
}
