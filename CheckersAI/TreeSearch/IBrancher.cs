using CheckersAI.Tree;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersAI.TreeSearch
{
    internal interface IBrancher<TNode, TValue> where TNode : INode<TNode, TValue> where TValue : struct
    {
        void Branch(TNode node);
    }
}
