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

            return (sbyte)(board.Player1PiecesCount - board.Player2PiecesCount);
        }
    }
}
