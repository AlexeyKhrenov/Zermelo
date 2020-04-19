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

        private IGameRules Rules { get; set; }

        private IBoard _board;
        public IBoard Board => _board;

        public int HistoryLength => _history?.Length ?? 0;

        private IHistory _history { get; set; }

        public Game(IGameRules rules, IPlayer player1, IPlayer player2, IHistory history, int size, bool revertedSides)
        {
            // remove this intialization
            player1.Figures = new List<IFigure>();
            player2.Figures = new List<IFigure>();

            _history = history;
            Size = size;
            Rules = rules;
            _board = rules.CreateBoard(player1, player2, revertedSides);
        }

        public IEnumerable<IFigure> GetFigures()
        {
            return _board.GetFigures();
        }

        public void Move(int x0, int y0, int x1, int y1)
        {
            var move = new HistoryItem(_board.ActivePlayer, new Point(x0, y0), new Point(x1, y1));
            _history.Push(move);
            //Rules.MakeMove(this, move);
        }

        public void Undo()
        {
            var move = _history.Pop();
            var lastMoveBeforeUndo = _history.Latest;

            //Rules.Undo(this, move, lastMoveBeforeUndo);
        }
    }
}
