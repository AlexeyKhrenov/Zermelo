using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Rules
{
    internal class InitialPositionRule : AbstractRule
    {
        public override string Name => nameof(InitialPositionRule);

        public override void ApplyRule(IGame game, Piece[,] pieces, IHistoryItem latestMove)
        {
            var size = game.Size;
            var changedSides = game.IsRevertedBoard;
            var player1Figures = game.Player1.Figures;
            var player2Figures = game.Player2.Figures;

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
                    var piece = new Piece(x, y, !isWhite, true, false);
                    pieces[x, y] = piece;
                    player1Figures.Add(piece);
                }
            }

            for (var y = 0; y < size / 2 - 1; y++)
            {
                var startX = 1 - y % 2;
                for (var x = startX; x < size; x += 2)
                {
                    var piece = new Piece(x, y, isWhite, false, true);
                    pieces[x, y] = piece;
                    player2Figures.Add(piece);
                }
            }

            Next(game, pieces, null);
        }

        public override void UndoRule(IGame game, Piece[,] pieces, IHistoryItem toUndo, IHistoryItem lastMoveBeforeUndo)
        {
            throw new InvalidOperationException();
        }
    }
}
