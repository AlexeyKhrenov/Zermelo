﻿using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

[assembly: InternalsVisibleTo("Benchmarking")]
[assembly: InternalsVisibleTo("ZermeloUnitTests")]
namespace CheckersAI
{
    public class ComputerPlayer
    {
        private string _name;
        public string Name => _name + " (computer)"; 

        public IList<IFigure> Figures { get; set; }

        public ComputerPlayer(string name)
        {
            _name = name;
        }

        public void MakeMove(IBoard board)
        {
            Thread.Sleep(200);
            var availableMoves = Figures.Select(x => x.AvailableMoves).SelectMany(x => x).ToList();

            var moveIndex = new Random().Next(0, availableMoves.Count);
            var move = availableMoves[moveIndex];

            var figure = Figures.First(x => x.AvailableMoves.Contains(move));
        }

        public void ClearAvailableMoves()
        {
            foreach (var figure in Figures)
            {
                figure.AvailableMoves.Clear();
            }
        }
    }
}
