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
            var piece = board.GetPiece(latestMove.To.X, latestMove.To.Y, latestMove.Player);
            if (!piece.IsQueen)
            {
                bool v1 = (piece.CanGoDown && piece.Y == board.GetSize() - 1);
                bool v2 = (piece.CanGoUp && piece.Y == 0);

                if (v1 || v2)
                {
                    piece.IsQueen = true;
                    piece.CanGoDown = true;
                    piece.CanGoUp = true;
                    board.Replace(piece, board.ActivePlayer);
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
                    piece.CanGoUp = false;
                }
                else
                {
                    piece.CanGoDown = false;
                }

                board.Replace(piece, toUndo.Player);
            }

            return NextUndo(board, toUndo, lastMoveBeforeUndo);
        }
    }
}
