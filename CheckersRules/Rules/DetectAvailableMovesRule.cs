using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Checkers.Rules
{
    internal class DetectAvailableMovesRule : AbstractRule
    {
        public override string Name => nameof(DetectAvailableMovesRule);

        public override void ApplyRule(IGame game, Piece[,] pieces)
        {
            foreach (var figure in game.Figures)
            {
                // todo - remove this cast
                var piece = pieces[figure.X, figure.Y];
                piece.AvailableMoves.Clear();
                var size = game.Size;

                if (piece.CanGoUp && piece.Y > 0)
                {
                    //left
                    if (piece.X > 0)
                    {
                        Check(piece, pieces, -1, -1);
                    }
                    //right
                    if (piece.X < size - 1)
                    {
                        Check(piece, pieces, -1, 1);
                    }
                }

                if (piece.CanGoDown && piece.Y < size - 1)
                {
                    //left
                    if (piece.X > 0)
                    {
                        Check(piece, pieces, 1, -1);
                    }
                    //right
                    if (piece.X < size - 1)
                    {
                        Check(piece, pieces, 1, 1);
                    }
                }
            }
        }

        public void Check(Piece piece, Piece[,] pieces, int directionDown, int directionRight)
        {
            if (pieces[piece.X + directionRight, piece.Y + directionDown] == null)
            {
                piece.AvailableMoves.Add(new Point(piece.X + directionRight, piece.Y + directionDown));
            }
        }
    }
}
