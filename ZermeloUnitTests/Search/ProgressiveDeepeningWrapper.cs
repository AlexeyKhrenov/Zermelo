using CheckersAI.InternalInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZermeloUnitTests.Search
{
    internal class ProgressiveDeepeningWrapper<TNode, TMetric, TState>
        where TNode : INode<TNode, TMetric>
        where TMetric : struct
        where TState : struct
    {
        public Queue<TNode> Run(ISearch<TNode, TMetric, TState> search, TNode node)
        {
            throw new NotImplementedException();
        }
    }
}
