using Checkers.Minifications;

namespace Checkers.Rules
{
    internal unsafe class NeedToCaptureRule : AbstractRule
    {
        public override BoardMinified ApplyRule(BoardMinified board, HistoryItemMinified latestMove)
        {
            board.ClearMoves();
            var (newBoard, noNeedToCallNext) = CheckRule(board);

            if (!noNeedToCallNext) return Next(newBoard, latestMove);

            return newBoard;
        }

        public override BoardMinified UndoRule(BoardMinified board, HistoryItemMinified toUndo,
            HistoryItemMinified lastMoveBeforeUndo)
        {
            board.ClearMoves();
            var (newBoard, noNeedToCallNext) = CheckRule(board);

            if (!noNeedToCallNext) return NextUndo(newBoard, toUndo, lastMoveBeforeUndo);

            return newBoard;
        }

        private (BoardMinified, bool) CheckRule(BoardMinified board)
        {
            var noNeedToCallNext = false;
            var size = board.GetSize();

            var activeSetPtr = board.ActivePlayer ? board.Player1Pieces : board.Player2Pieces;

            for (var i = 0; i < BoardMinified.BufferSize; i++)
            {
                var currentPtr = activeSetPtr + i;
                var piece = (PieceMinified) (*currentPtr);
                if (piece.IsEmpty()) break;
                if (piece.IsCaptured) continue;

                piece = Check(piece, board, size);
                noNeedToCallNext |= piece.HasAvailableMoves();
                *currentPtr = piece;
            }

            return (board, noNeedToCallNext);
        }

        // returns true if a piece to capture was detected
        public static PieceMinified Check(PieceMinified piece, BoardMinified board, int size)
        {
            if (piece.Y > 1)
            {
                // left
                if (piece.X > 1) piece = CheckDirection(piece, board, -1, -1);
                // right
                if (piece.X < size - 2) piece = CheckDirection(piece, board, 1, -1);
            }

            if (piece.Y < size - 2)
            {
                // left
                if (piece.X > 1) piece = CheckDirection(piece, board, -1, 1);

                // right
                if (piece.X < size - 2) piece = CheckDirection(piece, board, 1, 1);
            }

            return piece;
        }

        private static PieceMinified CheckDirection(PieceMinified piece, BoardMinified board, sbyte directionRight,
            sbyte directionDown)
        {
            var target = board.GetBoardCell((byte) (piece.X + directionRight), (byte) (piece.Y + directionDown));
            if (!target.IsEmpty() && target.IsWhite() != piece.IsWhite)
            {
                var second = board.GetBoardCell((byte) (piece.X + 2 * directionRight),
                    (byte) (piece.Y + 2 * directionDown));
                if (second.IsEmpty()) piece.AddAvailableMove(directionRight, directionDown, true);
            }

            return piece;
        }
    }
}