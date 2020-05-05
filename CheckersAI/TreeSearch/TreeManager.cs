using CheckersAI.InternalInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersAI.TreeSearch
{
    internal class TreeManager<TNode, TMetric> : ITreeManager<TNode, TMetric>
        where TNode : INode<TNode, TMetric>
        where TMetric : struct
    {
        private TNode _root;

        public TNode GoDownToNode(TNode node)
        {
            if (_root == null)
            {
                _root = node;
                return node;
            }

            var queue = new Queue<TNode>();
            queue.Enqueue(_root);

            while (queue.Count != 0)
            {
                var nextNode = queue.Dequeue();
                if (nextNode.Equals(node))
                {
                    return nextNode;
                }
                else
                {
                    if (nextNode.Children != null)
                    {
                        foreach (var child in nextNode.Children)
                        {
                            queue.Enqueue(child);
                        }
                    }

                    // todo - implement explicit garbage collection 
                    nextNode.Children = null;
                }
            }

            _root = node;
            return node;
        }
    }
}
