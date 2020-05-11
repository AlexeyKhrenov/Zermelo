using System.Collections.Generic;
using System.Linq;
using Game.Primitives;
using Game.PublicInterfaces;

namespace ZermeloCheckers.ViewModels
{
    public class FigureViewModel : IFigure
    {
        public byte X { get; set; }

        public byte Y { get; set; }

        public string Type { get; set; }

        public delegate void FigureMovedHandler(object sender, byte x0, byte y0, byte x1, byte y1);
        public event FigureMovedHandler FigureMoved;

        public List<Cell> AvailableMoves { get; set; }

        public bool IsCaptured { get; set; }

        public bool HasCoordinates(int x, int y)
        {
            return X == x && Y == y;
        }
         
        public bool TryMoveFigure(byte x1, byte y1)
        {
            if (IsMoveAllowed(x1, y1))
            {
                var x0 = X;
                var y0 = Y;

                FigureMoved(null, x0, y0, x1, y1);
                return true;
            }

            return false;
        }

        public bool IsMoveAllowed(int x, int y)
        {
            return AvailableMoves != null && AvailableMoves.Any(p => p.X == x && p.Y == y);
        }

        // todo - consider changing to smaller value types
        public override int GetHashCode()
        {
            return (X << 8) + Y;
        }

        public bool Equals(IFigure other)
        {
            if (other == null)
            {
                return false;
            }

            return X == other.X && Y == other.Y && Type == other.Type;
        }
    }
}
