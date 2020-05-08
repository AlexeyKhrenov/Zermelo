using Checkers.Minifications;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ZermeloUnitTests.PrimitivesMinifications
{
    public class PieceMinificationsTest
    {
        [Fact]
        public void PieceMinificationsTest_1()
        {
            var piece1 = new PieceMinified();
            var piece2 = new PieceMinified();
            Assert.True(piece1.Equals(piece2));

            piece2.CanGoDown = true;
            Assert.True(piece1.Equals(piece2));

            piece1.CanGoUp = true;
            Assert.True(piece1.Equals(piece2));

            piece1.IsCaptured = true;
            Assert.False(piece1.Equals(piece2));

            piece2.IsCaptured = true;
            Assert.True(piece1.Equals(piece2));
        }
    }
}
