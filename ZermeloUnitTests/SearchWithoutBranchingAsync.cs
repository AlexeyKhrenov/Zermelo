using Benchmarking;
using Benchmarking.ByteTree;
using CheckersAI.AsyncTreeSearch;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ZermeloUnitTests
{
    public class SearchWithoutBranchingAsync
    {
        private AlfaBetaSearchMultithreaded<AlfaBetaByteNode, byte, byte> _search;

        public SearchWithoutBranchingAsync()
        {
            var comparator = new Comparator();
            var brancher = new BrancherMock();
            var evaluator = new Evaluator();

            _search = new AlfaBetaSearchMultithreaded<AlfaBetaByteNode, byte, byte>(evaluator, brancher, comparator);
        }

        [Theory]
        [InlineData("1", 1, 0)]
        [InlineData("2 1", 2, 1)]
        [InlineData("1 2", 2, 1)]
        [InlineData("2 7 1 8", 2, 2)]
        [InlineData("8 1 7 2", 2, 2)]
        [InlineData("1 3 5 1 1 1 0 9", 3, 3)]
        [InlineData("8 7 3 9 9 8 2 4 1 8 8 9 9 9 3 4", 8, 4)]
        public void SearchWithoutBranchingAsyncTheory(string treeStr, byte expected, int depth)
        {
            var tree = TreeGenerator.ParseAsAlfaBetaTree(treeStr, 2);
            _search.Search(tree, depth);
            var actual = tree.Alfa;
            Assert.Equal(expected, actual);
        }
    }
}
