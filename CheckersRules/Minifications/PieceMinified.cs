using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;

namespace Checkers.Minifications
{
    public class PieceMinified : IMementoMinification<IFigure>, IFigure
    {
        public int X { get; set; }

        public int Y { get; set; }

        public List<Point> AvailableMoves { get; set; }

        public bool CanGoUp { get; set; }

        public bool CanGoDown { get; set; }

        public bool IsWhite { get; set; }

        public bool IsQueen { get; set; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string Type
        {
            get
            {
                if (IsWhite)
                {
                    if (IsQueen)
                    {
                        return PieceTypes.WhiteQueen.ToString();
                    }
                    else
                    {
                        return PieceTypes.White.ToString();
                    }
                }
                else
                {
                    if (IsQueen)
                    {
                        return PieceTypes.BlackQueen.ToString();
                    }
                    else
                    {
                        return PieceTypes.Black.ToString();
                    }
                }
            }
        }

        public PieceMinified()
        {
        }

        public PieceMinified(int x, int y, bool isWhite, bool canGoUp, bool canGoDown)
        {
            X = x;
            Y = y;
            IsWhite = isWhite;
            CanGoDown = canGoDown;
            CanGoUp = canGoUp;
            AvailableMoves = new List<Point>();
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

        public void Minify(IFigure from)
        {
            X = from.X;
            Y = from.Y;

            Enum.TryParse(from.Type, out PieceTypes type);

            switch (type)
            {
                case PieceTypes.Black:
                    IsQueen = false;
                    IsWhite = false;
                    break;
                case PieceTypes.White:
                    IsQueen = false;
                    IsWhite = true;
                    break;
                case PieceTypes.BlackQueen:
                    IsQueen = true;
                    IsWhite = false;
                    break;
                case PieceTypes.WhiteQueen:
                    IsQueen = true;
                    IsWhite = true;
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        public void Maximize(IFigure toMaximizedTarget)
        {
        }
    }
}
