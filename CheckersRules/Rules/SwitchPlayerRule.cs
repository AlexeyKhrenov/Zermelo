using Checkers.Minifications;
using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Rules
{
    internal class SwitchPlayerRule : AbstractRule
    {
        public override void ApplyRule(BoardMinified board, HistoryItemMinified latestMove)
        {
            board.SwitchPlayers();
            Next(board, latestMove);
        }

        public override void UndoRule(BoardMinified board, HistoryItemMinified toUndo, HistoryItemMinified lastMoveBeforeUndo)
        {
            if (board.ActivePlayer != toUndo.Player)
            {
                board.SwitchPlayers();
            }
            
            NextUndo(board, toUndo, lastMoveBeforeUndo);
        }
    }
}
