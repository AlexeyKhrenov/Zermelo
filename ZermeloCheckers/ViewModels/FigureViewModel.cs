using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;

using Game.PublicInterfaces;

namespace ZermeloCheckers.ViewModels
{
    public class FigureViewModel
    {
        public int X;
        public int Y;
        public string Type;

        public delegate void FigureMovedHandler(object sender, int x0, int y0, int x1, int y1);
        public event FigureMovedHandler FigureMoved;

        public List<Point> _all;
        public List<Point> AllowedMoves
        {
            get
            {
                return _all;
            }

            set
            {
                _all = value;
            }
        }

        public bool HasCoordinates(int x, int y)
        {
            return X == x && Y == y;
        }
         
        public bool TryMoveFigure(int x1, int y1)
        {
            if (IsMoveAllowed(x1, y1))
            {
                var x0 = X;
                var y0 = Y;

                FigureMoved(null, x0, y0, x1, y1);
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
