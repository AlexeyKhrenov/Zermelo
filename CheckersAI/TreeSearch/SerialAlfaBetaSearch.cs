﻿using Checkers.Minifications;
using CheckersAI.CheckersGameTree;
using CheckersAI.InternalInterfaces;
using Game.Primitives;
using System;
using System.Collections.Generic;
using System.Threading;

namespace CheckersAI.TreeSearch
{
    internal class SerialAlfaBetaSearch<TNode, TMetric, TState> : ISearch<TNode, TMetric, TState>
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

        public SerialAlfaBetaSearch(
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

        public int EstimateRequiredMemoryUsageIncrementInMb(int startDepth, int endDepth)
        {
            return 1;
        }

        public TMetric Search(TNode node, int depth, TMetric alfa, TMetric beta, TState state, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            if (node.Children == null && depth > 0)
            {
                _brancher.Branch(node, state);
            }

            if (depth == 0 || node.Children.Length == 0)
            {
                var result = _evaluator.Evaluate(state);
                node.Result = result;
                return result;
            }

            if (node.IsMaxPlayer)
            {
                var maxVal = _minValue;
                foreach (var child in node.Children)
                {
                    var childState = _stateTransitions.GoDown(state, child);
                    var result = Search(child, depth - 1, alfa, beta, childState, ct);
                    state = _stateTransitions.GoUp(childState, child);

                    if (!_comparator.IsBigger(maxVal, result))
                    {
                        maxVal = result;
                        node.BestChild = child;
                    }

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

                    var result = Search(child, depth - 1, alfa, beta, childState, ct);
                    state = _stateTransitions.GoUp(childState, child);

                    if (!_comparator.IsBigger(result, minVal))
                    {
                        minVal = result;
                        node.BestChild = child;
                    }

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
