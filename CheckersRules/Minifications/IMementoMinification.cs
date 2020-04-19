using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Minifications
{
    interface IMementoMinification<T> where T : class
    {
        void Minify(T fromMaximizedSource);

        void Maximize(T toMaximizedTarget);
    }
}
