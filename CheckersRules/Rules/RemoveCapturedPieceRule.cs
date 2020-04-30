using Checkers.Minifications;
using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkers.Rules
{
    internal class RemoveCapturedPieceRule : AbstractRule
    {
        public override BoardMinified ApplyRule(BoardMinified board, HistoryItemMinified latestMove)
        {
            if (latestMove.IsKill)
            {
                var capturedPieceX = (latestMove.From.X + latestMove.To.X) / 2;
                var capturedPieceY = (latestMove.From.Y + latestMove.To.Y) / 2;

                latestMove.Captured = board.RemovePiece(capturedPieceX, capturedPieceY, !board.ActivePlayer);
            }

            return Next(board, latestMove);
        }

        public override BoardMinified UndoRule(BoardMinified board, HistoryItemMinified toUndo, HistoryItemMinified lastMoveBeforeUndo)
        {
            if (toUndo.IsKill)
            {
                var captured = toUndo.Captured; 
                board.RestorePiece(captured, !toUndo.Player);
            }

            return NextUndo(board, toUndo, lastMoveBeforeUndo);
        }
    }
}
