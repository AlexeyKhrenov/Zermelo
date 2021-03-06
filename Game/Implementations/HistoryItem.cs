﻿using Game.Primitives;
using Game.PublicInterfaces;

namespace Game.Implementations
{
    public class HistoryItem : IHistoryItem
    {
        public HistoryItem(IPlayer player, Move move)
        {
            IsPieceChangeType = false;
            IsKill = false;
            Captured = null;

            Player = player;
            Move = move;
        }

        public IPlayer Player { get; }

        public Move Move { get; set; }

        public bool IsPieceChangeType { get; set; }

        public bool IsKill { get; set; }

        public IFigure Captured { get; set; }

        public override string ToString()
        {
            return $"{Move.From.X}, {Move.From.Y} -> {Move.To.X}, {Move.To.Y}";
        }
    }
}