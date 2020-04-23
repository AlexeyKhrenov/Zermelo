using System.Threading.Tasks;

namespace CheckersAI.AsyncTreeSearch
{
    internal class AlfaBetaSearchMultithreaded<TNode, TValue, TMetric>
        where TValue : struct
        where TNode : IAlfaBetaNode<TNode, TValue, TMetric>
        where TMetric : struct
    {
        private IEvaluator<TNode, TValue, TMetric> _evaluator;
        private IBrancher<TNode, TValue, TMetric> _brancher;
        private IComparator<TMetric> _comparator;
        private TMetric _maxValue;
        private TMetric _minValue;

        public AlfaBetaSearchMultithreaded(
            IEvaluator<TNode, TValue, TMetric> evaluator,
            IBrancher<TNode, TValue, TMetric> brancher,
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

        private async Task<TMetric> GoDown(TNode node, int depth)
        {
            if (depth == 0)
            {
                var result = await _evaluator.Evaluate(node);
                await GoUp(node, result);
            }

            if ((node.Children == null || node.Children.Length == 0) && depth > 0)
            {
                await _brancher.Branch(node);
            }
        }

        private async Task<TMetric> GoUp(TNode node, TMetric result)
        {
            if (node.IsMaxPlayer)
            {
                // todo - change to remember where better result came from
                if (_comparator.IsBigger(result, node.Alfa))
                {
                    node.Alfa = result;

                    // go up only if needed
                    if (_comparator.IsBigger(node.Beta, node.Alfa))
                    {
                        if (node.Parent != null)
                        {
                            await GoUp(node.Parent, node.Alfa);
                        }
                    }
                    else
                    {
                        // no need to go up
                    }
                }
            }
            else
            {
                // todo - change to remember where better result came from
                if (_comparator.IsBigger(node.Beta, result))
                {
                    // go up only if beta is updated
                    node.Beta = result;
                    if (_comparator.IsBigger(node.Beta, node.Alfa)) //todo - try to switch case check to improve perfomance
                    {
                        if (node.Parent != null)
                        {
                            await GoUp(node.Parent, node.Beta);
                        }
                    }
                }
            }
        }

        private void UpdateMaximizer(TNode maximizer, TMetric result)
        {

        }

        /// <summary>
        /// alfa - MinValue, beta - MaxValue
        /// </summary>
        private TMetric SearchAlfaBeta(TNode node, int depth, TMetric alfa, TMetric beta)
        {
            if ((node.Children == null || node.Children.Length == 0) && depth > 0)
            {
                _brancher.Branch(node);
                foreach (var children in node.Children)
                {
                }
            }

            if (depth == 0 || node.Children == null || node.Children.Length == 0)
            {
                return _evaluator.Evaluate(node);
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
