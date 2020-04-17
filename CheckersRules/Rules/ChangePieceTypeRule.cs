using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Rules
{
    internal class ChangePieceTypeRule : AbstractRule
    {
        public override string Name => nameof(ChangePieceTypeRule);

        public override void ApplyRule(IGame game, Piece[,] pieces, IHistoryItem latestMove)
        {
            var piece = pieces[latestMove.To.X, latestMove.To.Y];
            if (piece.IsQueen)
            {
                return;
            }

            if(
                (piece.CanGoDown && piece.Y == game.Size - 1) ||
                (piece.CanGoUp && piece.Y == 0)
            )
            {
                piece.IsQueen = true;
                latestMove.IsPieceChangeType = true;
            }

            Next(game, pieces, latestMove);
        }
    }
}
