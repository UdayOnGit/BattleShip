using System;
using Battleship.Models;

namespace Battleship
{
    public class BattleshipGame
    {
        public void Play()
        {
            System.Console.WriteLine($"Welcome to Battleship game.{Environment.NewLine}Enter player name");
            var playerName = Console.ReadLine();
            var player = new Player(playerName);
            var playerShipCount = GetPlayersShips();
            player.PlaceShipsOnBoard(playerShipCount);

            TakeFire(player);
        }

        private int GetPlayersShips()
        {
            Console.WriteLine("How many battleships you want to play with?");
            int result = 0;
            var input = Console.ReadLine();
            if (!int.TryParse(input, out result))
            {
                throw new InvalidOperationException("Invalid input.");
            }
            return result;
        }

        private void TakeFire(Player player)
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
                    var (isItAHit, isSunk) = player.TakeHit(attackCoordinates);

                    if (isItAHit)
                    {
                        var shipStatusMessage = isSunk ? "You sunk the ship" : "";
                        System.Console.WriteLine($"Yay! It's a hit.{shipStatusMessage}");
                        if (player.DidILoose())
                        {
                            System.Console.WriteLine($"{player.PlayerName} lost all his ships.{Environment.NewLine}Game over");
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