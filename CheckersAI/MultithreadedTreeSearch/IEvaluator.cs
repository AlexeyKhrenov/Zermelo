﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CheckersAI.MultithreadedTreeSearch
{
    internal interface IEvaluator<TNode, TValue, TMetric>
        where TNode : AlfaBetaNodeBase<TNode, TValue, TMetric>
        where TValue : struct
        where TMetric : struct
    {
        TMetric Evaluate(TNode node);
    }
}