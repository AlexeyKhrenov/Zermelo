using Game.Primitives;
using Game.PublicInterfaces;

namespace Checkers.Minifications
{
    internal class HistoryItemMinified
    {
        public HistoryItemMinified()
        {
        }

        public HistoryItemMinified(Cell from, Cell to, bool activePlayer)
        {
            From = from;
            To = to;
            Player = activePlayer;
        }

        // todo - minify this class
        public bool Player { get; set; }
        public bool IsPieceChangeType { get; set; }
        public bool IsKill => From.X - To.X > 1 || To.X - From.X > 1;
        public Cell From { get; set; }
        public Cell To { get; set; }
        public PieceMinified Captured { get; set; }

        public void Minify(IHistoryItem fromMaximizedSource, IBoard board)
        {
            IsPieceChangeType = fromMaximizedSource.IsPieceChangeType;
            From = fromMaximizedSource.Move.From;
            To = fromMaximizedSource.Move.To;

            if (fromMaximizedSource.Captured != null) Captured = ((Piece) fromMaximizedSource.Captured).ToMinified();

            Player = board.Player1 == fromMaximizedSource.Player;
        }

        public void Maximize(IHistoryItem toMaximizedTarget)
        {
            if (!Captured.IsEmpty()) toMaximizedTarget.Captured = Captured.ToMaximized();
        }

        public override string ToString()
        {
            return $"{From.X},{From.Y}->{To.X},{To.Y}";
        }
    }
}