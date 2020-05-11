using Game.Primitives;
using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZermeloUnitTests.Mocks
{
    internal class GameMock : IGame
    {
        public int Size => throw new NotImplementedException();

        public int HistoryLength => throw new NotImplementedException();

        public IBoard Board => _stateTransitions[_stateIndex];

        public IHistoryItem LatestMove => null;

        public bool CanUndo => throw new NotImplementedException();

        public Move[] Moves;

        private IBoard[] _stateTransitions;
        private IPlayer[] _playerTransitions;
        private int _stateIndex;

        public GameMock(IBoard[] stateTransitions, IPlayer[] playerTransitions)
        {
            _stateTransitions = stateTransitions;
            _playerTransitions = playerTransitions;
            Moves = new Move[stateTransitions.Length - 1];

            _playerTransitions[_stateIndex].Figures = _stateTransitions[_stateIndex].ActivePlayer.Figures;
            _stateTransitions[_stateIndex].ActivePlayer = _playerTransitions[_stateIndex];
        }

        public void Move(Move move)
        {
            Moves[_stateIndex] = move;
            _stateIndex++;

            _playerTransitions[_stateIndex].Figures = _stateTransitions[_stateIndex].ActivePlayer.Figures;
            _stateTransitions[_stateIndex].ActivePlayer = _playerTransitions[_stateIndex];
        }

        public void Undo(IPlayer player)
        {
            throw new NotImplementedException();
        }
    }
}
