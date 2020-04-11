using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Game.PublicInterfaces
{
    // todo - consider delering this interface
    public interface IFigure
    {
        int X { get; set; }

        int Y { get; set; }

        int FigureType { get; set; }

        Point[] AvailableMoves { get; set; }

        int Color { get; set; }
    }
}
