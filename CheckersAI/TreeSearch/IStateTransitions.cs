using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersAI.TreeSearch
{
    internal interface IStateTransitions<TState, TNode>
        where TNode : INode<TNode>
        where TState : struct
    {
        TState GoUp(TState state, TNode node);

        TState GoDown(TState state, TNode node);
    }
}
