using CheckersAI.TreeSearch;
using Game.Primitives;

namespace CheckersAI.CheckersGameTree
{
    public class GameNode : INode<GameNode>
    {
        public GameNode[] Children { get; set; }

        public Move Move{ get; set; }

        public bool IsMaxPlayer { get; set; }

        public GameNode(bool isMaxPlayer)
        {
        }
    }
}
