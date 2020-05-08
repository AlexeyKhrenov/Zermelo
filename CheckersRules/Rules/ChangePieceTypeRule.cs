using Checkers.Minifications;
using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkers.Rules
{
    internal class ChangePieceTypeRule : AbstractRule
    {
        public override BoardMinified ApplyRule(BoardMinified board, HistoryItemMinified latestMove)
        {
            var x = latestMove.To.X;
            var y = latestMove.To.Y;

            var piece = board.GetPiece(x, y, latestMove.Player);
            if (!piece.IsQueen)
            {
                bool v1 = (piece.CanGoDown && y == board.GetSize() - 1);
                bool v2 = (piece.CanGoUp && y == 0);

                if (v1 || v2)
                {
                    board.ChangePieceType(x, y, true, true, true, board.ActivePlayer);
                    latestMove.IsPieceChangeType = true;
                }
            }

            return Next(board, latestMove);
        }

        public override BoardMinified UndoRule(BoardMinified board, HistoryItemMinified toUndo, HistoryItemMinified lastMoveBeforeUndo)
        {
            if (toUndo.IsPieceChangeType)
            {
                var piece = board.GetPiece(toUndo.From.X, toUndo.From.Y, toUndo.Player);

                piece.IsQueen = false;
                if (toUndo.To.Y > toUndo.From.Y)
                {
                    board.ChangePieceType(piece.X, piece.Y, true, false, false, toUndo.Player);
                }
                else
                {
                    board.ChangePieceType(piece.X, piece.Y, false, true, false, toUndo.Player);
                }
            }

            return NextUndo(board, toUndo, lastMoveBeforeUndo);
        }
    }
}
