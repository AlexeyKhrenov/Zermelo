using Game.Implementations;
using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Game.Implementations
{
    public class Game : IGame
    {
        public int Size { get; set; }

        public bool Player1Turn { get; set; }

        public IPlayer ActivePlayer { get; set; }

        private IGameRules Rules { get; set; }

        private bool _isRevertedBoard { get; set; }
        public bool IsRevertedBoard => _isRevertedBoard;


        public IList<IFigure> Figures { get; set; }

        private IHistory _history { get; set; }

        public Game(IGameRules rules, IHistory history, int size, bool revertedSides)
        {
            Figures = new List<IFigure>();
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
        }

        public void SwitchPlayersTurn()
        {
        }
    }
}
