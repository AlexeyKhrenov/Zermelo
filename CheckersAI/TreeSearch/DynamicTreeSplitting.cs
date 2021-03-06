﻿using System;
using System.Threading;
using CheckersAI.InternalInterfaces;

namespace CheckersAI.TreeSearch
{
    internal class DynamicTreeSplitting<TNode, TMetric, TState> : ISearch<TNode, TMetric, TState>
        where TNode : class, IAlfaBetaNode<TNode, TMetric>
        where TMetric : struct
        where TState : struct
    {
        private readonly IBrancher<TNode, TState, TMetric> _brancher;
        private CancellationToken _cancellationToken;
        private IComparator<TMetric> _comparator;
        private CountdownEvent _currentGenerationCounter;
        private readonly IEvaluator<TState, TMetric> _evaluator;
        private ManualResetEvent _manualResetEvent;
        private readonly IStateTransitions<TState, TNode, TMetric> _stateTransitions;

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

        public TMetric Search(TNode node, int depth, TMetric alfa, TMetric beta, TState state,
            CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
            cancellationToken.Register(CancelOperation);

            _currentGenerationCounter = new CountdownEvent(1);

            _manualResetEvent = new ManualResetEvent(false);

            var localState = _stateTransitions.Copy(state);
            GoDown(node, localState, depth, node);
            _currentGenerationCounter.Signal();

            _cancellationToken.ThrowIfCancellationRequested();
            _manualResetEvent.WaitOne();

            _cancellationToken.ThrowIfCancellationRequested();

            // waiting for other threadpool tasks that are already scheduled
            _currentGenerationCounter.Wait();

            return node.Result;
        }

        // todo - implement after mesuring
        public int EstimateRequiredMemoryUsageIncrementInMb(int startDepth, int endDepth)
        {
            return 1;
        }

        private void GoDown(TNode node, TState state, int depth, TNode splittedFrom)
        {
            if (_cancellationToken.IsCancellationRequested) return;

            if (splittedFrom.IsFinalized) return;

            if (depth == 0)
            {
                if (node.IsEvaluated)
                    node.Update(node.TerminationResult);
                else
                    node.Update(_evaluator.Evaluate(state));

                GoUp(node, depth, state);
                return;
            }

            if (node.Children == null) _brancher.Branch(node, state);

            if (node.Children.Length == 0)
            {
                node.Update(_evaluator.Evaluate(state));
                GoUp(node, depth, state);
                return;
            }

            foreach (var child in node.Children)
                if (child.TryLockNode())
                {
                    child.UpdateAlfaBeta(node);
                    state = _stateTransitions.GoDown(state, child);
                    GoDown(child, state, depth - 1, splittedFrom);
                    break;
                }
        }

        private void GoUp(TNode node, int depth, TState state)
        {
            if (_cancellationToken.IsCancellationRequested) return;

            var parent = node.Parent;
            if (parent != null)
            {
                parent.Update(node);

                state = _stateTransitions.GoUp(state, node);

                if (parent.IsFinalized)
                    GoUp(parent, depth + 1, state);
                else
                    SplitNode(parent, depth + 1, state);
            }
            else
            {
                if (node.IsFinalized) _manualResetEvent.Set();
            }
        }

        private void SplitNode(TNode node, int depth, TState state)
        {
            if (_cancellationToken.IsCancellationRequested) return;

            TNode continuationNode = null;

            foreach (var child in node.Children)
                if (child.TryLockNode() && _currentGenerationCounter.TryAddCount())
                {
                    if (continuationNode == null)
                    {
                        continuationNode = child;
                    }
                    else
                    {
                        // todo - rework closure
                        var stateCopy = _stateTransitions.Copy(state);
                        var localState = _stateTransitions.GoDown(stateCopy, child);
                        ThreadPool.QueueUserWorkItem(
                            obj =>
                            {
                                child.UpdateAlfaBeta(node);
                                GoDown(child, localState, depth - 1, node);
                                _stateTransitions.DeallocateCopy(stateCopy);
                                try
                                {
                                    _currentGenerationCounter.Signal();
                                }
                                catch (InvalidOperationException) { }
                            }
                        );
                    }
                }

            if (continuationNode != null)
            {
                continuationNode.UpdateAlfaBeta(node);
                var localState = _stateTransitions.GoDown(state, continuationNode);
                GoDown(continuationNode, localState, depth - 1, continuationNode);
                _currentGenerationCounter.Signal();
            }
        }

        private void CancelOperation()
        {
            _manualResetEvent.Set();
        }
    }
}