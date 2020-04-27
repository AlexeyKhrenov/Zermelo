using Game.Primitives;
using Game.PublicInterfaces;
using System;

namespace Checkers.Minifications
{
    internal class HistoryItemMinified : IMementoMinification<IHistoryItem>
    {
        // todo - minify this class
        public bool Player { get; set; }
        public bool IsPieceChangeType { get; set; }
        public bool IsKill { get; set; }
        public Cell From { get; set; }
        public Cell To { get; set; }
        public PieceMinified Captured { get; set; }

        public void Minify(IHistoryItem fromMaximizedSource)
        {
            IsKill = fromMaximizedSource.IsKill;
            IsPieceChangeType = fromMaximizedSource.IsPieceChangeType;
            From = fromMaximizedSource.Move.From;
            To = fromMaximizedSource.Move.To;

            if (fromMaximizedSource.Captured != null)
            {
                Captured = new PieceMinified();
                Captured.Minify((Piece)fromMaximizedSource.Captured);
            }
        }

        public void Maximize(IHistoryItem toMaximizedTarget)
        {
            throw new InvalidOperationException();
        }
    }
}
