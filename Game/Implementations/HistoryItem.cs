using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Game.Implementations
{
    public struct HistoryItem : IHistoryItem
    {
        public IPlayer Player { get; private set; }

        public Point From { get; private set; }

        public Point To { get; private set; }

        public bool IsPieceChangeType { get; set; }

        public bool IsKill => From.X - To.X > 1 || From.X - To.X < -1;

        public HistoryItem(IPlayer player, Point from, Point to)
        {
            IsPieceChangeType = false;

            Player = player;
            From = from;
            To = to;
        }
    }
}
