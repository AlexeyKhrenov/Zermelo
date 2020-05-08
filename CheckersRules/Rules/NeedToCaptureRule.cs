using Checkers.Minifications;
using Game.Primitives;

namespace Checkers.Rules
{
    internal class NeedToCaptureRule : AbstractRule
    {
        public override BoardMinified ApplyRule(BoardMinified board, HistoryItemMinified latestMove)
        {
            var needToPassControl = true;

            foreach (var piece in board.ActiveSet)
            {
                if (piece.IsEmpty())
                {
                    break;
                }

                if (piece.IsCaptured)
                {
                    continue;
                }
                needToPassControl &= !Check(piece, board.Pieces);
            }

            if (needToPassControl)
            {
                return Next(board, latestMove);
            }

            return board;
        }

        public override BoardMinified UndoRule(BoardMinified board, HistoryItemMinified toUndo, HistoryItemMinified lastMoveBeforeUndo)
        {
            var needToPassControl = true;

            foreach (var piece in board.ActiveSet)
            {
                if (piece.IsEmpty())
                {
                    break;
                }
                if (piece.IsCaptured)
                {
                    continue;
                }
                needToPassControl &= !Check(piece, board.Pieces);
                board.Replace(piece, board.ActivePlayer);
            }

            if (needToPassControl)
            {
                return NextUndo(board, toUndo, lastMoveBeforeUndo);
            }

            return board;
        }

        // returns true if a piece to capture was detected
        public static bool Check(PieceMinified piece, BoardCell[,] pieces)
        {
            var result = false;

            var size = pieces.GetLength(0);
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

        private static bool CheckDirection(PieceMinified piece, BoardCell[,] pieces, int directionRight, int directionDown)
        {
            var target = pieces[piece.X + directionRight, piece.Y + directionDown];
            if (!target.IsEmpty() && target.IsWhite() != piece.IsWhite)
            {
                if (pieces[piece.X + 2 * directionRight, piece.Y + 2 * directionDown].IsEmpty())
                {
                    var i = directionDown > 0 ? 2 : 0;
                    var j = directionRight > 0 ? 1 : 0;
                    piece.AvailableMoves[i + j] = new Cell((byte)(piece.X + 2 * directionRight), (byte)(piece.Y + 2 * directionDown));
                    return true;
                }
            }
            return false;
        }
    }
}
