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

        public int FigureType { get; set; }

        public Point[] AvailableMoves { get; set; }

        public PieceTypes _type;

        public string Type => _type.ToString();

        public Piece(int x, int y, PieceTypes type)
        {
            X = x;
            Y = y;
            _type = type;
        }
    }
}
