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
            var piece = board.Pieces[latestMove.From.X, latestMove.From.Y];
            piece.X = latestMove.To.X;
            piece.Y = latestMove.To.Y;
            board.Pieces[latestMove.From.X, latestMove.From.Y] = null;
            board.Pieces[latestMove.To.X, latestMove.To.Y] = piece;

            Next(board, latestMove);
        }

        public override void UndoRule(BoardMinified board, HistoryItemMinified toUndo, HistoryItemMinified lastMoveBeforeUndo)
        {
            var piece = board.Pieces[toUndo.To.X, toUndo.To.Y];
            piece.X = toUndo.From.X;
            piece.Y = toUndo.From.Y;
            board.Pieces[toUndo.To.X, toUndo.To.Y] = null;
            board.Pieces[toUndo.From.X, toUndo.From.Y] = piece;

            NextUndo(board, toUndo, lastMoveBeforeUndo);
        }
    }
}
