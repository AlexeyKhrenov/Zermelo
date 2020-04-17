﻿using System;
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

        List<Point> AvailableMoves { get; set; }

        string Type { get; }
    }
}