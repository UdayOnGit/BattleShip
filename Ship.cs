namespace Battleship
{
    public class Ship
    {
        public int Length { get; set; }
        public int Hit { get; set; }
        public bool DidShipSink() => Length == Hit;

        public Ship(int length)
        {
            Length = length;
        }
    }
}