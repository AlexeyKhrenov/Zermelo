namespace CheckersAI.InternalInterfaces
{
    internal interface IComparator<T> where T : struct
    {
        bool IsBigger(T a, T b);
    }
}