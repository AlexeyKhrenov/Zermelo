using Benchmarking;
using CheckersAI.ByteTree;
using CheckersAI.TreeSearch;
using Xunit;

namespace ZermeloUnitTests.Search
{
    public class SearchWithoutBranchingTest
    {
        private AlfaBetaSearch<ByteNode, sbyte, sbyte> _search;

        public SearchWithoutBranchingTest()
        {
            var comparator = new Comparator();
            var brancher = new Brancher();
            var evaluator = new Evaluator();
            var stateTransitions = new StateTransitions();
            _search = new AlfaBetaSearch<ByteNode, sbyte, sbyte>(
                evaluator,
                brancher,
                comparator,
                stateTransitions,
                sbyte.MaxValue,
                sbyte.MinValue
            );
        }

        [Theory]
        [InlineData("1", 4, 0, 3)]
        [InlineData("7 -2 -3", 3, 1, -2)]
        [InlineData("1 -2 0 -6 8 1 0", 1, 2, 0)]
        [InlineData("2 -1 -3 -4 0 7 2 1 1 13 -5 0 -6 -7 3", 9, 3, 5)]
        public void SearchWithoutBranchingTheory(string treeStr, sbyte expected, int depth, sbyte startState)
        {
            var tree = TreeGenerator.ParseByteTree(treeStr, 2);

            // todo - add start value
            var actual = _search.Search(tree, depth, sbyte.MinValue, sbyte.MaxValue, startState);
            Assert.Equal(expected, actual);
        }
    }
}
