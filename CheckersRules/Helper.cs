using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers
{
    internal static class Helper
    {
        public static Piece[,] ToPieceMatrix(this IList<IFigure> figures, int size)
        {
            var pieces = new Piece[size, size];
            foreach (var figure in figures)
            {
                pieces[figure.X, figure.Y] = figure as Piece;
            }
            return pieces;
        }
    }
}
