﻿using System;

namespace ZermeloCheckers.GameFactory
{
    public class GameRequest
    {
        public GameRequest(byte gameSize)
        {
            GameSize = gameSize;
        }

        public byte GameSize { get; }

        public string Player1Name { get; set; }

        public string Player2Name { get; set; }

        public bool IsPlayer1ComputerPlayer { get; set; }

        public bool IsPlayer2ComputerPlayer { get; set; }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Player1Name) || string.IsNullOrWhiteSpace(Player2Name))
                throw new ArgumentException("Invalid game request - player name can't be empty");

            if (GameSize < 4 || GameSize > 8) throw new ArgumentException("GameSize must be between 4 and 8");
        }
    }
}