using System;
using System.Collections.Generic;
using System.Text;

namespace Game.PublicInterfaces
{
    public interface IGameRules
    {
        void PlaceFigures(IBoard board);

        void MakeMove(IBoard board, IHistoryItem latestMove);

        void Undo(IBoard board, IHistoryItem undo, IHistoryItem lastMoveBeforeUndo);
    }
}
