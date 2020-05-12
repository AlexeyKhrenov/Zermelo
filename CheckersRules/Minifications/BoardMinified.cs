using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Checkers.Minifications
{
    // todo - this should become a struct with methods
    internal unsafe struct BoardMinified
    {
        // todo rename to board cell
        public byte[] Pieces { get; set; }

        public const byte BufferSize = 12;
        public fixed int Player1Pieces[BufferSize];

        public fixed int Player2Pieces[BufferSize];

        public bool ActivePlayer { get; set; }

        // little optimisation for GameNode evaluation
        public byte Player1PiecesCount { get; set; }

        // little optimisation for GameNode evaluation
        public byte Player2PiecesCount { get; set; }

        private readonly byte _size;

        public BoardMinified(byte size)
        {
            _size = size;
            Pieces = new byte[size * size];
            ActivePlayer = true;
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
            return _size;
        }

        public PieceMinified RemovePiece(byte x, byte y, bool player)
        {
            var cell = GetBoardCell(x, y);
            var i = cell.GetIndex();
            ClearBoardCell(x, y);

            if (player)
            {
                Player1PiecesCount--;
                var toRemove = (PieceMinified) Player1Pieces[i];
                toRemove.IsCaptured = true;
                toRemove.ClearMoves();
                Player1Pieces[i] = toRemove;
                return toRemove;
            }
            else
            {
                Player2PiecesCount--;
                var toRemove = (PieceMinified) Player2Pieces[i];
                toRemove.IsCaptured = true;
                toRemove.ClearMoves();
                Player2Pieces[i] = toRemove;
                return toRemove;
            }
        }

        public void MovePiece(byte x0, byte y0, byte x1, byte y1, bool player)
        {
            var cell = GetBoardCell(x0, y0);
            var i = cell.GetIndex();

            SetBoardCell(x1, y1, cell);
            ClearBoardCell(x0, y0);

            if (player)
            {
                var piece = (PieceMinified) Player1Pieces[i];
                piece.X = x1;
                piece.Y = y1;
                Player1Pieces[i] = piece;
                return;
            }
            else
            {
                var piece = (PieceMinified) Player2Pieces[i];
                piece.X = x1;
                piece.Y = y1;
                Player2Pieces[i] = piece;
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
                // todo - we know the size of BoardMinified fixed buffers in advance
                for (byte i = 0; i < BufferSize; i++)
                {
                    var piece = (PieceMinified) Player1Pieces[i];
                    if (piece.IsEmpty()) throw new ArgumentException("Couldn't find the required piece");

                    if (piece.Equals(captured))
                    {
                        SetBoardCell(captured.X, captured.Y, new BoardCell(i, captured.IsWhite));
                        piece.IsCaptured = false;
                        Player1Pieces[i] = piece;
                        break;
                    }
                }
            }
            else
            {
                Player2PiecesCount++;
                // todo - we know the size of BoardMinified fixed buffers in advance
                for (byte i = 0; i < BufferSize; i++)
                {
                    var piece = (PieceMinified) Player2Pieces[i];
                    if (piece.IsEmpty()) throw new ArgumentException("Couldn't find the required piece");

                    if (piece.Equals(captured))
                    {
                        SetBoardCell(captured.X, captured.Y, new BoardCell(i, captured.IsWhite));
                        piece.IsCaptured = false;
                        Player2Pieces[i] = piece;
                        break;
                    }
                }
            }
        }

        public void ChangePieceType(byte x, byte y, bool canGoDown, bool canGoUp, bool isQueen, bool player)
        {
            var index = GetBoardCell(x, y).GetIndex();
            if (player)
            {
                var piece = (PieceMinified) Player1Pieces[index];
                piece.CanGoDown = canGoDown;
                piece.CanGoUp = canGoUp;
                piece.IsQueen = isQueen;
                Player1Pieces[index] = piece;
            }
            else
            {
                var piece = (PieceMinified) Player2Pieces[index];
                piece.CanGoDown = canGoDown;
                piece.CanGoUp = canGoUp;
                piece.IsQueen = isQueen;
                Player2Pieces[index] = piece;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BoardCell GetBoardCell(byte x, byte y)
        {
            return (BoardCell) Pieces[_size * y + x];
        }

        public void SetBoardCell(byte x, byte y, BoardCell boardCell)
        {
            Pieces[_size * y + x] = boardCell;
        }

        public void ClearBoardCell(byte x, byte y)
        {
            Pieces[_size * y + x] = 0;
        }

        public PieceMinified GetPiece(byte x, byte y, bool player)
        {
            var cell = GetBoardCell(x, y);
            if (player)
                return (PieceMinified) Player1Pieces[cell.GetIndex()];
            return (PieceMinified) Player2Pieces[cell.GetIndex()];
        }

        public void UpdatePieceAvailableMoves(PieceMinified piece, bool player)
        {
            var cell = GetBoardCell(piece.X, piece.Y);
            var index = cell.GetIndex();
            if (player)
            {
                var target = (PieceMinified) Player1Pieces[index];
                target.UpdateAvailableMoves(piece);
                Player1Pieces[index] = target;
            }
            else
            {
                var target = (PieceMinified) Player2Pieces[index];
                target.UpdateAvailableMoves(piece);
                Player2Pieces[index] = target;
            }
        }

        // todo - remove after it becomes a structure
        public void ClearMoves()
        {
            for (var i = 0; i < BufferSize; i++)
            {
                var piece = (PieceMinified) Player1Pieces[i];
                if (piece.IsEmpty()) break;
                piece.ClearMoves();
                Player1Pieces[i] = piece;
            }

            for (var i = 0; i < BufferSize; i++)
            {
                var piece = (PieceMinified) Player2Pieces[i];
                if (piece.IsEmpty()) break;
                piece.ClearMoves();
                Player2Pieces[i] = piece;
            }
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     not optimized
        /// </summary>
        public List<PieceMinified> GetPlayer1PiecesList()
        {
            var result = new List<PieceMinified>();
            fixed (int* player1Ptr = Player1Pieces)
            {
                for (var i = 0; i < BufferSize; i++)
                {
                    var piece = (PieceMinified) (*(player1Ptr + i));
                    if (piece.IsEmpty()) break;
                    result.Add(piece);
                }
            }

            return result;
        }

        /// <summary>
        ///     not optimized
        /// </summary>
        public List<PieceMinified> GetPlayer2PiecesList()
        {
            var result = new List<PieceMinified>();
            fixed (int* player2Ptr = Player2Pieces)
            {
                for (var i = 0; i < BufferSize; i++)
                {
                    var piece = (PieceMinified) (*(player2Ptr + i));
                    if (piece.IsEmpty()) break;
                    result.Add(piece);
                }
            }

            return result;
        }
    }
}