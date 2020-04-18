using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using Game.Implementations;
using Game.PublicInterfaces;

namespace Game.Implementations
{
    public class Game : IGame
    {
        public int Size { get; set; }

        public IPlayer ActivePlayer { get; set; }

        public IPlayer AwaitingPlayer => ActivePlayer == Player1 ? Player2 : Player1;

        public IPlayer Player1 { get; }

        public IPlayer Player2 { get; }

        private IGameRules Rules { get; set; }

        private bool _isRevertedBoard { get; set; }
        public bool IsRevertedBoard => _isRevertedBoard;
        public IList<IFigure> Figures { get; set; }

        public int HistoryLength => _history?.Length ?? 0;

        private IHistory _history { get; set; }

        public Game(IGameRules rules, IHistory history, int size, bool revertedSides)
        {
            // todo - need to inject this objects?
            Player1 = new Player("Player 1");
            Player2 = new Player("Player 2");

            Player1.Figures = new List<IFigure>();
            Player2.Figures = new List<IFigure>();

            ActivePlayer = Player1;

            _history = history;
            Size = size;
            Rules = rules;
            _isRevertedBoard = revertedSides;
            rules.CreateInitialPosition(this);
        }

        public void Move(int x0, int y0, int x1, int y1)
        {
            var move = new HistoryItem(ActivePlayer, new Point(x0, y0), new Point(x1, y1));
            _history.Push(move);
            Rules.MakeMove(this, move);
        }

        public void Undo()
        {
            var move = _history.Pop();
            var lastMoveBeforeUndo = _history.Latest;

            Rules.Undo(this, move, lastMoveBeforeUndo);
        }

        public void SwitchPlayersTurn()
        {
            if (ActivePlayer == Player1)
            {
                ActivePlayer = Player2;
            }
            else
            {
                ActivePlayer = Player1;
            }
        }
    }
}
