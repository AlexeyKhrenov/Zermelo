using System;
using System.Collections.Generic;
using System.Text;

namespace Game.PublicInterfaces
{
    public interface IGame
    {
        IPlayer ActivePlayer { get; set; }

        IPlayer AwaitingPlayer { get; }

        IPlayer Player1 { get; }

        IPlayer Player2 { get; }

        int Size { get; set; }

        void Move(int x0, int y0, int x1, int y1);

        void Undo();

        // think of renaming this method
        void SwitchPlayersTurn();

        bool IsRevertedBoard { get; }

        int HistoryLength { get; }
    }
}
