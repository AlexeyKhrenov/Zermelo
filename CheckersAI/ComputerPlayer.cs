using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CheckersAI
{
    public class ComputerPlayer : IPlayer
    {
        private string _name;
        public string Name => _name + " (computer)"; 

        public IList<IFigure> Figures { get; set; }

        public ComputerPlayer(string name)
        {
            _name = name;
        }

        public void MakeMove(IGame game)
        {
            Thread.Sleep(200);
            var availableMoves = Figures.Select(x => x.AvailableMoves).SelectMany(x => x).ToList();

            var moveIndex = new Random().Next(0, availableMoves.Count);
            var move = availableMoves[moveIndex];

            var figure = Figures.First(x => x.AvailableMoves.Contains(move));

            game.Move(figure.X, figure.Y, move.X, move.Y);
        }
    }
}
