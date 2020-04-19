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
        public int Player { get; set; }
        public bool IsPieceChangeType { get; set; }
        public bool IsKill { get; set; }
        public Point From { get; }
        public Point To { get; }
        public Piece Captured { get; set; }

        public void Minify(IHistoryItem maximizedSource)
        {
            throw new NotImplementedException();
        }

        public IHistoryItem Restore()
        {
            throw new NotImplementedException();
        }
    }
}
