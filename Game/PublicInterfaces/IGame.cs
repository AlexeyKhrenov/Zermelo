using Game.Primitives;

namespace Game.PublicInterfaces
{
    public interface IGame
    {
        int Size { get; }

        void Move(Move move);

        void Undo();

        int HistoryLength { get; }

        IBoard Board { get; }
    }
}
