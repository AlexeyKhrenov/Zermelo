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

                var capturedPiece = game.AwaitingPlayer.Figures.First(f => f.X == capturedPieceX && f.Y == capturedPieceY);

                pieces[capturedPieceX, capturedPieceY] = null;

                game.AwaitingPlayer.Figures.Remove(capturedPiece);
                latestMove.Captured = capturedPiece;
            }

            Next(game, pieces, latestMove);
        }

        public override void UndoRule(IGame game, Piece[,] pieces, IHistoryItem toUndo, IHistoryItem lastMoveBeforeUndo)
        {
            if (toUndo.IsKill)
            {
                var captured = toUndo.Captured;
                if (toUndo.Player == game.Player1)
                {
                    game.Player2.Figures.Add(captured);
                }
                else
                {
                    game.Player1.Figures.Add(captured);
                }
                
                // todo - avoid unboxing here
                pieces[captured.X, captured.Y] = captured as Piece;
            }

            NextUndo(game, pieces, toUndo, lastMoveBeforeUndo);
        }
    }
}
