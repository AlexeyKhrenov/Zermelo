using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZermeloUnitTests.Mocks
{
    public class PlayerMock : IPlayer
    {
        public string Name => string.Empty;

        public IEnumerable<IFigure> Figures { get; set; }

        public void MakeMove(IBoard board, IGameRules rules)
        {
            throw new NotImplementedException();
        }
    }
}
