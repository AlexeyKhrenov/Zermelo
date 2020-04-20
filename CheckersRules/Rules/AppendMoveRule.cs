using Checkers.Minifications;
using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkers.Rules
{
    internal class AppendMoveRule : AbstractRule
    {
        public override void ApplyRule(BoardMinified board, HistoryItemMinified latestMove)
        {
            board.MovePiece(latestMove.From.X, latestMove.From.Y, latestMove.To.X, latestMove.To.Y);
            Next(board, latestMove);
        }

        public override void UndoRule(BoardMinified board, HistoryItemMinified toUndo, HistoryItemMinified lastMoveBeforeUndo)
        {
            board.MovePiece(toUndo.To.X, toUndo.To.Y, toUndo.From.X, toUndo.From.Y);
            NextUndo(board, toUndo, lastMoveBeforeUndo);
        }
    }
}
