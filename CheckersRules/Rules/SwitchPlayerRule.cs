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
    }
}
