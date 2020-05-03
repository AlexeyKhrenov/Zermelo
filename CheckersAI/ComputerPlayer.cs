using Game.Primitives;
using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

[assembly: InternalsVisibleTo("Benchmarking")]
[assembly: InternalsVisibleTo("ZermeloUnitTests")]
namespace CheckersAI
{
    public class ComputerPlayer : IPlayer
    {
        private string _name;
        public string Name => _name + " (computer)";

        public bool IsComputerPlayer => true;

        public IEnumerable<IFigure> Figures { get; set; }

        public int MaxPly { get; }

        public int TimeToThinkMs { get; set; }

        // this is not a good idea to implement INotifyPropertyChanged for a single property or
        // to extend class with a wrapper
        public delegate void MaxPlyChangedHandler(int newValue);
        public event MaxPlyChangedHandler MaxPlyChanged;

        public ComputerPlayer(string name)
        {
            _name = name;
        }

        public void MakeMove(IBoard board, IGame game, CancellationToken cancellationToken)
        {
            cancellationToken.Register(() => StopThinking(board, game));
        }

        private void StopThinking(IBoard board, IGame game)
        {
            var availableMoves = Figures.Select(x => x.AvailableMoves).SelectMany(x => x).ToList();

            var moveIndex = new Random().Next(0, availableMoves.Count);
            var move = availableMoves[moveIndex];

            var figure = Figures.First(x => x.AvailableMoves.Contains(move));

            game.Move(new Move(figure.X, figure.Y, move.X, move.Y));
        }
    }
}
