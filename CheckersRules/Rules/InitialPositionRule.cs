﻿using Checkers.Minifications;
using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Rules
{
    internal class InitialPositionRule : AbstractRule
    {
        public override void ApplyRule(BoardMinified board, HistoryItemMinified latestMove)
        {
            var size = board.GetSize();
            var changedSides = board.InvertedCoordinates;
            var player1Figures = new List<PieceMinified>();
            var player2Figures = new List<PieceMinified>();

            if (size < 4)
            {
                throw new NotImplementedException("Game size smaller than 4");
            }

            // positioning pieces
            var isWhite = !changedSides;
            for (var y = size - 1; y > size / 2; y--)
            {
                var startX = 1 - y % 2;
                for (var x = startX; x < size; x += 2)
                {
                    var piece = new PieceMinified(x, y, isWhite, true, false);
                    board.Pieces[x, y] = piece;
                    player1Figures.Add(piece);
                }
            }

            for (var y = 0; y < size / 2 - 1; y++)
            {
                var startX = 1 - y % 2;
                for (var x = startX; x < size; x += 2)
                {
                    var piece = new PieceMinified(x, y, !isWhite, false, true);
                    board.Pieces[x, y] = piece;
                    player2Figures.Add(piece);
                }
            }

            board.Player1Pieces = player1Figures;
            board.Player2Pieces = player2Figures;

            Next(board, null);
        }

        public override void UndoRule(BoardMinified board, HistoryItemMinified toUndo, HistoryItemMinified lastMoveBeforeUndo)
        {
            throw new InvalidOperationException();
        }
    }
}
