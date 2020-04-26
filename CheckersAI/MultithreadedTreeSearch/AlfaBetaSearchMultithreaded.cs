using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CheckersAI.MultithreadedTreeSearch
{
    internal class AlfaBetaSearchMultithreaded<TNode, TValue, TMetric>
        where TValue : struct
        where TNode : class, IAlfaBetaNode<TNode, TValue, TMetric>
        where TMetric : struct
    {
        private IEvaluator<TNode, TValue, TMetric> _evaluator;
        private IBrancher<TNode, TValue, TMetric> _brancher;
        private IComparator<TMetric> _comparator;

        public AlfaBetaSearchMultithreaded(
            IEvaluator<TNode, TValue, TMetric> evaluator,
            IBrancher<TNode, TValue, TMetric> brancher,
            IComparator<TMetric> comparator
        )
        {
            _evaluator = evaluator;
            _brancher = brancher;
            _comparator = comparator;
        }

        public void Search(TNode tree, int depth)
        {
            var threads = new Thread[8];

            for (var i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(() => Traverse(tree, depth));
                threads[i].Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }
        }

        private int opCount = 0;

        // move min and max arguments to the constructor
        public void ClearTree(TNode[] tree, TMetric maxValue, TMetric minValue)
        {
            foreach (var node in tree)
            {
                node.Alfa = minValue;
                node.Beta = maxValue;
            }
        }

        public void Traverse(TNode node, int maxDepth)
        {
            TNode currentNode = node;

            if (node.Children.Length != 0)
            {
                // use iterative approach to save stack for huge trees
                while (currentNode != null && node.FinalizedFlag != 0)
                {
                    currentNode = NextNode(currentNode, maxDepth);
                }
            }
            else
            {
                var res = _evaluator.Evaluate(node);
                node.Alfa = res;
                node.Beta = res;
            }
        }

        private TNode NextNode(TNode node, int maxDepth)
        {
            Interlocked.Increment(ref opCount);

            if (node.FinalizedFlag == 0)
            {
                return UpdateParent(node);
            }

            if (node.IsCutOff)
            {
                node.Parent.UpdateFinalizedFlag(node.ChildAddressBit);
                return node.Parent;
            }

            if (node.Children == null)
            {
                _brancher.Branch(node);
            }

            if (node.Depth == maxDepth || node.Children.Length == 0)
            {
                node.IsAnnounced = true;
                var res = _evaluator.Evaluate(node);
                node.Update(res, 1);
                return UpdateParent(node);
            }

            foreach (var child in node.Children)
            {
                if (child.FinalizedFlag != 0 && !child.IsCutOff)
                {
                    if (!child.IsAnnounced)
                    {
                        child.UpdateAlfaBeta(node.Alfa, node.Beta);
                        return child;
                    }
                }
            }

            node.IsAnnounced = true;
            return node.Parent;
        }

        private TNode UpdateParent(TNode node)
        {
            var parent = node.Parent;
            parent.Update(node.Result, node.ChildAddressBit);
            return parent;
        }
    }
}