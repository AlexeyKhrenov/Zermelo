using Game.Primitives;

namespace Game.PublicInterfaces
{
    public interface IHistoryItem
    {
        IPlayer Player { get; }

        bool IsPieceChangeType { get; set; }

        bool IsKill { get; set; }

        Move Move { get; set; }

        IFigure Captured { get; set; }
    }
}