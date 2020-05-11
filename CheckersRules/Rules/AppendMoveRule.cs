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
        public override BoardMinified ApplyRule(BoardMinified board, HistoryItemMinified latestMove)
        {
            board.MovePiece(latestMove.From.X, latestMove.From.Y, latestMove.To.X, latestMove.To.Y, board.ActivePlayer);
            return Next(board, latestMove);
        }

        public override BoardMinified UndoRule(BoardMinified board, HistoryItemMinified toUndo, HistoryItemMinified lastMoveBeforeUndo)
        {
            board.MovePiece(toUndo.To.X, toUndo.To.Y, toUndo.From.X, toUndo.From.Y, toUndo.Player);
            return NextUndo(board, toUndo, lastMoveBeforeUndo);
        }
    }
}
