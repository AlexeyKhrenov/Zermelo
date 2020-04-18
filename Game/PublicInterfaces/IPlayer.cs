using System;
using System.Collections.Generic;
using System.Text;

namespace Game.PublicInterfaces
{
    public interface IPlayer
    {
        IList<IFigure> Figures { get; set; }
    }
}
