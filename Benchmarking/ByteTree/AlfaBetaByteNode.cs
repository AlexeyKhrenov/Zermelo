using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Benchmarking.ByteTree
{
    public class AlfaBetaByteNode : CheckersAI.MultithreadedTreeSearch.IAlfaBetaNode<AlfaBetaByteNode, byte, byte>
    {
        public byte Alfa { get; set; }

        public byte Beta { get; set; }

        public byte Value { get; set; }

        public AlfaBetaByteNode[] Children { get; set; }

        public AlfaBetaByteNode Parent { get; set; }

        public bool IsMaxPlayer { get; set; }

        public byte Result { get; set; }

        public bool IsAnnounced { get; set; }

        public bool IsCutOff { get; set; }

        public bool IsFinalized => _finalizedFlag == 0;

        public int Depth { get; set; }

        private uint _childAddressBit;
        private uint _finalizedFlag;

        public AlfaBetaByteNode(byte value, bool isMaxPlayer, params AlfaBetaByteNode[] children)
        {
            Value = value;
            IsMaxPlayer = isMaxPlayer;
            Children = children;

            Alfa = byte.MinValue;
            Beta = byte.MaxValue;

            if (IsMaxPlayer)
            {
                Result = byte.MinValue;
            }
            else
            {
                Result = byte.MaxValue;
            }

            for (var i = 0; i < Children.Length; i++)
            {
                uint childAddressBit = (uint)(1 << i);
                Children[i]._childAddressBit = childAddressBit;
                _finalizedFlag |= childAddressBit;
            }

            // todo - remove this when dynamic branching is created
            if (Children.Length == 0)
            {
                _finalizedFlag = 1;
            }
        }

        public void LinkBackChildren()
        {
            foreach (var child in Children)
            {
                child.Parent = this;
            }
        }

        public int GetDepth()
        {
            if (Children == null || Children.Length == 0)
            {
                return 0;
            }

            return Children[0].GetDepth() + 1;
        }

        // todo - rename this method
        public void EnumerateDepth()
        {
            var stack = new Stack<AlfaBetaByteNode>();
            stack.Push(this);

            while (stack.TryPop(out var next))
            {
                foreach (var child in next.Children)
                {
                    child.Depth = next.Depth + 1;
                    stack.Push(child);
                }
            }
        }

        public List<AlfaBetaByteNode> ToList()
        {
            var list = new List<AlfaBetaByteNode>();
            var level = new Queue<AlfaBetaByteNode>();

            level.Enqueue(this);

            while (level.TryDequeue(out var nextNode))
            {
                list.Add(nextNode);
                if (nextNode.Children != null)
                {
                    foreach (var child in nextNode.Children)
                    {
                        level.Enqueue(child);
                    }
                }
            }

            return list;
        }

        object _obj = new object();

        // todo - change to quicker implementation
        public void Update(AlfaBetaByteNode node)
        {
            var newValue = node.Result;

            lock (_obj)
            {
                if (IsMaxPlayer)
                {
                    if (newValue > Result) Result = newValue;
                    if (newValue > Alfa) Alfa = newValue;
                }
                else
                {
                    if (newValue < Result) Result = newValue;
                    if (newValue < Beta) Beta = newValue;
                }

                if (Alfa > Beta)
                {
                    IsCutOff = true;
                }

                _finalizedFlag &= ~node._childAddressBit;
            }
        }

        object _obj2 = new object();
        public void UpdateFinalizedFlag(AlfaBetaByteNode node)
        {
            var finalizedBit = node._childAddressBit;

            lock (_obj2)
            {
                _finalizedFlag &= ~finalizedBit;
            }
        }

        public void UpdateTerminalNode(byte result)
        {
            Result = result;
            _finalizedFlag = 0;
        }

        public void UpdateAlfaBeta(byte alfa, byte beta)
        {
            if (IsMaxPlayer)
            {
                if (beta < Beta)
                {
                    Beta = beta;
                }
            }
            else
            {
                if (alfa > Alfa)
                {
                    Alfa = alfa;
                }
            }

            if (Alfa > Beta)
            {
                IsCutOff = true;
            }
        }

        public void Clear()
        {
            Alfa = byte.MinValue;
            Beta = byte.MaxValue;
            Result = 0;
        }
    }
}
