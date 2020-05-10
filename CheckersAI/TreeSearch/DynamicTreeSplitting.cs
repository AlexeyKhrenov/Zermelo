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
        private ManualResetEvent _manualResetEvent;

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

        private CancellationTokenSource currentGenerationTokenSource;
        private CountdownEvent currentGenerationCounter;

        public TMetric Search(TNode node, int depth, TMetric alfa, TMetric beta, TState state, CancellationToken cancellationToken)
        {
            currentGenerationTokenSource = new CancellationTokenSource();
            currentGenerationCounter = new CountdownEvent(1);

            _manualResetEvent = new ManualResetEvent(false);

            var localState = _stateTransitions.Copy(state);
            GoDown(node, localState, depth, node);
            currentGenerationCounter.Signal();

            _manualResetEvent.WaitOne();

            currentGenerationTokenSource.Cancel();

            // waiting for other threadpool tasks that are already scheduled
            currentGenerationCounter.Wait();

            return node.Result;
        }

        private void GoDown(TNode node, TState state, int depth, TNode splittedFrom)
        {
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
            node.WasSplitted = true;

            if (currentGenerationTokenSource.IsCancellationRequested)
            {
                return;
            }

            foreach (var child in node.Children)
            {
                if (child.TryLockNode() && currentGenerationCounter.TryAddCount())
                {
                    var stateCopy = _stateTransitions.Copy(state);
                    var localState = _stateTransitions.GoDown(stateCopy, child);
                    ThreadPool.QueueUserWorkItem(
                        obj =>
                        {
                            GoDown(child, localState, depth - 1, node);
                            currentGenerationCounter.Signal();
                        }
                    );
                }
            }
        }
    }
}
