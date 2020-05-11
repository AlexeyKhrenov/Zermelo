using CheckersAI.InternalInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading; 

namespace CheckersAI.TreeSearch
{
    internal class ProgressiveDeepeningWrapper<TNode, TMetric, TState> : IProgressiveDeepeningWrapper<TNode, TMetric, TState>
        where TNode : INode<TNode, TMetric>
        where TMetric : struct
        where TState : struct
    {
        private ISearch<TNode, TMetric, TState> _search;

        public ProgressiveDeepeningWrapper(ISearch<TNode, TMetric, TState> search)
        {
            _search = search;
        }

        public (Queue<TNode>, int) Run(TState state,
            TNode node,
            TMetric alfa,
            TMetric beta,
            int maxDepth,
            CancellationToken ct)
        {
            var depth = 0;
            var result = new Queue<TNode>();

            while (!ct.IsCancellationRequested)
            {
                try
                {
                    if (depth > maxDepth)
                    {
                        break;
                    }

                    _search.Search(node, depth, alfa, beta, state, ct);

                    result.Clear();
                    var nextNode = node.BestChild;
                    while (nextNode != null)
                    {
                        result.Enqueue(nextNode);
                        if (nextNode.IsMaxPlayer != node.IsMaxPlayer)
                        {
                            break;
                        }
                        nextNode = nextNode.BestChild;
                    }

                    ClearTree(node);
                    depth++;
                }
                catch (OperationCanceledException)
                {
                }
            }

            return (result, depth);
        }

        // todo - look for perfomance here
        public static void ClearTree(TNode node)
        {
            var queue = new Queue<TNode>();
            queue.Enqueue(node);

            while (queue.Count > 0)
            {
                var next = queue.Dequeue();
                next.Clear();
                if (next.Children != null)
                {
                    foreach (var child in next.Children)
                    {
                        queue.Enqueue(child);
                    }
                }
            }
        }
    }
}
