using Checkers.Minifications;
using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Rules
{
    internal class SwitchPlayerRule : AbstractRule
    {
        public override BoardMinified ApplyRule(BoardMinified board, HistoryItemMinified latestMove)
        {
            board.SwitchPlayers();
            return Next(board, latestMove);
        }

        public override BoardMinified UndoRule(BoardMinified board, HistoryItemMinified toUndo, HistoryItemMinified lastMoveBeforeUndo)
        {
            if (board.ActivePlayer != toUndo.Player)
            {
                board.SwitchPlayers();
            }
            
            return NextUndo(board, toUndo, lastMoveBeforeUndo);
        }
    }
}
