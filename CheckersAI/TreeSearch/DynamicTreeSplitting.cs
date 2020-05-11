using CheckersAI.InternalInterfaces;
using System;
using System.Collections.Generic;
using System.Runtime;
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
        private ManualResetEvent _manualResetEvent;
        private CancellationToken _cancellationToken;
        private CountdownEvent _currentGenerationCounter;

        public DynamicTreeSplitting(
            IEvaluator<TState, TMetric> evaluator,
            IBrancher<TNode, TState, TMetric> brancher,
            IComparator<TMetric> comparator,
            IStateTransitions<TState, TNode, TMetric> stateTransitions
        )
        {
            _evaluator = evaluator;
            _brancher = brancher;
            _comparator = comparator;
            _stateTransitions = stateTransitions;
            _manualResetEvent = new ManualResetEvent(false);
        }

        public TMetric Search(TNode node, int depth, TMetric alfa, TMetric beta, TState state, CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
            cancellationToken.Register(CancelOperation);

            _currentGenerationCounter = new CountdownEvent(1);

            _manualResetEvent = new ManualResetEvent(false);

            var localState = _stateTransitions.Copy(state);
            GoDown(node, localState, depth, node);
            _currentGenerationCounter.Signal();

            _manualResetEvent.WaitOne();

            _cancellationToken.ThrowIfCancellationRequested();

            // waiting for other threadpool tasks that are already scheduled
            _currentGenerationCounter.Wait();

            return node.Result;
        }

        private void GoDown(TNode node, TState state, int depth, TNode splittedFrom)
        {
            if (_cancellationToken.IsCancellationRequested)
            {
                return;
            }

            if (splittedFrom.IsFinalized)
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
                    child.UpdateAlfaBeta(node);
                    var localState = _stateTransitions.GoDown(state, child);
                    GoDown(child, localState, depth - 1, splittedFrom);
                    break;
                }
            }
        }

        private void GoUp(TNode node, int depth, TState state)
        {
            if (_cancellationToken.IsCancellationRequested)
            {
                return;
            }

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
            if (_cancellationToken.IsCancellationRequested)
            {
                return;
            }

            foreach (var child in node.Children)
            {
                if (child.TryLockNode() && _currentGenerationCounter.TryAddCount())
                {
                    // todo - rework closure
                    var stateCopy = _stateTransitions.Copy(state);
                    var localState = _stateTransitions.GoDown(stateCopy, child);
                    ThreadPool.QueueUserWorkItem(
                        obj =>
                        {
                            child.UpdateAlfaBeta(node);
                            GoDown(child, localState, depth - 1, node);
                            _currentGenerationCounter.Signal();
                        }
                    );
                }
            }
        }

        private void CancelOperation()
        {
            _manualResetEvent.Set();
        }

        // todo - implement after mesuring
        public int EstimateRequiredMemoryUsageIncrementInMb(int startDepth, int endDepth)
        {
            return 1;
        }
    }
}
