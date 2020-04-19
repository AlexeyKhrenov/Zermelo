using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkers.Minifications
{
    internal class PlayerMinified : IMementoMinification<IPlayer>
    {
        // change to fixed array
        public PieceMinified[] Pieces { get; set; }

        public void Minify(IPlayer from)
        {
            var figures = new List<PieceMinified>();

            foreach (var figure in from.Figures)
            {
                var min = new PieceMinified();
                min.Minify(figure);
                figures.Add(min);
            }

            Pieces = figures.ToArray();
        }

        public void Maximize(IPlayer to)
        {
            var figures = new List<IFigure>();

            foreach (var figure in Pieces)
            {
                if (figure != null)
                {
                    figures.Add(figure);
                }
            }

            to.Figures = figures;
        }

        public void RestorePiece(PieceMinified piece)
        {
            for (var i = 0; i < Pieces.Length; i++)
            {
                if (Pieces[i] == null)
                {
                    Pieces[i] = piece;
                    return;
                }
            }

            throw new InvalidOperationException();
        }

        public PieceMinified RemovePiece(int x, int y)
        {
            for (var i = 0; i < Pieces.Length; i++)
            {
                if(Pieces[i].X == x&& Pieces[i].Y == y){
                    var piece = Pieces[i];
                    Pieces[i] = null;
                    return piece;
                }
            }

            throw new InvalidOperationException();
        }
    }
}
