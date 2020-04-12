using System;
using System.Collections.Generic;
using System.Text;

namespace Game.PublicInterfaces
{
    public interface IGameFactory
    {
        IGame CreateGame(int size, bool revertedSides);
    }
}
