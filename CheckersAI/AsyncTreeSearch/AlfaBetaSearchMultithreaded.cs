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
                threads[i].Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }
        }

        // move min and max arguments to the constructor
        public void ClearTree(TNode[] tree, TMetric maxValue, TMetric minValue)
        {
            foreach (var node in tree)
            {
                node.Alfa = minValue;
                node.Beta = maxValue;
            }
        }

        public void Traverse(TNode node, int depth)
        {
            TNode currentNode = node;

            // use iterative approach to save stack for huge trees
            while (!node.IsFinalized)
            {
                var next = GoDown(currentNode, depth);
                if (next != null)
                {
                    depth--;
                    currentNode = next;
                }
                else
                {
                    next = GoUp(currentNode);
                    if (next != null)
                    {
                        depth++;
                        currentNode = next;
                    }
                }
            }
        }

        private TNode GoDown(TNode node, int depth)
        {
            if (node.IsFinalized || CheckNodeCutoff(node) || node.IsAnnounced)
            {
                return default;
            }

            if (node.Children == null)
            {
                // need to lock the node for branching
                _brancher.Branch(node);
            }

            if (depth == 0 || node.Children.Length == 0)
            {
                //todo - remove evaluation - everything should be evaluated by branchers
                var result = _evaluator.Evaluate(node);
                node.Alfa = result;
                node.Beta = result;

                UpdateParent(node.Parent, result);

                node.IsFinalized = true;

                return default;
            }

            var finalizedOrCutoff = 0;
            foreach (var child in node.Children)
            {
                if (child.IsFinalized || child.WasCutOff)
                {
                    finalizedOrCutoff++;
                    continue;
                }

                if (!child.IsAnnounced)
                {
                    return child;
                }
            }

            if (node.Children.Length == finalizedOrCutoff)
            {
                UpdateParent(node.Parent, GetResult(node));
                node.IsFinalized = true;
            }
            else
            {
                node.IsAnnounced = true;
            }

            return default;
        }

        private bool CheckNodeCutoff(TNode node)
        {
            if (node.WasCutOff)
            {
                return true;
            }

            if (_comparator.IsBigger(node.Alfa, node.Beta))
            {
                node.WasCutOff = true;
                return true;
            }

            return false;
        }

        //always returns parent
        private TNode GoUp(TNode node)
        {
            var parent = node.Parent;
            return parent;
        }

        private void UpdateParent(TNode parent, TMetric newResult)
        {
            if (parent != null)
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

                CheckNodeCutoff(parent);
            }
        }

        private TMetric GetResult(TNode node)
        {
            return node.IsMaxPlayer ? node.Alfa : node.Beta;
        }
    }
}