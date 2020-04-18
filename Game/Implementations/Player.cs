using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementations
{
    public class Player : IPlayer
    {
        public IList<IFigure> Figures { get; set; }
    }
}
