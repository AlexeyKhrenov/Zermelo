using Checkers.Minifications;
using Game.Primitives;
using System.Collections.Generic;

namespace CheckersAI.CheckersGameTree
{
    internal class Evaluator : TreeSearch.IEvaluator<BoardMinified, sbyte>
    {
        private bool _playsFor1Player;

        public Evaluator(bool playsFor1Player)
        {
            _playsFor1Player = playsFor1Player;
        }

        public sbyte Evaluate(BoardMinified board)
        {
            // simply count available moves and checks if winning position
            return _playsFor1Player
                ? Count(board.Player1Pieces, board.Player2Pieces)
                : Count(board.Player2Pieces, board.Player1Pieces);
        }

        // todo - perfomance arch valuable point
        private sbyte Count(List<PieceMinified> myPieces, List<PieceMinified> hisPieces)
        {
            if (hisPieces.Count == 0)
            {
                return sbyte.MaxValue;
            }

            sbyte result = 0;

            foreach (var piece in myPieces)
            {
                result += (sbyte) piece.AvailableMoves.Count;
            }

            foreach (var piece in hisPieces)
            {
                result -= (sbyte) piece.AvailableMoves.Count;
            }

            return result;
        }
    }
}
