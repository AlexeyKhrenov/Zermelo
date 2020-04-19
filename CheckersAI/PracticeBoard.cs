using Checkers;
using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersAI
{
    class PracticeBoard : IBoard<Piece>
    {
        public IPlayer Player1 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public IPlayer Player2 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public List<Piece> Figures { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
