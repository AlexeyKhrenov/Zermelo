using Checkers.Minifications;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ZermeloUnitTests.PrimitivesMinifications
{
    public class BoardCellTest
    {
        [Fact]
        public void BoardCellTest_1()
        {
            var cell = new BoardCell();
            cell.IsEmpty().Should().BeTrue();
        }

        [Fact]
        public void BoardCellTest_2()
        {
            var cell = new BoardCell(32, true);
            cell.IsEmpty().Should().BeFalse();
            cell.GetIndex().Should().Be(32);
            cell.IsWhite().Should().BeTrue();
        }
    }
}
