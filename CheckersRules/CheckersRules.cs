using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Checkers.Rules;
using Game.PublicInterfaces;

namespace Checkers
{
    // todo - checkers rules appender or host or assembler
    internal class CheckersRules : IGameRules
    {
        private int _size;

        //todo - create several chains of responsibility for different situations
        private AbstractRule HeadOfChainOfRules;
        private AbstractRule HeadOfInitialPositionRules;

        public CheckersRules(int Size)
        {
            _size = Size;
            HeadOfChainOfRules = new NeedToCaptureRule();
            HeadOfChainOfRules.AddNextRuleInChain(new DetectAvailableMovesRule());

            HeadOfInitialPositionRules = new InitialPositionRule();
            HeadOfInitialPositionRules.AddNextRuleInChain(new DetectAvailableMovesRule());
        }

        public void CreateInitialPosition(IGame game)
        {
            HeadOfInitialPositionRules.ApplyRule(game, null);
        }

        public void MakeMove(IGame game, int x0, int y0, int x1, int y1)
        {
            var requiredFigure = game.Figures.FirstOrDefault(f => f.X == x0 && f.Y == y0);
            if (requiredFigure == null)
            {
                throw new InvalidOperationException();
            }

            requiredFigure.X = x1;
            requiredFigure.Y = y1;

            HeadOfChainOfRules.ApplyRule(game, game.Figures.ToPieceMatrix(game.Size));
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
