namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            var console = new ConsoleWrapper();
            var game = new BattleshipGame(console);
            game.Play();
        }
    }
}
