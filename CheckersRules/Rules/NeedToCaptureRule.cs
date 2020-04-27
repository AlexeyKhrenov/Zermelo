using Checkers.Minifications;
using Game.Primitives;
using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkers.Rules
{
    internal class NeedToCaptureRule : AbstractRule
    {
        public override void ApplyRule(BoardMinified board, HistoryItemMinified latestMove)
        {
            var needToPassControl = true;

            foreach (var figure in board.ActiveSet)
            {
                needToPassControl &= !Check(figure, board.Pieces);
            }

            if (needToPassControl)
            {
                Next(board, latestMove);
            }
        }

        public override void UndoRule(BoardMinified board, HistoryItemMinified toUndo, HistoryItemMinified lastMoveBeforeUndo)
        {
            var needToPassControl = true;

            foreach (var figure in board.ActiveSet)
            {
                needToPassControl &= !Check(figure, board.Pieces);
            }

            if (needToPassControl)
            {
                NextUndo(board, toUndo, lastMoveBeforeUndo);
            }
        }

        // returns true if a piece to capture was detected
        public static bool Check(PieceMinified figure, PieceMinified[,] pieces)
        {
            var result = false;

            var size = pieces.GetLength(0);
            var piece = pieces[figure.X, figure.Y];
            if (piece.Y > 1)
            {
                // left
                if (piece.X > 1)
                {
                    result |= CheckDirection(piece, pieces, -1, -1);
                }
                // right
                if (piece.X < size - 2)
                {
                    result |= CheckDirection(piece, pieces, 1, -1);
                }
            }
            if (piece.Y < size - 2)
            {
                // left
                if (piece.X > 1)
                {
                    result |= CheckDirection(piece, pieces, -1, 1);
                }

                // right
                if (piece.X < size - 2)
                {
                    result |= CheckDirection(piece, pieces, 1, 1);
                }
            }

            return result;
        }

        private static bool CheckDirection(PieceMinified piece, PieceMinified[,] pieces, int directionRight, int directionDown)
        {
            var target = pieces[piece.X + directionRight, piece.Y + directionDown];
            if (target != null && target.IsWhite != piece.IsWhite)
            {
                if (pieces[piece.X + 2 * directionRight, piece.Y + 2 * directionDown] == null)
                {
                    piece.AvailableMoves.Add(new Cell((byte)(piece.X + 2 * directionRight), (byte)(piece.Y + 2 * directionDown)));
                    return true;
                }
            }
            return false;
        }
    }
}
