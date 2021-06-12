namespace Battleship.Models
{
    public class Cell
    {
        public Coordinate CellCoordinate { get; set; }
        public bool IsOccupied { get; set; }
        public string ShipName { get; set; }
    }
}