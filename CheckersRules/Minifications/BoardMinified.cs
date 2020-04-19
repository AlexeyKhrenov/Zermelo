using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Minifications
{
    // todo - this should become a struct with methods
    internal class BoardMinified : IMementoMinification<IBoard>
    {
        public PieceMinified[,] Pieces { get; set; }

        public PlayerMinified Player1 { get; set; }

        public PlayerMinified Player2 { get; set; }

        public bool InvertedCoordinates { get; set; }

        public bool ActivePlayer { get; set; }

        public void SwitchPlayers()
        {
            ActivePlayer = !ActivePlayer;
        }

        // todo - create property
        public PlayerMinified GetActivePlayer()
        {
            return ActivePlayer ? Player1 : Player2;
        }

        // todo - create property
        public int GetSize()
        {
            return Pieces.GetLength(0);
        }

        public PieceMinified RemovePiece(int x, int y, bool player)
        {
            Pieces[x, y] = null;

            if (player)
            {
                return Player1.RemovePiece(x, y);
            }
            else
            {
                return Player2.RemovePiece(x, y);
            }
        }

        public void MovePiece(int x0, int y0, int x1, int y1)
        {
            Pieces[x1, y1] = Pieces[x0, y0];
            Pieces[x0, y0] = null;
        }

        internal void RestorePiece(PieceMinified captured, bool player)
        {
            if (player)
            {
                Player1.RestorePiece(captured);
            }
            else
            {
                Player2.RestorePiece(captured);
            }

            Pieces[captured.X, captured.Y] = captured;
        }

        public void Minify(IBoard from)
        {
            ActivePlayer = from.ActivePlayer == from.Player1;
            Player1 = new PlayerMinified();
            Player1.Minify(from.Player1);
            Player2 = new PlayerMinified();
            Player2.Minify(from.Player2);

            foreach (var figure in from.Player1.Figures)
            {
                var minPiece = new PieceMinified();
                minPiece.Minify(figure);
                Pieces[figure.X, figure.Y] = minPiece;
            }

            foreach (var figure in from.Player2.Figures)
            {
                var minPiece = new PieceMinified();
                minPiece.Minify(figure);
                Pieces[figure.X, figure.Y] = minPiece;
            }
        }

        public void Maximize(IBoard to)
        {
            to.ActivePlayer = ActivePlayer ? to.Player1 : to.Player2;
            Player1.Maximize(to.Player1);
            Player2.Maximize(to.Player2);
        }
    }
}
