﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Game.PublicInterfaces
{
    public interface IGame
    {
        int Size { get; set; }

        IList<IFigure> Figures { get; }
    }
}
