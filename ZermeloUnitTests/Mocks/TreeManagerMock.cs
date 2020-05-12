using CheckersAI.CheckersGameTree;
using CheckersAI.InternalInterfaces;

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