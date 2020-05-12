namespace Game.PublicInterfaces
{
    public interface IGameRules
    {
        void PlaceFigures(IBoard board);

        /// <summary>
        ///     return winner or null
        /// </summary>
        IPlayer MakeMove(IBoard board, IHistoryItem latestMove);

        void Undo(IBoard board, IHistoryItem undo, IHistoryItem lastMoveBeforeUndo);
    }
}