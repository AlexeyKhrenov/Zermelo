using System;
using System.Collections.Generic;
using System.Text;

namespace Game.PublicInterfaces
{
    public interface IGameRules
    {
        void CreateInitialPosition(IGame game);

        void MakeMove(IGame game, IHistoryItem latestMove);

        void Undo(IGame game, IHistoryItem undo, IHistoryItem lastMoveBeforeUndo);
    }
}
