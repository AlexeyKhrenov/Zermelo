using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Checkers.Minifications
{
    internal class HistoryItemMinified : IMementoMinification<IHistoryItem>
    {
        // todo - minify this class
        public bool Player { get; set; }
        public bool IsPieceChangeType { get; set; }
        public bool IsKill { get; set; }
        public Point From { get; set; }
        public Point To { get; set; }
        public PieceMinified Captured { get; set; }

        public void Minify(IHistoryItem fromMaximizedSource)
        {
            IsKill = fromMaximizedSource.IsKill;
            IsPieceChangeType = fromMaximizedSource.IsPieceChangeType;
            From = fromMaximizedSource.From;
            To = fromMaximizedSource.To;

            if (fromMaximizedSource.Captured != null)
            {
                Captured = new PieceMinified();
                Captured.Minify(fromMaximizedSource.Captured);
            }
        }

        public void Maximize(IHistoryItem toMaximizedTarget)
        {
            throw new InvalidOperationException();
        }
    }
}
