using System;

namespace Battleship.Models
{
    public class Board
    {
        private int BoardDimension = 10;
        private Cell[,] _board;

        public Board()
        {
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            _board = new Cell[BoardDimension, BoardDimension];
            for (int x = 0; x < BoardDimension; x++)
            {
                for (int y = 0; y < BoardDimension; y++)
                {
                    _board[x, y] = new Cell
                    {
                        CellCoordinate = new Coordinate(x, y),
                        IsOccupied = false
                    };
                }
            }
        }

        public void PlaceShipOnBoard(Coordinate coordinate, ShipDirection direction, Ship ship)
        {
            if (IsShipWithinBoardBounds(coordinate, direction, ship))
            {
                if (direction == ShipDirection.Vertical)
                {
                    for (int index = 0; index < ship.Length; index++)
                    {
                        int verticalIndex = coordinate.Y + index;
                        _board[coordinate.X, verticalIndex].IsOccupied = true;
                        _board[coordinate.X, verticalIndex].ShipName = ship.Name;
                    }
                }
                else
                {
                    for (int index = 0; index < ship.Length; index++)
                    {
                        int horizontalIndex = coordinate.X + index;
                        _board[horizontalIndex, coordinate.Y].IsOccupied = true;
                        _board[horizontalIndex, coordinate.Y].ShipName = ship.Name;
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("Ship placed outside the board");
            }

        }

        private bool IsShipWithinBoardBounds(Coordinate initialCoordinate, ShipDirection direction, Ship ship)
        {
            if(initialCoordinate.X < 0 || initialCoordinate.Y < 0)
            {
                return false;
            }
            
            var coordinate = direction == ShipDirection.Vertical ? initialCoordinate.Y: initialCoordinate.X;
            return BoardDimension > coordinate + ship.Length;
        }

        public Cell GetBoardCell(Coordinate coordinate) => _board[coordinate.X, coordinate.Y];
    }
}