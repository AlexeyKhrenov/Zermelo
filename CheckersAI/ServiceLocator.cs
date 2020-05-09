using Checkers;
using Checkers.Minifications;
using CheckersAI.CheckersGameTree;
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

            return new ComputerPlayer(name, search, treeManager);
        }

        internal static SerialAlfaBetaSearch<GameNode, sbyte, BoardMinified> CreateSerialGameTreeSearch()
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

        internal static DynamicTreeSplitting<GameNode, sbyte, BoardMinified> CreateDynamicTreeSplittingGameTreeSearch()
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
    }
}
