using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Checkers.Minifications;
using CheckersAI.CheckersGameTree;
using CheckersAI.InternalInterfaces;
using Game.Primitives;
using Game.PublicInterfaces;

namespace CheckersAI
{
    public class ComputerPlayer : IPlayer
    {
        private readonly string _name;
        private readonly IProgressiveDeepeningWrapper<GameNode, sbyte, BoardMinified> _search;
        private readonly ITreeManager<GameNode, sbyte> _treeManager;

        internal ComputerPlayer(string name,
            IProgressiveDeepeningWrapper<GameNode, sbyte, BoardMinified> search,
            ITreeManager<GameNode, sbyte> treeManager)
        {
            _name = name;
            _search = search;
            _treeManager = treeManager;
        }

        public int Ply { get; private set; }
        public string Name => _name + " (computer)";

        public bool IsComputerPlayer => true;

        public bool IsActive { get; set; }

        public List<IFigure> Figures { get; set; }

        public Task MakeMove(IGame game, CancellationToken ct)
        {
            var latestMove = game.LatestMove;

            var practiceBoard = game.Board.ToMinified();

            var root = new GameNode(latestMove, game.Board, practiceBoard.ActivePlayer);
            root = _treeManager.GoDownToNode(root);

            // add registration to abort threads
            var (plannedMoves, maxPly) =
                _search.Run(practiceBoard, root, sbyte.MinValue, sbyte.MaxValue, int.MaxValue, ct);

            Ply = maxPly;
            StopThinking(game, plannedMoves);
            return Task.CompletedTask;
        }

        private void StopThinking(IGame game, Queue<GameNode> plannedMoves)
        {
            while (game.Board.ActivePlayer == this)
                if (plannedMoves == null || plannedMoves.Count == 0)
                {
                    MakeRandomNextMove(game);
                }
                else
                {
                    var nextMove = plannedMoves.Dequeue();
                    game.Move(new Move(nextMove.Move.From, nextMove.Move.To));
                    _treeManager.GoDownToNode(nextMove);
                }

            GC.Collect();
        }

        private void MakeRandomNextMove(IGame game)
        {
            var availableMoves = Figures.Select(x => x.AvailableMoves).SelectMany(x => x).ToList();

            var moveIndex = new Random().Next(0, availableMoves.Count);

            if (availableMoves.Count == 0) return;

            var move = availableMoves[moveIndex];

            var figure = Figures.First(x => x.AvailableMoves.Contains(move));

            game.Move(new Move(figure.X, figure.Y, move.X, move.Y));
        }
    }
}