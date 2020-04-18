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

        bool IsKill { get; set; }

        Point From { get; }

        Point To { get; }

        IFigure Captured { get; set; }
    }
}
