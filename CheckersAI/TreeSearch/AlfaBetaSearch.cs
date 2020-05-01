namespace CheckersAI.TreeSearch
{
    // the implementation may have different output range and therefore different return type
    internal class AlfaBetaSearch<TNode, TMetric, TState> 
        where TNode : INode<TNode, TMetric>
        where TMetric : struct
        where TState : struct
    {
        private IEvaluator<TState, TMetric> _evaluator;
        private IBrancher<TNode, TState, TMetric> _brancher;
        private IComparator<TMetric> _comparator;
        private IStateTransitions<TState, TNode, TMetric> _stateTransitions;
        private TMetric _maxValue;
        private TMetric _minValue;

        public AlfaBetaSearch(
            IEvaluator<TState, TMetric> evaluator,
            IBrancher<TNode, TState, TMetric> brancher,
            IComparator<TMetric> comparator,
            IStateTransitions<TState, TNode, TMetric> stateTransitions,
            TMetric maxValue,
            TMetric minValue
        )
        {
            _evaluator = evaluator;
            _brancher = brancher;
            _comparator = comparator;
            _stateTransitions = stateTransitions;
            _minValue = minValue;
            _maxValue = maxValue;
        }

        public TMetric Search(TNode node, int depth, TMetric alfa, TMetric beta, TState state)
        {
            if (depth == 0)
            {
                var result = _evaluator.Evaluate(state);
                node.Result = result;
                return result;
            }

            if ((node.Children == null || node.Children.Length == 0) && depth > 0)
            {
                _brancher.Branch(node, state);
            }

            if (node.IsMaxPlayer)
            {
                var maxVal = _minValue;
                foreach (var child in node.Children)
                {
                    var childState = _stateTransitions.GoDown(state, child);
                    var result = Search(child, depth - 1, alfa, beta, childState);
                    // todo - remove this state undo after everything becomes struct
                    childState = _stateTransitions.GoUp(state, child);

                    maxVal = _comparator.IsBigger(result, maxVal) ? result : maxVal;

                    if (!_comparator.IsBigger(beta, result))
                    {
                        break;
                    }

                    alfa = _comparator.IsBigger(result, alfa) ? result : alfa;
                }

                // todo - refactor - remove extra variables
                node.Result = maxVal;
                return maxVal;
            }
            else
            {
                var minVal = _maxValue;
                foreach (var child in node.Children)
                {
                    var childState = _stateTransitions.GoDown(state, child);
                    var result = Search(child, depth - 1, alfa, beta, childState);
                    // todo - remove this state undo after everything becomes struct
                    childState = _stateTransitions.GoUp(state, child);

                    minVal = !_comparator.IsBigger(result, minVal) ? result : minVal;

                    if (!_comparator.IsBigger(result, alfa))
                    {
                        break;
                    }

                    beta = _comparator.IsBigger(beta, result) ? result : beta;
                }

                // todo - refactor - remove extra variables
                node.Result = minVal;
                return minVal;
            }
        }
    }
}
