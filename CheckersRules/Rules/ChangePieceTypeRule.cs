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
        public override void ApplyRule(BoardMinified board, HistoryItemMinified latestMove)
        {
            var piece = board.Pieces[latestMove.To.X, latestMove.To.Y];
            if (!piece.IsQueen)
            {
                bool v1 = (piece.CanGoDown && piece.Y == board.GetSize() - 1);
                bool v2 = (piece.CanGoUp && piece.Y == 0);

                if (v1 || v2)
                {
                    piece.IsQueen = true;
                    piece.CanGoDown = true;
                    piece.CanGoUp = true;
                    latestMove.IsPieceChangeType = true;
                }
            }

            Next(board, latestMove);
        }

        public override void UndoRule(BoardMinified board, HistoryItemMinified toUndo, HistoryItemMinified lastMoveBeforeUndo)
        {
            if (toUndo.IsPieceChangeType)
            {
                var piece = board.Pieces[toUndo.From.X, toUndo.From.Y];

                piece.IsQueen = false;
                if (toUndo.To.Y > toUndo.From.Y)
                {
                    piece.CanGoUp = false;
                }
                else
                {
                    piece.CanGoDown = false;
                }
            }

            NextUndo(board, toUndo, lastMoveBeforeUndo);
        }
    }
}
