using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Game.PublicInterfaces
{
    public interface IHistory
    {
        void Push(IPlayer player, Point from, Point to);

        IHistoryItem Pop();
    }
}
