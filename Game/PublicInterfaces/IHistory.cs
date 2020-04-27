namespace Game.PublicInterfaces
{
    // todo - consider deleting this interface
    public interface IHistory
    {
        void Push(IHistoryItem historyItem);

        int Length { get; }

        IHistoryItem Pop();

        IHistoryItem Latest { get; }
    }
}
