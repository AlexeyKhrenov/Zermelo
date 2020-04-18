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

            var figure = game.ActivePlayer.Figures.First(f => f.X == piece.X && f.Y == piece.Y);
            game.ActivePlayer.Figures.Remove(figure);

            figure.X = piece.X;
            figure.Y = piece.Y;

            game.ActivePlayer.Figures.Add(figure);

            ClearAvailableMoves(game.Player1);
            ClearAvailableMoves(game.Player2);

            pieces[latestMove.From.X, latestMove.From.Y] = null;
            pieces[latestMove.To.X, latestMove.To.Y] = piece;

            Next(game, pieces, latestMove);
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
