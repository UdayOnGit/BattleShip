using System;
using Battleship.Models;

namespace Battleship
{
    public class BattleshipGame
    {
        private readonly IConsole _console;
        public BattleshipGame(IConsole console)
        {
            _console = console;
        }
        public void Play()
        {
            _console.WriteLine($"Welcome to Battleship game.{Environment.NewLine}Enter player name");
            var playerName = Console.ReadLine();
            var player = new Player(playerName, _console);
            var playerShipCount = GetPlayersShips();
            player.PlaceShipsOnBoard(playerShipCount);

            TakeFire(player);
        }

        private int GetPlayersShips()
        {
            _console.WriteLine("How many battleships you want to play with?");
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
                _console.WriteLine("Enter attack coordinates as x,y. Hit Esc key to exit.");
                var input = Console.ReadLine();
                var coordinates = input.Split(',');
                if (coordinates.Length < 2
                    || !int.TryParse(coordinates[0], out var x)
                    || !int.TryParse(coordinates[1], out var y))
                {
                    _console.WriteLine("Invalid input, please try again");
                }
                else
                {
                    var attackCoordinates = new Coordinate(x, y);
                    var (isItAHit, isSunk) = player.TakeHit(attackCoordinates);

                    if (isItAHit)
                    {
                        var shipStatusMessage = isSunk ? "You sunk the ship" : "";
                        _console.WriteLine($"Yay! It's a hit.{shipStatusMessage}");
                        if (player.DidILoose())
                        {
                            _console.WriteLine($"{player.PlayerName} lost all his ships.{Environment.NewLine}Game over");
                            break;
                        }
                    }
                    else
                    {
                        _console.WriteLine("Nah, it's a miss");
                    }
                }

            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}