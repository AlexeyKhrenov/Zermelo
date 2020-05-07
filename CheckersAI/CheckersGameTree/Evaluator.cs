using Checkers.Minifications;
using CheckersAI.InternalInterfaces;
using System.Collections.Generic;

namespace CheckersAI.CheckersGameTree
{
    internal class Evaluator : IEvaluator<BoardMinified, sbyte>
    {
        public Evaluator()
        {
        }

        // todo - perfomance arch valuable point
        public sbyte Evaluate(BoardMinified board)
        {
            if (board.Player1PiecesCount == 0)
            {
                return sbyte.MinValue;
            }

            if (board.Player2PiecesCount == 0)
            {
                return sbyte.MaxValue;
            }

            sbyte result = 0;

            foreach (var piece in board.Player1Pieces)
            {
                if (!piece.IsCaptured)
                {
                    result += (sbyte)piece.CountAvailableMoves();
                }
            }

            foreach (var piece in board.Player2Pieces)
            {
                if (!piece.IsCaptured)
                {
                    result -= (sbyte)piece.CountAvailableMoves();
                }
            }

            return result;
        }
    }
}
