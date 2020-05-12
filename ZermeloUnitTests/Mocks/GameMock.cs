using System;
using Game.Primitives;
using Game.PublicInterfaces;

namespace ZermeloUnitTests.Mocks
{
    internal class GameMock : IGame
    {
        private readonly IPlayer[] _playerTransitions;
        private int _stateIndex;

        private readonly IBoard[] _stateTransitions;

        public Move[] Moves;

        public GameMock(IBoard[] stateTransitions, IPlayer[] playerTransitions)
        {
            _stateTransitions = stateTransitions;
            _playerTransitions = playerTransitions;
            Moves = new Move[stateTransitions.Length - 1];

            _playerTransitions[_stateIndex].Figures = _stateTransitions[_stateIndex].ActivePlayer.Figures;
            _stateTransitions[_stateIndex].ActivePlayer = _playerTransitions[_stateIndex];
        }

        public int Size => throw new NotImplementedException();

        public int HistoryLength => throw new NotImplementedException();

        public IBoard Board => _stateTransitions[_stateIndex];

        public IHistoryItem LatestMove => null;

        public bool CanUndo => throw new NotImplementedException();

        public IPlayer Winner => throw new NotImplementedException();

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