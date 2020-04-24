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

        public void Search(TNode node, int depth)
        {
            Task.Run(() => GoDown(node, depth)).Wait();
        }

        private async Task GoDown(TNode node, int depth)
        {
            if (node.Parent != null)
            {
                if (NeedToCutOff(node.Parent))
                {
                    return;
                }
            }

            if (depth == 0)
            { 
                var result = await _evaluator.Evaluate(node);
                // change this logic to be more recursive
                node.Alfa = result;
                node.Beta = result;
                await Bubble(node, result);
            }

            if ((node.Children == null || node.Children.Length == 0) && depth > 0)
            {
                await _brancher.Branch(node);
            }

            var tasks = new Task[node.Children.Length];
            for (var i = 0; i < node.Children.Length; i++)
            {
                tasks[i] = GoDown(node.Children[i], depth - 1); //todo - check for closure
            }

            await Task.WhenAll(tasks);
        }

        
        private async Task Bubble(TNode node, TMetric newValue)
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
                    await Bubble(node.Parent, GetResult(node));
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
