using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Minifications
{
    // todo - this should become a struct with methods
    internal class BoardMinified : IMementoMinification<IBoard>
    {
        public Piece[,] Pieces { get; set; }
        public Piece[] Player1Pieces { get; set; }
        public Piece[] Player2Pieces { get; set; }
        public Piece[] ActiveSetOfFigures { get; set; }
        public Piece[] AwaitingSetOfFigures { get; set; }

        public bool InvertedCoordinates { get; set; }
        public int ActivePlayer { get; set; }

        public void SwitchPlayers()
        {
            if (ActiveSetOfFigures == Player1Pieces)
            {
                ActiveSetOfFigures = Player2Pieces;
            }
            else
            {
                ActiveSetOfFigures = Player1Pieces;
            }
        }

        public int GetSize()
        {
            return Pieces.GetLength(0);
        }

        internal bool IsActivePlayer(int player)
        {
            throw new NotImplementedException();
        }

        public Piece RemovePiece(int x, int y)
        {
        }

        internal void RestorePiece(Piece captured)
        {
            throw new NotImplementedException();
        }

        public IBoard Restore()
        {
            throw new NotImplementedException();
        }

        public void Minify(IBoard maximizedSource)
        {
            throw new NotImplementedException();
        }
    }
}
