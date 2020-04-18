using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkers.Rules
{
    internal class AppendMoveRule : AbstractRule
    {
        public override string Name => nameof(AppendMoveRule);

        public override void ApplyRule(IGame game, Piece[,] pieces, IHistoryItem latestMove)
        {
            var piece = pieces[latestMove.From.X, latestMove.From.Y];
            piece.X = latestMove.To.X;
            piece.Y = latestMove.To.Y;
            pieces[latestMove.From.X, latestMove.From.Y] = null;
            pieces[latestMove.To.X, latestMove.To.Y] = piece;

            ClearAvailableMoves(game.Player1);
            ClearAvailableMoves(game.Player2);

            Next(game, pieces, latestMove);
        }

        public override void UndoRule(IGame game, Piece[,] pieces, IHistoryItem toUndo, IHistoryItem lastMoveBeforeUndo)
        {
            var piece = pieces[toUndo.To.X, toUndo.To.Y];
            piece.X = toUndo.From.X;
            piece.Y = toUndo.From.Y;
            pieces[toUndo.To.X, toUndo.To.Y] = null;
            pieces[toUndo.From.X, toUndo.From.Y] = piece;

            ClearAvailableMoves(game.Player1);
            ClearAvailableMoves(game.Player2);

            NextUndo(game, pieces, toUndo, lastMoveBeforeUndo);
        }

        private void ClearAvailableMoves(IPlayer player)
        {
            foreach (var figure in player.Figures)
            {
                figure.AvailableMoves.Clear();
            }
        }
    }
}
