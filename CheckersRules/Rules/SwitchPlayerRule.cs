using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Rules
{
    internal class SwitchPlayerRule : AbstractRule
    {
        public override string Name => nameof(SwitchPlayerRule);

        public override void ApplyRule(IGame game, Piece[,] pieces, IHistoryItem latestMove)
        {
            game.SwitchPlayersTurn();
            Next(game, pieces, latestMove);
        }

        public override void UndoRule(IGame game, Piece[,] pieces, IHistoryItem toUndo, IHistoryItem lastMoveBeforeUndo)
        {
            if (game.ActivePlayer != toUndo.Player)
            {
                game.SwitchPlayersTurn();
            }
            
            NextUndo(game, pieces, toUndo, lastMoveBeforeUndo);
        }
    }
}
