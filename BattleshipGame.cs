using System;

namespace Battleship
{
    public class BattleshipGame
    {
        public void Play()
        {
            var battleShips = GetPlayerBattleShips();

            var board = new Board();
            PlaceBattleShipsOnBoard(battleShips, board);

            System.Console.WriteLine("Player 2 fire at will.");
            Fire(board);
        }

        private int GetPlayerBattleShips()
        {
            Console.WriteLine("Welcome player, how many battleships you want to play with?");
            int result = 0;
            var input = Console.ReadLine();
            if (!int.TryParse(input, out result))
            {
                throw new InvalidOperationException("Invalid input.");
            }
            return result;
        }

        private void PlaceBattleShipsOnBoard(int shipCount, Board board)
        {
            for (int nIndex = 0; nIndex < shipCount; nIndex++)
            {
                System.Console.WriteLine($"Enter the co-ordinates(x,y) to place ship number: {nIndex + 1}");
                var input = Console.ReadLine();
                var coordinate = input.Split(',');

                if (coordinate.Length < 2
                    || !int.TryParse(coordinate[0], out var x)
                    || !int.TryParse(coordinate[1], out var y))
                {
                    throw new InvalidOperationException("Invalid input, please try again!");
                }

                System.Console.WriteLine("Enter ship length (1-10)");
                input = Console.ReadLine();
                if (!int.TryParse(input, out var shipLength))
                {
                    throw new InvalidOperationException("Invalid ship length");
                }

                var boardCoordinate = new Coordinate(x, y);
                var ship = new Ship(shipLength);
                if (board.PlaceShipOnBoard(boardCoordinate, ship))
                {
                    System.Console.WriteLine("Ship successfully placed on board");
                }
                else
                {
                    throw new InvalidOperationException("Unable to place ship on board");
                }
            }
        }

        private void Fire(Board board)
        {
            do
            {
                System.Console.WriteLine("Enter attack coordinates as x,y. Hit Esc key to exit.");
                var input = Console.ReadLine();
                var coordinates = input.Split(',');
                if (coordinates.Length < 2
                    || !int.TryParse(coordinates[0], out var x)
                    || !int.TryParse(coordinates[1], out var y))
                {
                    System.Console.WriteLine("Invalid input, please try again");
                }
                else
                {
                    var attackCoordinates = new Coordinate(x, y);
                    var (isItAHit, hasShipSunk) = board.IsItAHit(attackCoordinates);

                    if (isItAHit)
                    {
                        var shipStatusMessage = hasShipSunk ? "You sunk the ship" : "";
                        System.Console.WriteLine($"Yay! It's a hit.{shipStatusMessage}");
                        if(board.HasPlayerLostYet())
                        {
                            System.Console.WriteLine($"Player lost all his ships.{Environment.NewLine}Game over");
                            break;
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("Nah, it's a miss");
                    }
                }

            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}