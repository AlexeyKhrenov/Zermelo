using Checkers.Minifications;
using FluentAssertions;
using Game.Primitives;
using Xunit;

namespace ZermeloUnitTests.PrimitivesMinifications
{
    public class AvailableMovesTest
    {
        private readonly Cell _emptyCell = new Cell();

        [Theory]
        [InlineData(1, 1, false, 3, 3, 4, 4)]
        [InlineData(1, 1, true, 3, 3, 5, 5)]
        [InlineData(1, -1, true, 3, 3, 5, 1)]
        [InlineData(-1, 1, true, 3, 3, 1, 5)]
        [InlineData(-1, 0, false, 3, 3, 2, 2)]
        [InlineData(-1, 1, false, 3, 3, 2, 4)]
        public void AvailableMovesTest_1(sbyte directionRight, sbyte directionDown, bool isCapture, byte x0, byte y0,
            byte x1, byte y1)
        {
            var availableMoves = new AvailableMoves();
            availableMoves.AddDirection(directionRight, directionDown, isCapture);
            var cells = availableMoves.ToCells(x0, y0);

            cells[0].X.Should().Be(x1);
            cells[0].Y.Should().Be(y1);
            cells[1].Should().Be(_emptyCell);
            cells[2].Should().Be(_emptyCell);
            cells[3].Should().Be(_emptyCell);
        }

        [Theory]
        [InlineData(1, 1, 1, -1, false, 3, 3, 4, 4, 4, 2)]
        [InlineData(1, 1, -1, -1, true, 3, 3, 5, 5, 1, 1)]
        [InlineData(-1, 1, 1, -1, false, 3, 3, 4, 2, 2, 4)]
        [InlineData(1, -1, -1, -1, true, 3, 3, 5, 1, 1, 1)]
        public void AvailableMovesTest_2(
            sbyte directionRight1,
            sbyte directionDown1,
            sbyte directionRight2,
            sbyte directionDown2,
            bool isCapture,
            byte x0,
            byte y0,
            byte x11,
            byte y11,
            byte x12,
            byte y12)
        {
            var availableMoves = new AvailableMoves();

            availableMoves.AddDirection(directionRight1, directionDown1, isCapture);
            availableMoves.AddDirection(directionRight2, directionDown2, isCapture);

            var cells = availableMoves.ToCells(x0, y0);

            cells[0].X.Should().Be(x11);
            cells[0].Y.Should().Be(y11);

            cells[1].X.Should().Be(x12);
            cells[1].Y.Should().Be(y12);

            cells[2].Should().Be(_emptyCell);
            cells[3].Should().Be(_emptyCell);
        }

        [Fact]
        public void AvailableMovesTest_3()
        {
            var availableMoves = new AvailableMoves();
            availableMoves.AddDirection(-1, 1, false);
            availableMoves.AddDirection(-1, 1, false);

            var cells = availableMoves.ToCells(7, 5);

            cells[0].X.Should().Be(6);
            cells[0].Y.Should().Be(6);

            cells[1].Should().Be(_emptyCell);
            cells[2].Should().Be(_emptyCell);
            cells[3].Should().Be(_emptyCell);
        }

        [Fact]
        public void AvailableMovesTest_4()
        {
            var availableMoves = new AvailableMoves();
            availableMoves.AddDirection(-1, 1, false);
            availableMoves.AddDirection(-1, 1, true);

            var cells = availableMoves.ToCells(7, 5);

            cells[0].X.Should().Be(5);
            cells[0].Y.Should().Be(7);

            cells[1].Should().Be(_emptyCell);
            cells[2].Should().Be(_emptyCell);
            cells[3].Should().Be(_emptyCell);
        }
    }
}