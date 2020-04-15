using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    public class Game : IGame
    {
        public int Size { get; set; }

        public bool Player1Turn { get; set; }

        private IGameRules Rules { get; set; }

        private bool _isRevertedBoard { get; set; }
        public bool IsRevertedBoard => _isRevertedBoard;


        public IList<IFigure> Figures { get; set; }

        public Game(IGameRules rules, int size, bool revertedSides)
        {
            Figures = new List<IFigure>();
            Size = size;
            Rules = rules;
            _isRevertedBoard = revertedSides;
            rules.CreateInitialPosition(this);
        }

        public void Move(int x0, int y0, int x1, int y1)
        {
            Rules.MakeMove(this, x0, y0, x1, y1);
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }

        public void SwitchPlayersTurn()
        {
            throw new NotImplementedException();
        }
    }
}
