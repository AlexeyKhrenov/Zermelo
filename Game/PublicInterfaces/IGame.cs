using System;
using System.Collections.Generic;
using System.Text;

namespace Game.PublicInterfaces
{
    public interface IGame
    {
        IEnumerable<IFigure> GetFigures();

        int Size { get; }

        void Move(int x0, int y0, int x1, int y1);

        void Undo();

        int HistoryLength { get; }
    }
}
