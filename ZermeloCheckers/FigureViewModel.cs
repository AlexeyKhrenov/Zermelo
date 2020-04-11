using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ZermeloCheckers
{
    public class FigureViewModel
    {
        public int X;
        public int Y;
        public FigureType Type;

        public delegate void FigureMovedHandler(object sender, FigureViewModel figure);
        public event FigureMovedHandler FigureMoved;

        public delegate void FigureTypeChangedHandler(object sender, FigureViewModel figure);
        public event FigureTypeChangedHandler FigureTypeChanged;

        public Point[] AllowedMoves;

        public bool HasCoordinates(int x, int y)
        {
            return X == x && Y == y;
        }

        public bool TryMoveFigure(int x, int y)
        {
            if (IsMoveAllowed(x, y))
            {
                X = x;
                Y = y;
                FigureMoved(null, this);
                return true;
            }

            return false;
        }

        public bool IsMoveAllowed(int x, int y)
        {
            return AllowedMoves != null && AllowedMoves.Any(p => p.X == x && p.Y == y);
        }
    }
}
