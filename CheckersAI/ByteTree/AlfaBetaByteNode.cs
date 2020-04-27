using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace CheckersAI.ByteTree
{
    internal class AlfaBetaByteNode : MultithreadedTreeSearch.AlfaBetaNodeBase<AlfaBetaByteNode, sbyte, sbyte>
    {
        public AlfaBetaByteNode(sbyte value, bool isMaxPlayer, params AlfaBetaByteNode[] children)
        {
            Value = value;
            IsMaxPlayer = isMaxPlayer;
            Children = children;

            Alfa = sbyte.MinValue;
            Beta = sbyte.MaxValue;

            if (IsMaxPlayer)
            {
                Result = sbyte.MinValue;
            }
            else
            {
                Result = sbyte.MaxValue;
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

            while (stack.Count != 0)
            {
                var next = stack.Pop();
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

            while (level.Count != 0)
            {
                var nextNode = level.Dequeue();
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
        public override void Update(AlfaBetaByteNode node)
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

        public override void UpdateAlfaBeta(sbyte alfa, sbyte beta)
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

        public override void Clear()
        {
            Alfa = sbyte.MinValue;
            Beta = sbyte.MaxValue;
            Result = 0;
        }
    }
}
