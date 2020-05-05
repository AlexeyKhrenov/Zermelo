using CheckersAI.CheckersGameTree;
using CheckersAI.InternalInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZermeloUnitTests.Mocks
{
    internal class TreeManagerMock : ITreeManager<GameNode, sbyte>
    {
        public GameNode GoDownToNode(GameNode node)
        {
            return node;
        }
    }
}
