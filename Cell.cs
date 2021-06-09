namespace Battleship
{
    public class Cell
    {
        public Coordinate CellCoordinate { get; set; }
        public Ship Ship { get; set; }
        public AttackStatus IsCellAttackedEarlier { get; set; }
    }
}