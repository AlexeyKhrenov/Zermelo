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
            if (!piece.IsQueen)
            {
                bool v1 = (piece.CanGoDown && piece.Y == game.Size - 1);
                bool v2 = (piece.CanGoUp && piece.Y == 0);

                if (v1 || v2)
                {
                    piece.IsQueen = true;
                    piece.CanGoDown = true;
                    piece.CanGoUp = true;
                    latestMove.IsPieceChangeType = true;
                }
            }

            Next(game, pieces, latestMove);
        }
    }
}
