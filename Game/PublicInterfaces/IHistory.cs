using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Game.PublicInterfaces
{
    // todo - consider deleting this interface
    public interface IHistory
    {
        void Push(IHistoryItem historyItem);

        IHistoryItem Pop();
    }
}
