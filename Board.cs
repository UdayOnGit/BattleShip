using System.Collections.Generic;
using System.Linq;

namespace Battleship
{
    public class Board
    {
        private const int BoardDimension = 10;
        private List<Ship> shipsOnBoard;

        private Cell[,] boardCoordinates;

        public Board()
        {
            InitializeBoard();
        }

        public bool PlaceShipOnBoard(Coordinate cooordinate, Ship ship)
        {
            List<Cell> cells = new List<Cell>();
            for (int index = 0; index < ship.Length; index++)
            {
                cells.Add(boardCoordinates[cooordinate.X + index, cooordinate.Y]);
            }
            if (cells.Any(x => x.Ship != null))
            {
                return false;
            }
            foreach (var cell in cells)
            {
                cell.Ship = ship;
            }
            shipsOnBoard.Add(ship);

            return true;
        }

        public (bool isItAHit, bool didTheShipSink) IsItAHit(Coordinate coordinate)
        {
            var cell = boardCoordinates[coordinate.X, coordinate.Y];
            var hit = cell.Ship != null;
            if (hit)
            {
                cell.Ship.Hit++;
                var ShipSank = cell.Ship.DidShipSink();
                return (hit, ShipSank);
            }
            return (false, false);
        }

        public bool HasPlayerLostYet() => shipsOnBoard.All(x => x.DidShipSink());

        private void InitializeBoard()
        {
            shipsOnBoard = new List<Ship>();
            boardCoordinates = new Cell[BoardDimension, BoardDimension];
            for (int x = 0; x < boardCoordinates.GetLength(0); x++)
            {
                for (int y = 0; y < boardCoordinates.GetLength(1); y++)
                {
                    boardCoordinates[x, y] = new Cell
                    {
                        CellCoordinate = new Coordinate(x, y),
                        Ship = null,
                        IsCellAttackedEarlier = AttackStatus.NotYetAttacked
                    };
                }
            }
        }
    }
}