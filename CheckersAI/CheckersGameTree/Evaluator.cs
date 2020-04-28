using Checkers.Minifications;
using Game.Primitives;
using System.Collections.Generic;

namespace CheckersAI.CheckersGameTree
{
    internal class Evaluator :
        CheckersAI.TreeSearch.IEvaluator<GameNode, Move, sbyte>
    {
        private bool _playsFor1Player;

        public Evaluator(bool playsFor1Player)
        {
            _playsFor1Player = playsFor1Player;
        }

        public sbyte Evaluate(GameNode node)
        {
            // simply count available moves and checks if winning position
            return _playsFor1Player
                ? Count(node.Board.Player1Pieces, node.Board.Player2Pieces)
                : Count(node.Board.Player2Pieces, node.Board.Player1Pieces);
        }

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
