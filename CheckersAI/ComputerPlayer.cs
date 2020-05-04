using Game.Primitives;
using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Benchmarking")]
[assembly: InternalsVisibleTo("ZermeloUnitTests")]
namespace CheckersAI
{
    public class ComputerPlayer : IPlayer
    {
        private string _name;
        public string Name => _name + " (computer)";

        public bool IsComputerPlayer => true;

        public bool IsActive { get; set; }

        public IEnumerable<IFigure> Figures { get; set; }

        public int Ply { get; }

        public ComputerPlayer(string name)
        {
            _name = name;
        }

        public Task MakeMove(IGame game, CancellationToken cancellationToken)
        {
            // add registration to abort threads
            StartProgressiveDeepening(cancellationToken);
            StopThinking(game);
            return Task.CompletedTask;
        }

        public void StartProgressiveDeepening(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var Rand = new Random();
                var rn = Rand.NextDouble();
            }
        }

        private void StopThinking(IGame game)
        {
            var availableMoves = Figures.Select(x => x.AvailableMoves).SelectMany(x => x).ToList();

            var moveIndex = new Random().Next(0, availableMoves.Count);
            var move = availableMoves[moveIndex];

            var figure = Figures.First(x => x.AvailableMoves.Contains(move));

            game.Move(new Move(figure.X, figure.Y, move.X, move.Y));
        }
    }
}
