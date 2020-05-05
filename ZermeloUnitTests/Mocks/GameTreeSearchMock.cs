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
        public void ClearTree(GameNode node)
        {
            throw new NotImplementedException();
        }

        public Queue<GameNode> DoProgressiveDeepening(GameNode node, BoardMinified state, sbyte alfa, sbyte beta, int maxDepth, CancellationToken ct)
        {
            return new Queue<GameNode>();
        }

        public sbyte Search(GameNode node, int depth, sbyte alfa, sbyte beta, BoardMinified state, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
