using Checkers.Minifications;
using Game.Primitives;
using System;

namespace Checkers.Rules
{
    internal class NeedToCaptureRule : AbstractRule
    {
        public override BoardMinified ApplyRule(BoardMinified board, HistoryItemMinified latestMove)
        {
            board.ClearMoves();
            var (newBoard, noNeedToCallNext) = CheckRule(board);

            if (!noNeedToCallNext)
            {
                return Next(newBoard, latestMove);
            }

            return newBoard;
        }

        public override BoardMinified UndoRule(BoardMinified board, HistoryItemMinified toUndo, HistoryItemMinified lastMoveBeforeUndo)
        {
            board.ClearMoves();
            var (newBoard, noNeedToCallNext) = CheckRule(board);

            if (!noNeedToCallNext)
            {
                return NextUndo(newBoard, toUndo, lastMoveBeforeUndo);
            }

            return newBoard;
        }

        private (BoardMinified, bool) CheckRule(BoardMinified board)
        {
            var noNeedToCallNext = false;
            var pieces = board.ActiveSet;
            var size = board.GetSize();

            for (var i = 0; i < pieces.Length; i++)
            {
                if (pieces[i].IsEmpty())
                {
                    break;
                }
                if (pieces[i].IsCaptured)
                {
                    continue;
                }
                pieces[i] = Check(pieces[i], board.Pieces, size);

                noNeedToCallNext |= pieces[i].HasAvailableMoves();
            }

            return (board, noNeedToCallNext);
        }

        // returns true if a piece to capture was detected
        public static PieceMinified Check(PieceMinified piece, BoardCell[,] pieces, int size)
        {
            if (piece.Y > 1)
            {
                // left
                if (piece.X > 1)
                {
                    piece = CheckDirection(piece, pieces, -1, -1);
                }
                // right
                if (piece.X < size - 2)
                {
                    piece = CheckDirection(piece, pieces, 1, -1);
                }
            }
            if (piece.Y < size - 2)
            {
                // left
                if (piece.X > 1)
                {
                    piece = CheckDirection(piece, pieces, -1, 1);
                }

                // right
                if (piece.X < size - 2)
                {
                    piece = CheckDirection(piece, pieces, 1, 1);
                }
            }

            return piece;
        }

        private static PieceMinified CheckDirection(PieceMinified piece, BoardCell[,] pieces, sbyte directionRight, sbyte directionDown)
        {
            var target = pieces[piece.X + directionRight, piece.Y + directionDown];
            if (!target.IsEmpty() && target.IsWhite() != piece.IsWhite)
            {
                if (pieces[piece.X + 2 * directionRight, piece.Y + 2 * directionDown].IsEmpty())
                {
                    piece.AddAvailableMove(directionRight, directionDown, true);
                }
            }
            return piece;
        }
    }
}
