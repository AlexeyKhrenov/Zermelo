using Benchmarking;
using Benchmarking.ByteTree;
using CheckersAI.TreeSearch;
using Xunit;

namespace ZermeloUnitTests
{
    public class SearchWithoutBranchingTest
    {
        private AlfaBetaSearch<ByteNode, byte, byte> _search;

        public SearchWithoutBranchingTest()
        {
            var comparator = new Comparator();
            var brancher = new BrancherMock();
            var evaluator = new Evaluator();
            _search = new AlfaBetaSearch<ByteNode, byte, byte>(evaluator, brancher, comparator, byte.MaxValue, byte.MinValue);
        }

        [Theory]
        [InlineData("1", 1, 0)]
        [InlineData("2 1", 2, 1)]
        [InlineData("2 7 1 8", 2, 2)]
        [InlineData("1 3 5 1 1 1 0 9", 3, 3)]
        [InlineData("8 7 3 9 9 8 2 4 1 8 8 9 9 9 3 4", 8, 4)]
        public void SearchWithoutBranchingTheory(string treeStr, byte expected, int depth)
        {
            var tree = TreeGenerator.ParseByteTree(treeStr, 2);
            var actual = _search.Search(tree, depth);
            Assert.Equal(expected, actual);
        }
    }
}
