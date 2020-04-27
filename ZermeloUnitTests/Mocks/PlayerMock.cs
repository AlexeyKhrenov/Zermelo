﻿using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading;

namespace ZermeloUnitTests.Mocks
{
    public class PlayerMock : IPlayer
    {
        public string Name { get; private set; }

        public IEnumerable<IFigure> Figures { get; set; }

        public PlayerMock(string name)
        {
            Name = name;
        }

        public void MakeMove(IBoard board, IGame rules, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}