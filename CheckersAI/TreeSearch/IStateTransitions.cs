using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersAI.TreeSearch
{
    internal interface IStateTransitions<TState, TNode, TMetric>
        where TNode : INode<TNode, TMetric>
        where TState : struct
        where TMetric : struct
    {
        TState GoUp(TState state, TNode node);

        TState GoDown(TState state, TNode node);
    }
}
