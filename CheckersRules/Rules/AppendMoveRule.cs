using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Rules
{
    internal class AppendMoveRule : AbstractRule
    {
        public override string Name => nameof(AppendMoveRule);

        public override void ApplyRule(IGame game, Piece[,] pieces, IHistoryItem latestMove)
        {
            var piece = pieces[latestMove.From.X, latestMove.From.Y];
            piece.X = latestMove.To.X;
            piece.Y = latestMove.To.Y;

            pieces[latestMove.From.X, latestMove.From.Y] = null;
            pieces[latestMove.To.X, latestMove.To.Y] = piece;

            Next(game, pieces, latestMove);
        }
    }
}
