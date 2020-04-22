using Checkers.Minifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersAI.Evaluators
{
    // the implementation may have different output range and therefore different return type
    internal interface IEvaluator<T> where T : struct
    {
        T Evaluate(object board);
    }
}
