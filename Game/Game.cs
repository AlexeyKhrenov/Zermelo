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

        public IList<IFigure> Figures { get; private set; }

        public Game(IGameRules rules, int size, bool revertedSides)
        {
            Size = size;
            Rules = rules;
            Figures = rules.CreateInitialPosition(Size, revertedSides);
        }
    }
}
