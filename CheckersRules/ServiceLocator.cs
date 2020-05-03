using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers
{
    public static class ServiceLocator
    {
        public static IGameRules CreateRules()
        {
            return new CheckersRules();
        }
    }
}
