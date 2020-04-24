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
            //if (node.Parent != null)
            //{
            //    if (NeedToCutOff(node.Parent))
            //    {
            //        return;
            //    }
            //}

            if (depth == 0)
            { 
                var result = await _evaluator.Evaluate(node);
                // change this logic to be more recursive
                node.Alfa = result;
                node.Beta = result;
                await GoUp(node);
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

        
        private async Task GoUp(TNode node)
        {
            var parent = node.Parent;

            if (parent == null)
            {
                return;
            }

            var newValue = GetResult(node);

            if (parent.IsMaxPlayer)
            {
                if (_comparator.IsBigger(parent.Alfa, newValue))
                {
                    if (ReferenceEquals(parent.BestMove, node))
                    {
                        var bestMove = FindMaxNode(parent.Children);
                        parent.BestMove = bestMove;
                        parent.Alfa = GetResult(bestMove);
                        await GoUp(parent);
                    }
                }
                else
                {
                    if (_comparator.IsBigger(newValue, parent.Alfa))
                    {
                        parent.Alfa = newValue;
                        parent.BestMove = node;
                        await GoUp(parent);
                    }

                }
            }
            else
            {
                if (_comparator.IsBigger(parent.Beta, newValue))
                {
                    parent.Beta = newValue;
                    parent.BestMove = node;
                    await GoUp(parent);
                }
                else
                {
                    if (ReferenceEquals(parent.BestMove, node))
                    {
                        if (_comparator.IsBigger(newValue, parent.Beta))
                        {
                            var bestMove = FindMinNode(parent.Children);
                            parent.Beta = GetResult(bestMove);
                            parent.BestMove = bestMove;
                            await GoUp(parent);
                        }
                    }
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
            return !_comparator.IsBigger(parent.Beta, parent.Alfa);
        }
    }
}
