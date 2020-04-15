using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Rules
{
    internal class InitialPositionRule : AbstractRule
    {
        public override string Name => nameof(InitialPositionRule);

        public override void ApplyRule(IGame game, Piece[,] pieces)
        {
            var size = game.Size;
            var changedSides = game.IsRevertedBoard;
            var figures = game.Figures;

            if (size < 4)
            {
                throw new NotImplementedException("Game size smaller than 4");
            }

            // positioning pieces
            var isWhite = !changedSides;
            for (var y = size - 1; y > size / 2; y--)
            {
                var startX = 1 - y % 2;
                for (var x = startX; x < size; x += 2)
                {
                    figures.Add(new Piece(x, y, !isWhite, true, false));
                }
            }

            for (var y = 0; y < size / 2 - 1; y++)
            {
                var startX = 1 - y % 2;
                for (var x = startX; x < size; x += 2)
                {
                    figures.Add(new Piece(x, y, isWhite, false, true));
                }
            }

            game.Figures = figures;

            PassControlToNext(game, figures.ToPieceMatrix(size));
        }
    }
}
