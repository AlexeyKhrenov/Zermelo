using CheckersAI.Evaluators;
using CheckersAI.Tree;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersAI.TreeSearch
{
    // the implementation may have different output range and therefore different return type
    internal class AlfaBetaSearch<TValue>
    {
        public sbyte Search(Node<TValue> root, int depth, IEvaluator<sbyte> evaluator)
        {
            return SearchAlfaBeta(root, depth, sbyte.MinValue, sbyte.MaxValue, evaluator);
        }

        private sbyte SearchAlfaBeta(Node<TValue> node, int depth, sbyte alfa, sbyte beta, IEvaluator<sbyte> evaluator)
        {
            // for debug purposes - remove or surround with preprocessor directives
            node.IsVisited = true;

            if (depth == 0 || node.Children == null || node.Children.Length == 0)
            {
                return evaluator.Evaluate(node);
            }

            if (node.IsMaxPlayer)
            {
                var maxVal = sbyte.MinValue;
                foreach (var child in node.Children)
                {
                    var result = SearchAlfaBeta(child, depth - 1, alfa, beta, evaluator);
                    maxVal = result > maxVal ? result : maxVal;
                    if (result >= beta)
                    {
                        break;
                    }

                    alfa = result > alfa ? result : alfa;
                }
                return maxVal;
            }
            else
            {
                var minVal = sbyte.MaxValue;
                foreach (var child in node.Children)
                {
                    var result = SearchAlfaBeta(child, depth - 1, alfa, beta, evaluator);
                    minVal = result < minVal ? result : minVal;
                    if (result <= alfa)
                    {
                        break;
                    }

                    beta = result < beta ? result : beta;
                }
                return minVal;
            }
        }
    }
}
