using Checkers.Minifications;
using CheckersAI.CheckersGameTree;
using CheckersAI.InternalInterfaces;
using CheckersAI.TreeSearch;
using Game.Primitives;
using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace CheckersAI
{
    public class ComputerPlayer : IPlayer
    {
        public string Name => _name + " (computer)";

        public bool IsComputerPlayer => true;

        public bool IsActive { get; set; }

        public IEnumerable<IFigure> Figures { get; set; }

        public int Ply { get; }

        private string _name;
        private ISearch<GameNode, sbyte, BoardMinified> _search;
        private ITreeManager<GameNode, sbyte> _treeManager;

        internal ComputerPlayer(string name,
            ISearch<GameNode, sbyte, BoardMinified> search,
            ITreeManager<GameNode, sbyte> treeManager)
        {
            _name = name;
            _search = search;
            _treeManager = treeManager;
        }

        public Task MakeMove(IGame game, CancellationToken ct)
        {
            var latestMove = game.LatestMove;

            var practiceBoard = new BoardMinified();
            practiceBoard.Minify(game.Board);

            var root = new GameNode(latestMove, game.Board);
            root = _treeManager.GoDownToNode(root);

            // add registration to abort threads
            var plannedMoves = _search.DoProgressiveDeepening(root, practiceBoard, sbyte.MinValue, sbyte.MaxValue, int.MaxValue, ct);

            StopThinking(game, plannedMoves);
            return Task.CompletedTask;
        }

        private void StopThinking(IGame game, Queue<GameNode> plannedMoves)
        {
            while (game.Board.ActivePlayer == this)
            {
                if (plannedMoves == null || plannedMoves.Count == 0)
                {
                    MakeRandomNextMove(game);
                }
                else
                {
                    var nextMove = plannedMoves.Dequeue();
                    game.Move(new Move(nextMove.Move.From, nextMove.Move.To));
                }
            }
        }

        private void MakeRandomNextMove(IGame game)
        {
            var availableMoves = Figures.Select(x => x.AvailableMoves).SelectMany(x => x).ToList();

            var moveIndex = new Random().Next(0, availableMoves.Count);
            var move = availableMoves[moveIndex];

            var figure = Figures.First(x => x.AvailableMoves.Contains(move));

            game.Move(new Move(figure.X, figure.Y, move.X, move.Y));
        }
    }
}
