namespace Game.PublicInterfaces
{
    // todo - consider deleting this interface
    public interface IHistory
    {
        int Length { get; }

        IHistoryItem Latest { get; }
        void Push(IHistoryItem historyItem);

        IHistoryItem Pop();
    }
}