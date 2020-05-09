using Checkers.Minifications;
using Game.Primitives;
using System;

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
            var pieces = board.ActiveSet;
            var size = board.GetSize();

            for (var i = 0; i < board.ActiveSet.Length; i++)
            {
                if (pieces[i].IsEmpty())
                {
                    break;
                }
                if (pieces[i].IsCaptured)
                {
                    continue;
                }

                if (pieces[i].CanGoUp && pieces[i].Y > 0)
                {
                    //left
                    if (pieces[i].X > 0)
                    {
                        pieces[i] = Check(pieces[i], board.Pieces, -1, -1);
                    }
                    //right
                    if (pieces[i].X < size - 1)
                    {
                        pieces[i] = Check(pieces[i], board.Pieces, -1, 1);
                    }
                }

                if (pieces[i].CanGoDown && pieces[i].Y < size - 1)
                {
                    //left
                    if (pieces[i].X > 0)
                    {
                        pieces[i] = Check(pieces[i], board.Pieces, 1, -1);
                    }
                    //right
                    if (pieces[i].X < size - 1)
                    {
                        pieces[i] = Check(pieces[i], board.Pieces, 1, 1);
                    }
                }
            }

            return board;
        }

        public PieceMinified Check(PieceMinified piece, BoardCell[,] pieces, sbyte directionDown, sbyte directionRight)
        {
            if (pieces[piece.X + directionRight, piece.Y + directionDown].IsEmpty())
            {
                piece.AddAvailableMove(directionRight, directionDown, false);
            }
            return piece;
        }
    }
}
