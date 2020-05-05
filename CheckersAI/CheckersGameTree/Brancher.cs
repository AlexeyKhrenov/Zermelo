using Checkers;
using Checkers.Minifications;
using CheckersAI.InternalInterfaces;
using Game.Primitives;
using System.Collections.Generic;

namespace CheckersAI.CheckersGameTree
{
    internal class Brancher : IBrancher<GameNode, BoardMinified, sbyte>
    {
        public Brancher()
        {
        }

        public void Branch(GameNode node, BoardMinified practiceBoard)
        {
            // remove list to speed up perfomance
            var children = new List<GameNode>();
            foreach (var piece in practiceBoard.ActiveSet)
            {
                foreach (var move in piece.AvailableMoves)
                {
                    var child = new GameNode();
                    child.Move = new HistoryItemMinified(new Cell(piece.X, piece.Y), move, practiceBoard.ActivePlayer);

                    // todo - remove - duplicated information
                    child.IsMaxPlayer = !practiceBoard.ActivePlayer;
                    child.Parent = node;
                    children.Add(child);
                }
            }

            node.Children = children.ToArray();
        }
    }
}
