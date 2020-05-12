namespace CheckersAI.InternalInterfaces
{
    // the implementation may have different output range and therefore different return type
    internal interface IEvaluator<TState, TMetric>
        where TMetric : struct
        where TState : struct
    {
        TMetric Evaluate(TState state);
    }
}