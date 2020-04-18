using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers
{
    internal static class Helper
    {
        public static Piece[,] ToPieceMatrix(int size, IList<IFigure> player1Figures, IList<IFigure> player2Figures)
        {
            var pieces = new Piece[size, size];

            foreach (var figure in player1Figures)
            {
                pieces[figure.X, figure.Y] = figure as Piece;
            }

            foreach (var figure in player2Figures)
            {
                pieces[figure.X, figure.Y] = figure as Piece;
            }
            return pieces;
        }
    }
}
