using Checkers;
using Checkers.Minifications;
using CheckersAI.CheckersGameTree;
using CheckersAI.InternalInterfaces;
using CheckersAI.TreeSearch;
using Game.PublicInterfaces;

namespace CheckersAI
{
    public static class ServiceLocator
    {
        public static IPlayer CreateComputerPlayer(string name)
        {
            var treeManager = new TreeManager<GameNode, sbyte>();
            var search = CreateSerialGameTreeSearch();
            var searchWrapper = CreateProgressiveDeepeningWrapper(search);

            return new ComputerPlayer(name, searchWrapper, treeManager);
        }

        internal static ISearch<GameNode, sbyte, BoardMinified> CreateSerialGameTreeSearch()
        {
            var rules = new CheckersRules();

            var comparator = new Comparator();
            var brancher = new Brancher(rules);
            var evaluator = new Evaluator();
            var stateTransitions = new StateTransitions(rules);

            return new SerialAlfaBetaSearch<GameNode, sbyte, BoardMinified>(
                evaluator,
                brancher,
                comparator,
                stateTransitions,
                sbyte.MaxValue,
                sbyte.MinValue
            );
        }

        internal static ISearch<GameNode, sbyte, BoardMinified> CreateDynamicTreeSplittingGameTreeSearch()
        {
            var rules = new CheckersRules();

            var comparator = new Comparator();
            var brancher = new Brancher(rules);
            var evaluator = new Evaluator();
            var stateTransitions = new StateTransitions(rules);

            return new DynamicTreeSplitting<GameNode, sbyte, BoardMinified>(
                evaluator,
                brancher,
                comparator,
                stateTransitions
            );
        }

        internal static IProgressiveDeepeningWrapper<GameNode, sbyte, BoardMinified> 
            CreateProgressiveDeepeningWrapper(ISearch<GameNode, sbyte, BoardMinified> search)
        {
            return new ProgressiveDeepeningWrapper<GameNode, sbyte, BoardMinified>(search);
        }
    }
}
