namespace Battleship
{
    public class Ship
    {
        public int Length { get; set; }
        public int Hit { get; set; }
        public bool IsSunk => Length == Hit;
        public string Name { get; set; }

        public Ship(int length, string name)
        {
            Length = length;
            Name = name;
        }
    }
}