using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersAI.TreeSearch
{
    interface IComparator<T> where T : struct
    {
        bool IsBigger(T a, T b);
    }
}
