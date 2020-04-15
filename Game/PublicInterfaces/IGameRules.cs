using System;
using System.Collections.Generic;
using System.Text;

namespace Game.PublicInterfaces
{
    public interface IGameRules
    {
        void CreateInitialPosition(IGame game);

        void MakeMove(IGame game, int x0, int y0, int x1, int y1);

        void Undo();
    }
}
