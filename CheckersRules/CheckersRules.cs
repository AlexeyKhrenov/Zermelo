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
        private AbstractRule ChainOfRules;
        private AbstractRule InitialPositionRules;

        public CheckersRules(int Size)
        {
            _size = Size;
            ChainOfRules = new AppendMoveRule();
            ChainOfRules.AddNext(new RemoveCapturedPieceRule());
            ChainOfRules.AddNext(new ChangePieceTypeRule());
            ChainOfRules.AddNext(new NeedToContinueCaptureRule());
            ChainOfRules.AddNext(new SwitchPlayerRule());
            ChainOfRules.AddNext(new NeedToCaptureRule());
            ChainOfRules.AddNext(new DetectAvailableMovesRule());

            InitialPositionRules = new InitialPositionRule();
            InitialPositionRules.AddNext(new DetectAvailableMovesRule());
        }

        public void CreateInitialPosition(IGame game)
        {
            var matrix = Helper.ToPieceMatrix(game.Size, game.Player1.Figures, game.Player2.Figures);
            InitialPositionRules.ApplyRule(game, matrix, null);
        }

        public void MakeMove(IGame game, IHistoryItem move)
        {
            var matrix = Helper.ToPieceMatrix(game.Size, game.Player1.Figures, game.Player2.Figures);
            ChainOfRules.ApplyRule(game, matrix, move);
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
