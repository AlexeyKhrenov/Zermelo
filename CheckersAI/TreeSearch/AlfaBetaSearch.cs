using CheckersAI.Tree;

namespace CheckersAI.TreeSearch
{
    // the implementation may have different output range and therefore different return type
    internal class AlfaBetaSearch<TNode, TValue, TMetric> 
        where TValue : struct 
        where TNode : INode<TNode, TValue>
        where TMetric : struct
    {
        private IEvaluator<TNode, TValue, TMetric> _evaluator;
        private IBrancher<TNode, TValue> _brancher;
        private IComparator<TMetric> _comparator;
        private TMetric _maxValue;
        private TMetric _minValue;

        public AlfaBetaSearch(
            IEvaluator<TNode, TValue, TMetric> evaluator,
            IBrancher<TNode, TValue> brancher,
            IComparator<TMetric> comparator,
            TMetric maxValue,
            TMetric minValue
        )
        {
            _evaluator = evaluator;
            _brancher = brancher;
            _comparator = comparator;
            _minValue = minValue;
            _maxValue = maxValue;
        }

        public TMetric Search(TNode node, int depth)
        {
            return SearchAlfaBeta(node, depth, _minValue, _maxValue);
        }

        /// <summary>
        /// alfa - MinValue, beta - MaxValue
        /// </summary>
        private TMetric SearchAlfaBeta(TNode node, int depth, TMetric alfa, TMetric beta)
        {
            if (depth == 0)
            {
                return _evaluator.Evaluate(node);
            }

            if ((node.Children == null || node.Children.Length == 0) && depth > 0)
            {
                _brancher.Branch(node);
            }

            if (node.IsMaxPlayer)
            {
                var maxVal = _minValue;
                foreach (var child in node.Children)
                {
                    var result = SearchAlfaBeta(child, depth - 1, alfa, beta);
                    maxVal = _comparator.IsBigger(result, maxVal) ? result : maxVal;

                    if (!_comparator.IsBigger(beta, result))
                    {
                        break;
                    }

                    alfa = _comparator.IsBigger(result, alfa) ? result : alfa;
                }
                return maxVal;
            }
            else
            {
                var minVal = _maxValue;
                foreach (var child in node.Children)
                {
                    var result = SearchAlfaBeta(child, depth - 1, alfa, beta);
                    minVal = !_comparator.IsBigger(result, minVal) ? result : minVal;

                    if (!_comparator.IsBigger(result, alfa))
                    {
                        break;
                    }

                    beta = _comparator.IsBigger(beta, result) ? result : beta;
                }
                return minVal;
            }
        }
    }
}
