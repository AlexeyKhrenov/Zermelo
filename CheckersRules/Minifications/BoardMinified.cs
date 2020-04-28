using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkers.Minifications
{
    // todo - this should become a struct with methods
    internal class BoardMinified
    {
        public PieceMinified[,] Pieces { get; set; }

        public bool InvertedCoordinates { get; set; }

        // change to array
        public List<PieceMinified> Player1Pieces { get; set; }

        // change to array
        public List<PieceMinified> Player2Pieces { get; set; }

        public bool ActivePlayer { get; set; }

        public List<PieceMinified> ActiveSet => ActivePlayer ? Player1Pieces : Player2Pieces;

        public void SwitchPlayers()
        {
            ActivePlayer = !ActivePlayer;
        }

        // todo - create property
        public byte GetSize()
        {
            return (byte)Pieces.GetLength(0);
        }

        public PieceMinified RemovePiece(int x, int y, bool player)
        {
            Pieces[x, y] = null;

            if (player)
            {
                // todo - remove Linq
                var toRemove = Player1Pieces.First(f => f.X == x && f.Y == y);
                Player1Pieces.Remove(toRemove);
                return toRemove;
            }
            else
            {
                // todo - remove Linq
                var toRemove = Player2Pieces.First(f => f.X == x && f.Y == y);
                Player2Pieces.Remove(toRemove);
                return toRemove;
            }
        }

        public void MovePiece(byte x0, byte y0, byte x1, byte y1)
        {
            Pieces[x1, y1] = Pieces[x0, y0];
            Pieces[x0, y0] = null;

            // todo - optimize it along with Linq removal and collections
            var piece = Player1Pieces.FirstOrDefault(f => f.X == x0 && f.Y == y0);


            if (piece == null)
            {
                piece = Player2Pieces.First(f => f.X == x0 && f.Y == y0);
            }

            piece.X = x1;
            piece.Y = y1;
        }

        internal void RestorePiece(PieceMinified captured, bool player)
        {
            if (player)
            {
                Player1Pieces.Add(captured);
            }
            else
            {
                Player2Pieces.Add(captured);
            }

            Pieces[captured.X, captured.Y] = captured;
        }

        public void Minify(IBoard from)
        {
            Player1Pieces = new List<PieceMinified>();
            Player2Pieces = new List<PieceMinified>();

            Pieces = new PieceMinified[from.Size, from.Size];
            InvertedCoordinates = from.InvertedCoordinates;

            ActivePlayer = from.ActivePlayer == from.Player1;

            foreach (var figure in from.Player1.Figures)
            {
                var minPiece = new PieceMinified();
                minPiece.Minify((Piece)figure);
                Player1Pieces.Add(minPiece);
                Pieces[figure.X, figure.Y] = minPiece;
            }

            foreach (var figure in from.Player2.Figures)
            {
                var minPiece = new PieceMinified();
                minPiece.Minify((Piece)figure);
                Player2Pieces.Add(minPiece);
                Pieces[figure.X, figure.Y] = minPiece;
            }
        }

        public void Maximize(IBoard to)
        {
            to.Player1.Figures = Player1Pieces.Select(x => x.ToPiece()).ToList();
            to.Player2.Figures = Player2Pieces.Select(x => x.ToPiece()).ToList();

            if (to.ActivePlayer != (ActivePlayer ? to.Player1 : to.Player2))
            {
                to.SwitchPlayers();
            }
        }
    }
}
