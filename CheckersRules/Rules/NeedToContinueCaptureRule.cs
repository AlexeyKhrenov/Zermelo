using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Rules
{
    internal class NeedToContinueCaptureRule : AbstractRule
    {
        public override string Name => nameof(NeedToContinueCaptureRule);

        public override void ApplyRule(IGame game, Piece[,] pieces, IHistoryItem latestMove)
        {
            if (latestMove.IsKill)
            {
                var piece = pieces[latestMove.To.X, latestMove.To.Y];

                if (NeedToCaptureRule.Check(piece, pieces))
                {
                    return;
                }
            }

            Next(game, pieces, latestMove);
        }

        public override void UndoRule(IGame game, Piece[,] pieces, IHistoryItem toUndo, IHistoryItem lastMoveBeforeUndo)
        {
            if (
                lastMoveBeforeUndo != null &&
                toUndo.IsKill &&
                lastMoveBeforeUndo.IsKill &&
                toUndo.Player == lastMoveBeforeUndo.Player)
            {
                var piece = pieces[toUndo.From.X, toUndo.From.Y];

                NeedToCaptureRule.Check(piece, pieces);

                if (game.ActivePlayer != lastMoveBeforeUndo.Player)
                {
                    game.SwitchPlayersTurn();
                }

                return;
            }
            else
            {
                NextUndo(game, pieces, toUndo, lastMoveBeforeUndo);
            }
        }
    }
}
