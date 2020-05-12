using Game.Primitives;

namespace Game.PublicInterfaces
{
    public interface IGame
    {
        int Size { get; }

        int HistoryLength { get; }

        IBoard Board { get; }

        IHistoryItem LatestMove { get; }

        bool CanUndo { get; }

        IPlayer Winner { get; }

        void Move(Move move);

        void Undo(IPlayer player);
    }
}