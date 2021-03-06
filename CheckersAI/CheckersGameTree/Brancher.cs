﻿using System.Collections.Generic;
using Checkers;
using Checkers.Minifications;
using CheckersAI.InternalInterfaces;
using Game.Primitives;

namespace CheckersAI.CheckersGameTree
{
    internal unsafe class Brancher : IBrancher<GameNode, BoardMinified, sbyte>
    {
        private readonly IEvaluator<BoardMinified, sbyte> _evaluator;
        private readonly CheckersRules _rules;
        private readonly IStateTransitions<BoardMinified, GameNode, sbyte> _stateTransitions;

        public Brancher(CheckersRules rules, IEvaluator<BoardMinified, sbyte> evaluator,
            IStateTransitions<BoardMinified, GameNode, sbyte> stateTransitions)
        {
            _rules = rules;
            _evaluator = evaluator;
            _stateTransitions = stateTransitions;
        }

        public void Branch(GameNode node, BoardMinified practiceBoard)
        {
            // todo - remove list to speed up perfomance
            var children = new List<GameNode> {Capacity = 16};

            node.ExpectedFinalizedFlag = 0;

            practiceBoard = _rules.FastForwardAvailableMoves(practiceBoard);

            var activeSetPtr = practiceBoard.ActivePlayer ? practiceBoard.Player1Pieces : practiceBoard.Player2Pieces;

            var childBitIndex = 0;

            for (var i = 0; i < BoardMinified.BufferSize; i++)
            {
                var piece = (PieceMinified) (*(activeSetPtr + i));

                if (piece.IsEmpty()) break;
                foreach (var move in piece.GetAvailableMoves())
                    if (move.IsNotNull)
                    {
                        var child = new GameNode(!practiceBoard.ActivePlayer);
                        child.Move = new HistoryItemMinified(new Cell(piece.X, piece.Y), move,
                            practiceBoard.ActivePlayer);

                        // todo - remove - duplicated information
                        child.Parent = node;
                        children.Add(child);
                        child.ChildAddressBit = 1 << childBitIndex;
                        node.ExpectedFinalizedFlag += child.ChildAddressBit;
                        childBitIndex++;
                    }
                    else
                    {
                        break;
                    }
            }

            foreach (var child in children)
            {
                var copy = _stateTransitions.Copy(practiceBoard);
                var localState = _stateTransitions.GoDown(copy, child);
                child.TerminationResult = _evaluator.Evaluate(localState);
                child.IsEvaluated = true;
            }

            node.Children = children.ToArray();
            InsertionSort(node.Children, node.IsMaxPlayer);
        }

        private static void InsertionSort(GameNode[] childNodes, bool isMaxPlayer)
        {
            for (var i = 0; i < childNodes.Length - 1; i++)
            for (var j = i + 1; j > 0; j--)
                if (isMaxPlayer)
                {
                    if (childNodes[j - 1].TerminationResult < childNodes[j].TerminationResult)
                    {
                        var temp = childNodes[j - 1];
                        childNodes[j - 1] = childNodes[j];
                        childNodes[j] = temp;
                    }
                }
                else
                {
                    if (childNodes[j - 1].TerminationResult > childNodes[j].TerminationResult)
                    {
                        var temp = childNodes[j - 1];
                        childNodes[j - 1] = childNodes[j];
                        childNodes[j] = temp;
                    }
                }
        }
    }
}