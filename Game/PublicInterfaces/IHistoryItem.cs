using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Game.PublicInterfaces
{
    public interface IHistoryItem
    {
        IPlayer Player { get; }

        bool IsPieceChangeType { get; set; }

        bool IsKill { get; }

        Point From { get; }

        Point To { get; }
    }
}
