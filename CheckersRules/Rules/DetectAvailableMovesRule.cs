﻿using Checkers.Minifications;
using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Checkers.Rules
{
    internal class DetectAvailableMovesRule : AbstractRule
    {
        public override void ApplyRule(BoardMinified board, HistoryItemMinified latestMove)
        {
            CheckRule(board, latestMove);
            Next(board, latestMove);
        }

        public override void UndoRule(BoardMinified board, HistoryItemMinified toUndo, HistoryItemMinified lastMoveBeforeUndo)
        {
            CheckRule(board, toUndo);
            NextUndo(board, toUndo, lastMoveBeforeUndo);
        }

        private void CheckRule(BoardMinified board, HistoryItemMinified latestMove)
        {
            foreach (var figure in board.GetActivePlayer().Pieces)
            {
                // todo - remove this cast
                var piece = board.Pieces[figure.X, figure.Y];
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
        }

        public void Check(PieceMinified piece, PieceMinified[,] pieces, int directionDown, int directionRight)
        {
            if (pieces[piece.X + directionRight, piece.Y + directionDown] == null)
            {
                piece.AvailableMoves.Add(new Point(piece.X + directionRight, piece.Y + directionDown));
            }
        }
    }
}
