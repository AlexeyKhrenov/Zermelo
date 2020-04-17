using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkers.Rules
{
    internal class RemoveCapturedPieceRule : AbstractRule
    {
        public override string Name => nameof(RemoveCapturedPieceRule);

        public override void ApplyRule(IGame game, Piece[,] pieces, IHistoryItem latestMove)
        {
            latestMove.IsKill = latestMove.From.X - latestMove.To.X > 1 || latestMove.From.X - latestMove.To.X < -1;
            if (latestMove.IsKill)
            {
                var capturedPieceX = (latestMove.From.X + latestMove.To.X) / 2;
                var capturedPieceY = (latestMove.From.Y + latestMove.To.Y) / 2;
                var capturedPiece = game.Figures.First(f => f.X == capturedPieceX && f.Y == capturedPieceY);

                pieces[capturedPieceX, capturedPieceY] = null;

                game.Figures.Remove(capturedPiece);
            }

            Next(game, pieces, latestMove);
        }
    }
}
