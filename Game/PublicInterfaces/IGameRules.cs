using System;
using System.Collections.Generic;
using System.Text;

namespace Game.PublicInterfaces
{
    public interface IGameRules
    {
        List<IFigure> CreateInitialPosition(int size, bool changedSides);
    }
}
