using System;
using System.Collections.Generic;
using System.Text;

using Game.Implementations;
using Game.Primitives;
using Game.PublicInterfaces;

namespace Game.Implementations
{
    public class Game : IGame
    {
        public int Size { get; set; }

        private IGameRules Rules { get; set; }

        public IBoard Board { get; private set; }

        public int HistoryLength => _history?.Length ?? 0;

        private IHistory _history { get; set; }

        public IHistoryItem LatestMove => _history.Latest;

        public Game(IGameRules rules, IPlayer player1, IPlayer player2, IHistory history, int size)
        {
            // remove this intialization
            player1.Figures = new List<IFigure>();
            player2.Figures = new List<IFigure>();

            _history = history;
            Size = size;
            Rules = rules;

            Board = new Board(player1, player2, size);
            Rules.PlaceFigures(Board);
        }

        public void Move(Move move)
        {
            var historyItem = new HistoryItem(Board.ActivePlayer, move);
            _history.Push(historyItem);

            // make return type IHistoryItem
            Rules.MakeMove(Board, historyItem);
        }

        public void Undo(IPlayer player)
        {
            while (CanUndo)
            {
                var move = _history.Pop();
                var lastMoveBeforeUndo = _history.Latest;

                Rules.Undo(Board, move, lastMoveBeforeUndo);

                if (player == move.Player)
                {
                    break;
                }
            }
        }

        public bool CanUndo
        {
            get
            {
                if (_history.Length == 0)
                {
                    return false;
                }
                if (Board.Player1.IsComputerPlayer && Board.Player2.IsComputerPlayer)
                {
                    return false;
                }
                if (_history.Length == 1 && _history.Latest.Player.IsComputerPlayer)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
