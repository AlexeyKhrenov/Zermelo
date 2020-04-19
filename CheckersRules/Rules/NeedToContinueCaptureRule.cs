using Checkers.Minifications;
using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Rules
{
    internal class NeedToContinueCaptureRule : AbstractRule
    {
        public override void ApplyRule(BoardMinified board, HistoryItemMinified latestMove)
        {
            if (latestMove.IsKill)
            {
                var piece = board.Pieces[latestMove.To.X, latestMove.To.Y];

                if (NeedToCaptureRule.Check(piece, board.Pieces))
                {
                    return;
                }
            }

            Next(board, latestMove);
        }

        public override void UndoRule(BoardMinified board, HistoryItemMinified toUndo, HistoryItemMinified lastMoveBeforeUndo)
        {
            if (
                lastMoveBeforeUndo != null &&
                toUndo.IsKill &&
                lastMoveBeforeUndo.IsKill &&
                toUndo.Player == lastMoveBeforeUndo.Player)
            {
                var piece = board.Pieces[toUndo.From.X, toUndo.From.Y];

                NeedToCaptureRule.Check(piece, board.Pieces);

                if (board.ActivePlayer == lastMoveBeforeUndo.Player)
                {
                    board.SwitchPlayers();
                }

                return;
            }
            else
            {
                NextUndo(board, toUndo, lastMoveBeforeUndo);
            }
        }
    }
}
