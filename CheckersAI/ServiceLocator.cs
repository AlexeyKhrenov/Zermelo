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
            var search = CreateDynamicTreeSplittingGameTreeSearch();
            var searchWrapper = CreateProgressiveDeepeningWrapper(search);

            return new ComputerPlayer(name, searchWrapper, treeManager);
        }

        internal static ISearch<GameNode, sbyte, BoardMinified> CreateSerialGameTreeSearch()
        {
            var rules = new CheckersRules();

            var comparator = new Comparator();
            var evaluator = new Evaluator();
            var stateTransitions = new StateTransitions(rules);
            var brancher = new Brancher(rules, evaluator, stateTransitions);

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
            var evaluator = new Evaluator();
            var stateTransitions = new StateTransitions(rules);
            var brancher = new Brancher(rules, evaluator, stateTransitions);

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