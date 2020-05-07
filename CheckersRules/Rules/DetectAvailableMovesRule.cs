using Checkers.Minifications;
using Game.Primitives;

namespace Checkers.Rules
{
    internal class DetectAvailableMovesRule : AbstractRule
    {
        public override BoardMinified ApplyRule(BoardMinified board, HistoryItemMinified latestMove)
        {
            CheckRule(board, latestMove);
            return Next(board, latestMove);
        }

        public override BoardMinified UndoRule(BoardMinified board, HistoryItemMinified toUndo, HistoryItemMinified lastMoveBeforeUndo)
        {
            CheckRule(board, toUndo);
            return NextUndo(board, toUndo, lastMoveBeforeUndo);
        }

        private BoardMinified CheckRule(BoardMinified board, HistoryItemMinified latestMove)
        {
            foreach (var piece in board.ActiveSet)
            {
                if (piece.IsCaptured)
                {
                    continue;
                }
                var size = board.GetSize();

                if (piece.CanGoUp && piece.Y > 0)
                {
                    //left
                    if (piece.X > 0)
                    {
                        Check(piece, board.Pieces, -1, -1);
                    }
                    //right
                    if (piece.X < size - 1)
                    {
                        Check(piece, board.Pieces, -1, 1);
                    }
                }

                if (piece.CanGoDown && piece.Y < size - 1)
                {
                    //left
                    if (piece.X > 0)
                    {
                        Check(piece, board.Pieces, 1, -1);
                    }
                    //right
                    if (piece.X < size - 1)
                    {
                        Check(piece, board.Pieces, 1, 1);
                    }
                }
            }

            return board;
        }

        public void Check(PieceMinified piece, BoardCell[,] pieces, int directionDown, int directionRight)
        {
            if (pieces[piece.X + directionRight, piece.Y + directionDown].IsEmpty())
            {
                var i = directionDown > 0 ? 2 : 0;
                var j = directionRight > 0 ? 1 : 0;
                piece.AvailableMoves[i + j] = new Cell((byte)(piece.X + directionRight), (byte)(piece.Y + directionDown));
            }
        }
    }
}
