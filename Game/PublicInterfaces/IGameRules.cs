using System;
using System.Collections.Generic;
using System.Text;

namespace Game.PublicInterfaces
{
    public interface IGameRules
    {
        IBoard CreateBoard(IPlayer player1, IPlayer player2, bool invertedCoordinates);

        void MakeMove(IBoard game, IHistoryItem latestMove);

        void Undo(IBoard board, IHistoryItem undo, IHistoryItem lastMoveBeforeUndo);
    }
}
