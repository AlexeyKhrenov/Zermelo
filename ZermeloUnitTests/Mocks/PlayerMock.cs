﻿using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZermeloUnitTests.Mocks
{
    public class PlayerMock : IPlayer
    {
        public string Name { get; private set; }

        public bool IsComputerPlayer => false;

        public IEnumerable<IFigure> Figures { get; set; }

        public int MaxPly => throw new NotImplementedException();

        public int TimeToThinkMs { get; set; }
        public bool IsActive { get; set; }

        public PlayerMock(string name)
        {
            Name = name;
        }

        public Task MakeMove(IGame game, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
