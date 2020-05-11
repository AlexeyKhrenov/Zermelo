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
                piece.ClearMoves();

                var newPiece = NeedToCaptureRule.Check(piece, board.Pieces, board.GetSize());
                if (newPiece.HasAvailableMoves())
                {
                    board.ClearMoves();
                    board.UpdatePieceAvailableMoves(newPiece, board.ActivePlayer);
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
                piece.ClearMoves();

                var newPiece = NeedToCaptureRule.Check(piece, board.Pieces, board.GetSize());
                if (newPiece.HasAvailableMoves())
                {
                    board.ClearMoves();
                    board.UpdatePieceAvailableMoves(newPiece, toUndo.Player);

                    if (board.ActivePlayer != lastMoveBeforeUndo.Player)
                    {
                        board.SwitchPlayers();
                    }

                    return board;
                }
            }

            return NextUndo(board, toUndo, lastMoveBeforeUndo);
        }
    }
}
