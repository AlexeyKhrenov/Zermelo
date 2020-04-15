using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using Game.PublicInterfaces;

namespace Checkers
{
    internal class Piece : IFigure
    {
        public int X { get; set; }

        public int Y { get; set; }

        public List<Point> AvailableMoves { get; set; }

        // todo - convert to smart properties
        public bool CanGoUp { get; set; }

        // todo - convert to smart properties
        public bool CanGoDown { get; set; }

        public bool IsWhite { get; set; }

        public bool IsQueen { get; set; }

        public string Type {
            get
            {
                if (IsWhite)
                {
                    if (IsQueen)
                    {
                        return PieceTypes.BlackQueen.ToString();
                    }
                    else
                    {
                        return PieceTypes.Black.ToString();
                    }
                }
                else
                {
                    if (IsQueen)
                    {
                        return PieceTypes.WhiteQueen.ToString();
                    }
                    else
                    {
                        return PieceTypes.White.ToString();
                    }
                }
            }
        }

        public Piece(int x, int y, bool isWhite, bool canGoUp, bool canGoDown)
        {
            X = x;
            Y = y;
            IsWhite = isWhite;
            CanGoDown = canGoDown;
            CanGoUp = canGoUp;
            AvailableMoves = new List<Point>();
        }
    }
}
