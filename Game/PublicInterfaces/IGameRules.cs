using System;
using System.Collections.Generic;
using System.Text;

namespace Game.PublicInterfaces
{
    public interface IGameRules
    {
        List<IFigure> CreateInitialPosition(int size, bool changedSides);

        void MakeMove(IGame game, int x0, int y0, int x1, int y1);

        void Undo();
    }
}
