using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersAI.AsyncTreeSearch
{
    internal interface IComparator<T> : TreeSearch.IComparator<T> where T : struct
    {
    }
}
