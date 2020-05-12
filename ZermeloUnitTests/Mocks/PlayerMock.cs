using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Game.PublicInterfaces;

namespace ZermeloUnitTests.Mocks
{
    public class PlayerMock : IPlayer
    {
        public PlayerMock(string name)
        {
            Name = name;
        }

        public int MaxPly => throw new NotImplementedException();

        public int TimeToThinkMs { get; set; }
        public string Name { get; }

        public bool IsComputerPlayer => false;

        public List<IFigure> Figures { get; set; }
        public bool IsActive { get; set; }

        public Task MakeMove(IGame game, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}