using System.Collections.Generic;
using System.Threading;
using Checkers.Minifications;
using CheckersAI.CheckersGameTree;
using CheckersAI.InternalInterfaces;

namespace ZermeloUnitTests.Mocks
{
    internal class ProgressiveWrapperMock : IProgressiveDeepeningWrapper<GameNode, sbyte, BoardMinified>
    {
        public Queue<GameNode> Result { get; set; }

        public (Queue<GameNode>, int) Run(BoardMinified state, GameNode node, sbyte alfa, sbyte beta, int maxDepth,
            CancellationToken ct)
        {
            return (Result, 0);
        }
    }
}