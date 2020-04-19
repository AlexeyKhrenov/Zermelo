using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Minifications
{
    public class PlayerMinified : IMementoMinification<IPlayer>
    {
        public Piece[] Pieces { get; set; }

        public void Minify(IPlayer maximizedSource)
        {

        }

        public IPlayer Restore()
        {
            throw new NotImplementedException();
        }
    }
}
