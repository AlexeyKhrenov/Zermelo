using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckersAI.AsyncTreeSearch
{
    internal class AlfaBetaSearchTaskBased<TNode, TValue, TMetric>
        where TValue : struct
        where TNode : IAlfaBetaNode<TNode, TValue, TMetric>
        where TMetric : struct
    {
        private IEvaluator<TNode, TValue, TMetric> _evaluator;
        private IBrancher<TNode, TValue, TMetric> _brancher;
        private IComparator<TMetric> _comparator;

        public AlfaBetaSearchTaskBased(
            IEvaluator<TNode, TValue, TMetric> evaluator,
            IBrancher<TNode, TValue, TMetric> brancher,
            IComparator<TMetric> comparator
        )
        {
            _evaluator = evaluator;
            _brancher = brancher;
            _comparator = comparator;
        }

        public void Search(TNode node, int depth)
        {
            GoDown(node, depth);
        }

        public void ClearTree(TNode[] nodes, TMetric maxValue, TMetric minValue)
        {
            foreach (var node in nodes)
            {
                node.Alfa = minValue;
                node.Beta = maxValue;
            }
        }

        private int _cutted = 0;
        private void GoDown(TNode node, int depth)
        {
            if (node.Parent != null)
            {
                if (NeedToCutOff(node.Parent))
                {
                    _cutted++;
                    return;
                }
            }

            if (depth == 0)
            { 
                var result = _evaluator.Evaluate(node);
                // change this logic to be more recursive
                node.Alfa = result;
                node.Beta = result;
                Bubble(node, result);
            }

            if ((node.Children == null || node.Children.Length == 0) && depth > 0)
            {
                _brancher.Branch(node);
            }

            Parallel.ForEach(node.Children, x => GoDown(x, depth - 1));
        }
        
        private void Bubble(TNode node, TMetric newValue)
        {
            if (node.IsMaxPlayer)
            {
                if(_comparator.IsBigger(newValue, node.Alfa))
                {
                    node.Alfa = newValue;
                }
            }
            else
            {
                if(_comparator.IsBigger(node.Beta, newValue))
                {
                    node.Beta = newValue;
                }
            }

            node.ChildrenPropagatedCount++;
            if(_comparator.IsBigger(node.Alfa, node.Beta))
            {
                node.IsFinalizedDuringSearch = true;
            }

            if(!_comparator.IsBigger(node.Alfa, node.Beta) &&
                !_comparator.IsBigger(node.Beta, node.Alfa) ||
                node.Children.Length == node.ChildrenPropagatedCount)
            {
                node.IsFinalizedDuringSearch = true;
                if (node.Parent != null)
                {
                    Bubble(node.Parent, GetResult(node));
                }
            }
        }

        private TMetric GetResult(TNode node)
        {
            return node.IsMaxPlayer ? node.Alfa : node.Beta;
        }

        private TNode FindMaxNode(TNode[] nodes)
        {
            var max = nodes[0];
            foreach (var node in nodes)
            {
                if (IsBigger(node, max))
                {
                    max = node;
                }
            }
            return max;
        }

        private TNode FindMinNode(TNode[] nodes)
        {
            var min = nodes[0];
            foreach (var node in nodes)
            {
                if (IsBigger(min, node))
                {
                    min = node;
                }
            }
            return min;
        }

        private bool IsBigger(TNode a, TNode b)
        {
            return _comparator.IsBigger(GetResult(a), GetResult(b));
        }

        private bool NeedToCutOff(TNode parent)
        {
            return parent.IsFinalizedDuringSearch;
        }
    }
}
