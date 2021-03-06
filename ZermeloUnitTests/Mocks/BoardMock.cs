﻿using System;
using System.Collections.Generic;
using System.Linq;
using Checkers;
using Game.PublicInterfaces;

namespace ZermeloUnitTests.Mocks
{
    public class BoardMock : IBoard
    {
        public BoardMock(string[] boardStr, byte size, bool invertedCoordinates)
        {
            InvertedCoordinates = invertedCoordinates;
            Size = size;
            Player1 = new PlayerMock("player1");
            Player2 = new PlayerMock("player2");
            ActivePlayer = Player1;
            AwaitingPlayer = Player2;

            var player1Figures = new List<Piece>();
            var player2Figures = new List<Piece>();

            for (byte i = 0; i < boardStr.Length; i++)
            for (byte j = 0; j < boardStr.Length; j++)
                switch (boardStr[i][j])
                {
                    case 'w':
                        player1Figures.Add(new Piece(j, i, true, !invertedCoordinates, invertedCoordinates, false));
                        break;
                    case 'W':
                        player1Figures.Add(new Piece(j, i, true, true, true, true));
                        break;
                    case 'b':
                        player2Figures.Add(new Piece(j, i, false, invertedCoordinates, !invertedCoordinates, false));
                        break;
                    case 'B':
                        player2Figures.Add(new Piece(j, i, false, true, true, true));
                        break;
                }

            Player1.Figures = player1Figures.Select(x => (IFigure) x).ToList();
            Player2.Figures = player2Figures.Select(x => (IFigure) x).ToList();
        }

        public bool InvertedCoordinates { get; }
        public IPlayer ActivePlayer { get; set; }

        public IPlayer AwaitingPlayer { get; set; }

        public IPlayer Player1 { get; }

        public IPlayer Player2 { get; }

        public byte Size { get; }

        public IEnumerable<IFigure> Figures => throw new NotImplementedException();

        public void SwitchPlayers()
        {
            if (ActivePlayer == Player1)
            {
                ActivePlayer = Player2;
                AwaitingPlayer = Player1;
            }
            else
            {
                ActivePlayer = Player1;
                AwaitingPlayer = Player2;
            }
        }
    }
}