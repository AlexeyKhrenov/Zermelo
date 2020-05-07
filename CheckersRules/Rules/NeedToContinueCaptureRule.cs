using Checkers.Minifications;
using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Rules
{
    internal class NeedToContinueCaptureRule : AbstractRule
    {
        public override BoardMinified ApplyRule(BoardMinified board, HistoryItemMinified latestMove)
        {
            if (latestMove.IsKill)
            {
                var piece = board.GetPiece(latestMove.To.X, latestMove.To.Y, latestMove.Player);

                if (NeedToCaptureRule.Check(piece, board.Pieces))
                {
                    return board;
                }
            }

            return Next(board, latestMove);
        }

        public override BoardMinified UndoRule(BoardMinified board, HistoryItemMinified toUndo, HistoryItemMinified lastMoveBeforeUndo)
        {
            if (
                lastMoveBeforeUndo != null &&
                toUndo.IsKill &&
                lastMoveBeforeUndo.IsKill &&
                toUndo.Player == lastMoveBeforeUndo.Player)
            {
                var piece = board.GetPiece(toUndo.From.X, toUndo.From.Y, toUndo.Player);

                NeedToCaptureRule.Check(piece, board.Pieces);

                if (board.ActivePlayer != lastMoveBeforeUndo.Player)
                {
                    board.SwitchPlayers();
                }

                return board;
            }
            else
            {
                return NextUndo(board, toUndo, lastMoveBeforeUndo);
            }
        }
    }
}
