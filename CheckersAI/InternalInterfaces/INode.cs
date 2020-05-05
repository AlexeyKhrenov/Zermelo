using System;

namespace CheckersAI.InternalInterfaces
{
    // such generic arguments to avoid unboxing
    internal interface INode<TNode, TMetric> : IEquatable<TNode>
        where TNode : INode<TNode, TMetric>
        where TMetric : struct
    {
        TNode[] Children { get; set; }

        bool IsMaxPlayer { get; }

        TMetric Result { get; set; }

        TNode BestChild { get; set; }

        void Clear();
    }
}
