using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Minifications
{
    interface IMementoMinification<T>
    {
        T Restore();

        void Minify(T maximizedSource);
    }
}
