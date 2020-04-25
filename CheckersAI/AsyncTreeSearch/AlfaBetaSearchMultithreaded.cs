using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CheckersAI.AsyncTreeSearch
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
            var threads = new Thread[Environment.ProcessorCount];

            for (var i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(() => Traverse(tree, depth));
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }
        }

        public void Traverse(TNode node, int depth)
        {
            TNode currentNode = node;

            // use iterative approach to save stack for huge trees
            while (!node.IsFinalized)
            {
                depth--;
                var next = GoDown(node, depth);
                if (next != null)
                {
                    currentNode = next;
                }
                else
                {
                    currentNode = GoUp(currentNode);
                }
            }
        }

        private TNode GoDown(TNode node, int depth)
        {
            if (CheckNodeFinalized(node) || CheckNodeCuttoff(node) || CheckNodeAnnounced(node))
            {

            }

            if (node.Children == null)
            {
                // need to lock the node for branching
                _brancher.Branch(node);
            }

            if (depth == 0 || node.Children.Length == 0)
            {
                node.IsFinalized = true;

                //todo - remove evaluation - everything should be evaluated by branchers
                var result = _evaluator.Evaluate(node);
                node.Alfa = result;
                node.Beta = result;
                return default;
            }

            foreach (var child in node.Children)
            {
                if (!child.IsFinalized && !child.IsAnnounced && !child.WasCutOff)
                {
                    return child;
                }
            }

            node.IsAnnounced = true;
            return default;
        }

        //always returns parent
        private TNode GoUp(TNode node)
        {
            var parent = node.Parent;

            if (node.IsFinalized)
            {
                var newResult = GetResult(node);
                parent.ChildrenPropagatedCount++;
                UpdateParent(parent, newResult);
            }

            return parent;
        }

        private bool CanProcessNode(TNode node)
        {
            if (node.WasCutOff || node.IsFinalized || node.IsAnnounced)
            {
                return false;
            }

            if (_comparator.IsBigger(node.Alfa, node.Beta))
            {
                node.WasCutOff = true;
                return true;
            }

            return false;
        }

        private void UpdateParent(TNode parent, TMetric newResult)
        {
            if (parent.IsMaxPlayer)
            {
                // interlock parent
                if (_comparator.IsBigger(newResult, parent.Alfa))
                {
                    parent.Alfa = newResult;
                }
            }
            else
            {
                // interlock parent
                if (_comparator.IsBigger(parent.Beta, newResult))
                {
                    parent.Beta = newResult;
                }
            }
        }

        private TMetric GetResult(TNode node)
        {
            return node.IsMaxPlayer ? node.Alfa : node.Beta;
        }
    }
}