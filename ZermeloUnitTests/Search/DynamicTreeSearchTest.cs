using System.Threading;
using CheckersAI.ByteTree;
using CheckersAI.TreeSearch;
using Xunit;

namespace ZermeloUnitTests.Search
{
    public class DynamicTreeSearchTest
    {
        private readonly CancellationTokenSource _cts;
        private readonly DynamicTreeSplitting<AlfaBetaByteNode, sbyte, sbyte> _search;

        public DynamicTreeSearchTest()
        {
            var comparator = new Comparator();
            var brancher = new Brancher();
            var evaluator = new Evaluator();
            var stateTransitions = new StateTransitions();
            _search = new DynamicTreeSplitting<AlfaBetaByteNode, sbyte, sbyte>(
                evaluator,
                brancher,
                comparator,
                stateTransitions
            );

            _cts = new CancellationTokenSource();
        }

        [Theory]
        [InlineData("1", 3, 0, 3)]
        [InlineData("7 -2 -3", -4, 1, -2)]
        [InlineData("1 -2 0 -6 8 1 0", 0, 2, 0)]
        [InlineData("2 -1 -3 -4 0 7 2 1 1 13 -5 0 -6 -7 3", 7, 3, 5)]
        public void SearchWithoutBranchingTheory(string treeStr, sbyte expected, int depth, sbyte startState)
        {
            var tree = TreeGenerator.ParseAlfaBetaTree(treeStr, 2);

            // todo - add start value
            var actual = _search.Search(tree, depth, sbyte.MinValue, sbyte.MaxValue, startState, _cts.Token);
            Assert.Equal(expected, actual);
        }
    }
}