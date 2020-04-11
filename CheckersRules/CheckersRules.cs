using System;
using System.Collections.Generic;
using Game.PublicInterfaces;

namespace CheckersRules
{
    public class CheckersRules : IGameRules
    {
        public List<IFigure> CreateInitialPosition(int size, bool changedSides)
        {
            var figures = new List<IFigure>();

            if (size < 4)
            {
                throw new NotImplementedException("Game size smaller than 4");
            }

            // positioning pieces
            var firstColor = changedSides ? PieceTypes.Black : PieceTypes.White;
            for (var y = size; y > size / 2 - 1; y++)
            {
                var startX = 1 - y % 2;
                for (var x = startX; x < size; x += 2)
                {
                    figures.Add(new Piece(x, y, firstColor));
                }
            }

            var secondColor = changedSides ? PieceTypes.White : PieceTypes.Black;
            for (var y = 0; y < size / 2 - 1; y++)
            {
                var startX = 1 - y % 2;
                for (var x = startX; x < size; x += 2)
                {
                    figures.Add(new Piece(x, y, firstColor));
                }
            }

            return figures;
        }
    }
}
