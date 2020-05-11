using Checkers.Minifications;
using CheckersAI.CheckersGameTree;
using CheckersAI.InternalInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ZermeloUnitTests.Mocks
{
    internal class GameTreeSearchMock : ISearch<GameNode, sbyte, BoardMinified>
    {
        public int EstimateRequiredMemoryUsageIncrementInMb(int startDepth, int endDepth)
        {
            return 0;
        }

        public sbyte Search(GameNode node, int depth, sbyte alfa, sbyte beta, BoardMinified state, CancellationToken cancellationToken)
        {
            return sbyte.MinValue;
        }
    }
}
