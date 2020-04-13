using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Game.PublicInterfaces;

namespace Checkers
{
    internal class CheckersRules : IGameRules
    {
        public int Size { get; set; }

        public List<IFigure> CreateInitialPosition(int size, bool changedSides)
        {
            Size = size;

            var figures = new List<IFigure>();

            if (size < 4)
            {
                throw new NotImplementedException("Game size smaller than 4");
            }

            // positioning pieces
            var firstColor = changedSides ? PieceTypes.Black : PieceTypes.White;
            for (var y = size - 1; y > size / 2; y--)
            {
                var startX = 1 - y % 2;
                for (var x = startX; x < size; x += 2)
                {
                    figures.Add(new Piece(x, y, firstColor));
                }
            }

            var secondColor = changedSides ? PieceTypes.White : PieceTypes.Black;
            for (var y = 0; y < size / 2 - 1; y++)
            {
                var startX = 1 - y % 2;
                for (var x = startX; x < size; x += 2)
                {
                    figures.Add(new Piece(x, y, secondColor));
                }
            }

            EvaluateAllowedMoves(figures, size);

            return figures;
        }

        public void MakeMove(IGame game, int x0, int y0, int x1, int y1)
        {
            var requiredFigure = game.Figures.FirstOrDefault(f => f.X == x0 && f.Y == y0);
            if (requiredFigure == null)
            {
                throw new InvalidOperationException();
            }

            requiredFigure.X = x1;
            requiredFigure.Y = y1;

            EvaluateAllowedMoves(game.Figures, game.Size);
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }

        private static void EvaluateAllowedMoves(IList<IFigure> figures, int size)
        {
            foreach (var figure in figures)
            {
                var allowedMoves = new List<Point>();

                allowedMoves.Add(new Point(figure.X + 1, figure.Y + 1));
                allowedMoves.Add(new Point(figure.X - 1, figure.Y - 1));
                allowedMoves.Add(new Point(figure.X + 1, figure.Y - 1));
                allowedMoves.Add(new Point(figure.X - 1, figure.Y + 1));

                allowedMoves.Add(new Point(figure.X + 1, figure.Y));
                allowedMoves.Add(new Point(figure.X - 1, figure.Y));
                allowedMoves.Add(new Point(figure.X, figure.Y + 1));
                allowedMoves.Add(new Point(figure.X, figure.Y - 1));

                allowedMoves.RemoveAll(x => x.X < 0 || x.Y < 0 || x.X >= size || x.Y >= size);

                foreach (var neigh in figures)
                {
                    allowedMoves.RemoveAll(x => x.X == neigh.X && x.Y == neigh.Y);
                }

                figure.AvailableMoves = allowedMoves.ToArray();
            }
        }
    }
}
