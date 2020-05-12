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
    unsafe internal struct BoardMinified
    {
        // todo rename to board cell
        public BoardCell[,] Pieces { get; set; }

        public const byte BufferSize = 20;
        public fixed int Player1Pieces[BufferSize];

        public fixed int Player2Pieces[BufferSize];

        public bool ActivePlayer { get; set; }

        // little optimisation for GameNode evaluation
        public byte Player1PiecesCount { get; set; }

        // little optimisation for GameNode evaluation
        public byte Player2PiecesCount { get; set; }

        public BoardMinified(int size)
        {
            Pieces = new BoardCell[size, size];
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
            return (byte)Pieces.GetLength(0);
        }

        public PieceMinified RemovePiece(int x, int y, bool player)
        {
            var index = Pieces[x, y].GetIndex();
            Pieces[x, y].RemovePiece();

            if (player)
            {
                Player1PiecesCount--;
                var toRemove = (PieceMinified)Player1Pieces[index];
                toRemove.IsCaptured = true;
                toRemove.ClearMoves();
                Player1Pieces[index] = toRemove;
                return toRemove;
            }
            else
            {
                Player2PiecesCount--;
                var toRemove = (PieceMinified)Player2Pieces[index];
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
                var piece = (PieceMinified)Player1Pieces[i];
                piece.X = x1;
                piece.Y = y1;
                Player1Pieces[i] = piece;
                return;
            }
            else
            {
                var piece = (PieceMinified)Player2Pieces[i];
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
                    var piece = (PieceMinified)Player1Pieces[i];
                    if (piece.IsEmpty())
                    {
                        throw new ArgumentException("Couldn't find the required piece");
                    }

                    if (piece.Equals(captured))
                    {
                        Pieces[captured.X, captured.Y] = new BoardCell(i, captured.IsWhite);
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
                    var piece = (PieceMinified)Player2Pieces[i];
                    if (piece.IsEmpty())
                    {
                        throw new ArgumentException("Couldn't find the required piece");
                    }

                    if (piece.Equals(captured))
                    {
                        Pieces[captured.X, captured.Y] = new BoardCell(i, captured.IsWhite);
                        piece.IsCaptured = false;
                        Player2Pieces[i] = piece;
                        break;
                    }
                }
            }
        }

        public void ChangePieceType(byte x, byte y, bool canGoDown, bool canGoUp, bool isQueen, bool player)
        {
            var index = Pieces[x, y].GetIndex();
            if (player)
            {
                var piece = (PieceMinified)Player1Pieces[index];
                piece.CanGoDown = canGoDown;
                piece.CanGoUp = canGoUp;
                piece.IsQueen = isQueen;
                Player1Pieces[index] = piece;
            }
            else
            {
                var piece = (PieceMinified)Player2Pieces[index];
                piece.CanGoDown = canGoDown;
                piece.CanGoUp = canGoUp;
                piece.IsQueen = isQueen;
                Player2Pieces[index] = piece;
            }
        }

        public PieceMinified GetPiece(byte x, byte y, bool player)
        {
            var cell = Pieces[x, y];
            if (player)
            {
                return (PieceMinified)Player1Pieces[cell.GetIndex()];
            }
            else
            {
                return (PieceMinified)Player2Pieces[cell.GetIndex()];
            }
        }

        public void UpdatePieceAvailableMoves(PieceMinified piece, bool player)
        {
            var cell = Pieces[piece.X, piece.Y];
            var index = cell.GetIndex();
            if (player)
            {
                var target = (PieceMinified)Player1Pieces[index];
                target.UpdateAvailableMoves(piece);
                Player1Pieces[index] = target;
            }
            else
            {
                var target = (PieceMinified)Player2Pieces[index];
                target.UpdateAvailableMoves(piece);
                Player2Pieces[index] = target;
            }
        }

        // todo - remove after it becomes a structure
        public void ClearMoves()
        {
            for (var i = 0; i < BufferSize; i++)
            {
                var piece = (PieceMinified)Player1Pieces[i];
                if (piece.IsEmpty())
                {
                    break;
                }
                piece.ClearMoves();
                Player1Pieces[i] = piece;
            }

            for (var i = 0; i < BufferSize; i++)
            {
                var piece = (PieceMinified)Player2Pieces[i];
                if (piece.IsEmpty())
                {
                    break;
                }
                piece.ClearMoves();
                Player2Pieces[i] = piece;
            }
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// not optimized
        /// </summary>
        public List<PieceMinified> GetPlayer1PiecesList()
        {
            var result = new List<PieceMinified>();
            fixed (int* player1Ptr = Player1Pieces)
            {
                for (var i = 0; i < BufferSize; i++)
                {
                    var piece = (PieceMinified)(*(player1Ptr + i));
                    if (piece.IsEmpty())
                    {
                        break;
                    }
                    result.Add(piece);
                }
            }
            return result;
        }

        /// <summary>
        /// not optimized
        /// </summary>
        public List<PieceMinified> GetPlayer2PiecesList()
        {
            var result = new List<PieceMinified>();
            fixed (int* player2Ptr = Player2Pieces)
            {
                for (var i = 0; i < BufferSize; i++)
                {
                    var piece = (PieceMinified)(*(player2Ptr + i));
                    if (piece.IsEmpty())
                    {
                        break;
                    }
                    result.Add(piece);
                }
            }
            return result;
        }
    }
}
