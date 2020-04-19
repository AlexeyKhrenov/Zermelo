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

        public IBoard Board { get; private set; }

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

            Board = new Board(player1, player2, size, revertedSides);
            Rules.PlaceFigures(Board);
        }

        public void Move(int x0, int y0, int x1, int y1)
        {
            var move = new HistoryItem(Board.ActivePlayer, new Point(x0, y0), new Point(x1, y1));
            _history.Push(move);

            // make return type IHistoryItem
            Rules.MakeMove(Board, move);
        }

        public void Undo()
        {
            var move = _history.Pop();
            var lastMoveBeforeUndo = _history.Latest;

            //Rules.Undo(this, move, lastMoveBeforeUndo);
        }
    }
}
