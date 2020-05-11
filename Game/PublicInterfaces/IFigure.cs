using Game.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.PublicInterfaces
{
    // todo - consider delering this interface
    public interface IFigure : IEquatable<IFigure>
    {
        byte X { get; set; }

        byte Y { get; set; }

        List<Cell> AvailableMoves { get; set; }

        string Type { get; }

        bool IsCaptured { get; }
    }
}
