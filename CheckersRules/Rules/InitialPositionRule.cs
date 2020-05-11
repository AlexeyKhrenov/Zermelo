﻿using Checkers.Minifications;
using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Rules
{
    internal class InitialPositionRule : AbstractRule
    {
        public override BoardMinified ApplyRule(BoardMinified board, HistoryItemMinified latestMove)
        {
            var size = board.GetSize();
            var player1Figures = new PieceMinified[20];
            var player2Figures = new PieceMinified[20];

            if (size < 4)
            {
                throw new NotImplementedException("Game size smaller than 4");
            }

            // todo - refactor this
            var isWhite = true;
            byte i = 0;
            for (byte y = (byte)(size - 1); y > size / 2; y--)
            {
                var startX = (byte)(1 - y % 2);
                for (var x = startX; x < size; x += 2)
                {
                    var piece = new PieceMinified(x, y, isWhite, true, false);
                    player1Figures[i] = piece;
                    board.Pieces[x, y] = new BoardCell(i, isWhite);
                    i++;
                }
            }

            i = 0;
            for (byte y = 0; y < size / 2 - 1; y++)
            {
                var startX = (byte)(1 - y % 2);
                for (var x = startX; x < size; x += 2)
                {
                    var piece = new PieceMinified(x, y, !isWhite, false, true);
                    player2Figures[i] = piece;
                    board.Pieces[x, y] = new BoardCell(i, isWhite);
                    i++;
                }
            }

            board.Player1Pieces = player1Figures;
            board.Player2Pieces = player2Figures;

            return Next(board, null);
        }

        public override BoardMinified UndoRule(BoardMinified board, HistoryItemMinified toUndo, HistoryItemMinified lastMoveBeforeUndo)
        {
            throw new InvalidOperationException();
        }
    }
}
