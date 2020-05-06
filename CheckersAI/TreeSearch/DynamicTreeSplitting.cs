using CheckersAI.InternalInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CheckersAI.TreeSearch
{
    internal class DynamicTreeSplitting<TNode, TMetric, TState> : ISearch<TNode, TMetric, TState>
        where TNode : IAlfaBetaNode<TNode, TMetric>
        where TMetric : struct
        where TState : struct
    {
        private IEvaluator<TState, TMetric> _evaluator;
        private IBrancher<TNode, TState, TMetric> _brancher;
        private IComparator<TMetric> _comparator;
        private IStateTransitions<TState, TNode, TMetric> _stateTransitions;
        private TMetric _maxValue;
        private TMetric _minValue;
        private ManualResetEvent _manualResetEvent;

        public DynamicTreeSplitting(
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

            _manualResetEvent = new ManualResetEvent(false);
        }

        public TMetric Search(TNode node, int depth, TMetric alfa, TMetric beta, TState state, CancellationToken cancellationToken)
        {
            _manualResetEvent = new ManualResetEvent(false);

            node.Alfa = alfa;
            node.Beta = beta;

            GoDown(node, state, depth, node);

            _manualResetEvent.WaitOne();

            return node.Result;
        }

        private void GoDown(TNode node, TState state, int depth, TNode splittedFrom)
        {
            // todo - remove comparator
            if (_comparator.IsBigger(splittedFrom.Alfa, splittedFrom.Beta))
            {
                return;
            }

            if (depth == 0)
            {
                node.Update(_evaluator.Evaluate(state));
                GoUp(node, depth, state);
                return;
            }

            if (node.Children == null)
            {
                _brancher.Branch(node, state);
            }

            if (node.Children.Length == 0)
            {
                node.Update(_evaluator.Evaluate(state));
                GoUp(node, depth, state);
                return;
            }

            foreach (var child in node.Children)
            {
                if (child.TryLockNode())
                {
                    var localState = _stateTransitions.GoDown(state, child);
                    GoDown(child, localState, depth - 1, splittedFrom);
                    break;
                }
            }
        }

        private void GoUp(TNode node, int depth, TState state)
        {
            var parent = node.Parent;
            if (parent != null)
            {
                parent.Update(node);

                var localState = _stateTransitions.GoUp(state, node);

                if (parent.IsFinalized)
                {
                    GoUp(parent, depth + 1, localState);
                }
                else
                {
                    SplitNode(parent, depth + 1, localState);
                }
            }
            else
            {
                if (node.IsFinalized)
                {
                    _manualResetEvent.Set();
                }
            }
        }

        private void SplitNode(TNode node, int depth, TState state)
        {
            foreach (var child in node.Children)
            {
                if (child.TryLockNode())
                {
                    var localState = _stateTransitions.GoDown(state, child);
                    ThreadPool.QueueUserWorkItem(
                        obj =>
                        GoDown(child, localState, depth - 1, node));
                }
            }
        }

        public void ClearTree(TNode node)
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

        public Queue<TNode> DoProgressiveDeepening(TNode node, TState state, TMetric alfa, TMetric beta, int maxDepth, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
