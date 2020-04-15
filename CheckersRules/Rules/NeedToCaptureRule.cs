using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkers.Rules
{
    internal class NeedToCaptureRule : AbstractRule
    {
        public override string Name => nameof(NeedToCaptureRule);

        public override void ApplyRule(IGame game, Piece[,] pieces)
        {
            var needToPassControl = true;

            foreach (var figure in game.Figures)
            {
                figure.AvailableMoves.Clear();
                needToPassControl &= !Check(figure, pieces);
            }

            if (needToPassControl)
            {
                PassControlToNext(game, pieces);
            }
        }

        // returns true if a piece to capture was detected
        private bool Check(IFigure figure, Piece[,] pieces)
        {
            var result = false;

            var size = pieces.GetLength(0);
            var piece = pieces[figure.X, figure.Y];
            if (piece.Y > 1)
            {
                // left
                if (piece.X > 1)
                {
                    result |= CheckDirection(piece, pieces, -1, -1);
                }
                // right
                if (piece.X < size - 2)
                {
                    result |= CheckDirection(piece, pieces, 1, -1);
                }
            }
            if (piece.Y < size - 2)
            {
                // left
                if (piece.X > 1)
                {
                    result |= CheckDirection(piece, pieces, -1, 1);
                }

                // right
                if (piece.X < size - 2)
                {
                    result |= CheckDirection(piece, pieces, 1, 1);
                }
            }

            return result;
        }

        private bool CheckDirection(Piece piece, Piece[,] pieces, int directionRight, int directionDown)
        {
            var target = pieces[piece.X + directionRight, piece.Y + directionDown];
            if (target != null && target.IsWhite != piece.IsWhite)
            {
                if (pieces[piece.X + 2 * directionRight, piece.Y + 2 * directionDown] == null)
                {
                    piece.AvailableMoves.Add(new System.Drawing.Point(piece.X + 2 * directionRight, piece.Y + 2 * directionDown));
                    return true;
                }
            }
            return false;
        }
    }
}
