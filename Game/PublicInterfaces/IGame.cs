using System;
using System.Collections.Generic;
using System.Text;

namespace Game.PublicInterfaces
{
    public interface IGame
    {
        int Size { get; set; }

        IList<IFigure> Figures { get; }

        void Move(int x0, int y0, int x1, int y1);

        void Undo(); 
    }
}
