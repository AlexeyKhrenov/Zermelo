using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkers.Minifications
{
    // todo - this should become a struct with methods
    internal struct BoardMinified
    {
        // todo rename to board cell
        public BoardCell[,] Pieces { get; set; }

        public bool InvertedCoordinates { get; set; }

        // todo change to array
        public List<PieceMinified> Player1Pieces { get; set; }

        // todo change to array
        public List<PieceMinified> Player2Pieces { get; set; }

        public bool ActivePlayer { get; set; }

        public List<PieceMinified> ActiveSet => ActivePlayer ? Player1Pieces : Player2Pieces;

        // little optimisation for GameNode evaluation
        public byte Player1PiecesCount { get; set; }

        // little optimisation for GameNode evaluation
        public byte Player2PiecesCount { get; set; }

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
            var index = Pieces[x, y].GetIndex();
            Pieces[x, y].RemovePiece();

            if (player)
            {
                Player1PiecesCount--;
                var toRemove = Player1Pieces[index];
                toRemove.IsCaptured = true;
                return toRemove;
            }
            else
            {
                Player2PiecesCount--;
                var toRemove = Player2Pieces[index];
                toRemove.IsCaptured = true;
                return toRemove;
            }
        }

        public void MovePiece(byte x0, byte y0, byte x1, byte y1)
        {
            Pieces[x1, y1] = Pieces[x0, y0];
            Pieces[x0, y0].RemovePiece();

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
                Player1PiecesCount++;
                RestorePiece(Player1Pieces, captured);
            }
            else
            {
                Player2PiecesCount++;
                RestorePiece(Player2Pieces, captured);
            }
        }

        private void RestorePiece(List<PieceMinified> playersPieces, PieceMinified captured)
        {
            for (byte i = 0; i < playersPieces.Count; i++)
            {
                if (playersPieces[i].Equals(captured))
                {
                    Pieces[captured.X, captured.Y] = new BoardCell(i, captured.IsWhite);
                    playersPieces[i].IsCaptured = false;
                }
            }
        }

        public PieceMinified GetPiece(byte x, byte y, bool player)
        {
            var cell = Pieces[x, y];
            if (player)
            {
                return Player1Pieces[cell.GetIndex()];
            }
            else
            {
                return Player2Pieces[cell.GetIndex()];
            }
        }

        // todo - remove after it becomes a structure
        public void ClearMoves()
        {
            foreach (var figure in Player1Pieces)
            {
                figure.ClearMoves();
            }

            foreach (var figure in Player2Pieces)
            {
                figure.ClearMoves();
            }
        }
    }
}
