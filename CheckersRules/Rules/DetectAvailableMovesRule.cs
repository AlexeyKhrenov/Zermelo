using Checkers.Minifications;

namespace Checkers.Rules
{
    internal unsafe class DetectAvailableMovesRule : AbstractRule
    {
        public override BoardMinified ApplyRule(BoardMinified board, HistoryItemMinified latestMove)
        {
            board = CheckRule(board, latestMove);
            return Next(board, latestMove);
        }

        public override BoardMinified UndoRule(BoardMinified board, HistoryItemMinified toUndo,
            HistoryItemMinified lastMoveBeforeUndo)
        {
            board = CheckRule(board, toUndo);
            return NextUndo(board, toUndo, lastMoveBeforeUndo);
        }

        private BoardMinified CheckRule(BoardMinified board, HistoryItemMinified latestMove)
        {
            var size = board.GetSize();

            var activeSetPtr = board.ActivePlayer ? board.Player1Pieces : board.Player2Pieces;

            for (var i = 0; i < BoardMinified.BufferSize; i++)
            {
                var currentPtr = activeSetPtr + i;
                var piece = (PieceMinified) (*currentPtr);
                if (piece.IsEmpty()) break;
                if (piece.IsCaptured) continue;

                if (piece.CanGoUp && piece.Y > 0)
                {
                    //left
                    if (piece.X > 0) piece = Check(piece, board, -1, -1);
                    //right
                    if (piece.X < size - 1) piece = Check(piece, board, -1, 1);
                }

                if (piece.CanGoDown && piece.Y < size - 1)
                {
                    //left
                    if (piece.X > 0) piece = Check(piece, board, 1, -1);
                    //right
                    if (piece.X < size - 1) piece = Check(piece, board, 1, 1);
                }

                *currentPtr = piece;
            }

            return board;
        }

        public PieceMinified Check(PieceMinified piece, BoardMinified board, sbyte directionDown, sbyte directionRight)
        {
            var boardCell = board.GetBoardCell((byte) (piece.X + directionRight), (byte) (piece.Y + directionDown));
            if (boardCell.IsEmpty()) piece.AddAvailableMove(directionRight, directionDown, false);
            return piece;
        }
    }
}