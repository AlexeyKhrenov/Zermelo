using Game.PublicInterfaces;

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