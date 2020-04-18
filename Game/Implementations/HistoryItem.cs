using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Game.Implementations
{
    public class HistoryItem : IHistoryItem
    {
        public IPlayer Player { get; private set; }

        public Point From { get; private set; }

        public Point To { get; private set; }

        public bool IsPieceChangeType { get; set; }

        public bool IsKill { get; set; }

        public IFigure Captured { get; set; }

        public HistoryItem(IPlayer player, Point from, Point to)
        {
            IsPieceChangeType = false;
            IsKill = false;
            Captured = null;

            Player = player;
            From = from;
            To = to;
        }

        public override string ToString()
        {
            return $"{From.X}, {From.Y} -> {To.X}, {To.Y}";
        }
    }
}
