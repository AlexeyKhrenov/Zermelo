using Game.Primitives;
using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Checkers.Minifications
{
    // todo - this should become a struct with methods
    internal struct BoardMinified
    {
        // todo rename to board cell
        public BoardCell[,] Pieces { get; set; }

        public bool InvertedCoordinates { get; set; }

        // todo - change it to fixed
        public PieceMinified[] Player1Pieces;

        public PieceMinified[] Player2Pieces;

        public bool ActivePlayer { get; set; }

        public PieceMinified[] ActiveSet => ActivePlayer ? Player1Pieces : Player2Pieces;

        // little optimisation for GameNode evaluation
        public byte Player1PiecesCount { get; set; }

        // little optimisation for GameNode evaluation
        public byte Player2PiecesCount { get; set; }

        public BoardMinified(int size)
        {
            Pieces = new BoardCell[size, size];
            Player1Pieces = new PieceMinified[20];
            Player2Pieces = new PieceMinified[20];
            ActivePlayer = true;
            InvertedCoordinates = false;
            Player1PiecesCount = 0;
            Player2PiecesCount = 0;
        }

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
                toRemove.ClearMoves();
                Player1Pieces[index] = toRemove;
                return toRemove;
            }
            else
            {
                Player2PiecesCount--;
                var toRemove = Player2Pieces[index];
                toRemove.IsCaptured = true;
                toRemove.ClearMoves();
                Player2Pieces[index] = toRemove;
                return toRemove;
            }
        }

        public void MovePiece(byte x0, byte y0, byte x1, byte y1, bool player)
        {
            var i = Pieces[x0, y0].GetIndex();

            Pieces[x1, y1] = Pieces[x0, y0];
            Pieces[x0, y0].RemovePiece();

            if (player)
            {
                Player1Pieces[i].X = x1;
                Player1Pieces[i].Y = y1;
                return;
            }
            else
            {
                Player2Pieces[i].X = x1;
                Player2Pieces[i].Y = y1;
                return;
            }

            throw new ArgumentException("Coudn't find the required piece");
        }

        internal void RestorePiece(PieceMinified captured, bool player)
        {
            captured.IsCaptured = true;

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

        private void RestorePiece(PieceMinified[] playersPieces, PieceMinified captured)
        {
            for (byte i = 0; i < playersPieces.Length; i++)
            {
                if (playersPieces[i].IsEmpty())
                {
                    throw new ArgumentException("Couldn't find the required piece");
                }

                if (playersPieces[i].IsCaptured && playersPieces[i].Equals(captured))
                {
                    Pieces[captured.X, captured.Y] = new BoardCell(i, captured.IsWhite);
                    playersPieces[i].IsCaptured = false;
                    return;
                }
            }
        }

        public void ChangePieceType(byte x, byte y, bool canGoDown, bool canGoUp, bool isQueen, bool player)
        {
            var index = Pieces[x, y].GetIndex();
            if (player)
            {
                Player1Pieces[index].CanGoDown = canGoDown;
                Player1Pieces[index].CanGoUp = canGoUp;
                Player1Pieces[index].IsQueen = isQueen;
            }
            else
            {
                Player2Pieces[index].CanGoDown = canGoDown;
                Player2Pieces[index].CanGoUp = canGoUp;
                Player2Pieces[index].IsQueen = isQueen;
            }
        }

        public void UpdatePieceAvailableMoves(byte x, byte y, Cell[] availableMoves, bool player)
        {
            var index = Pieces[x, y].GetIndex();
            if (player)
            {
                Player1Pieces[index].AvailableMoves = availableMoves;
            }
            else
            {
                Player2Pieces[index].AvailableMoves = availableMoves;
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
            for (var i = 0; i < Player1Pieces.Length; i++)
            {
                if (Player1Pieces[i].IsEmpty())
                {
                    break;
                }
                Player1Pieces[i].ClearMoves();
            }

            for (var i = 0; i < Player2Pieces.Length; i++)
            {
                if (Player2Pieces[i].IsEmpty())
                {
                    break;
                }
                Player2Pieces[i].ClearMoves();
            }
        }

        public void Validate()
        {
            var index = 0;
            for (var i = 0; i < Player1Pieces.Length; i++)
            {
                if (Player1Pieces[i].IsEmpty())
                {
                    break;
                }
                if (!Player1Pieces[i].IsCaptured)
                {
                    index++;
                }
            }
            if (index != Player1PiecesCount)
            {
                throw new InvalidOperationException();
            }
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
