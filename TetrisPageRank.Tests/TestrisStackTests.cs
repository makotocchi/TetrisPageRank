using FluentAssertions;
using System;
using Xunit;

namespace TetrisPageRank.Tests
{
    public class TestrisStackTests
    {
        [Fact]
        public void CreateStack_ReceivesStackDigits_ReturnsCompactStack()
        {
            int[] stackDigits = new[] { 0, 1, -2, 0, 0, -1, 0, 1 };
            int expectedStack = 1160004421;

            int compactStack = TetrisStack.CreateStack(stackDigits);

            compactStack.Should().Be(expectedStack);
        }

        [Fact]
        public void GetReadableDigits_ReceivesBase9Stack_ReturnsReadableDigits()
        {
            int compactStack = 1160004421;
            int[] expectedDigits = new[] { 0, 1, -2, 0, 0, -1, 0, 1 };

            int[] stackDigits = TetrisStack.GetReadableDigits(compactStack);

            stackDigits.Should().BeEquivalentTo(expectedDigits);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 2)]
        [InlineData(2, 3)]
        [InlineData(3, 4)]
        [InlineData(4, 0)]
        [InlineData(5, -1)]
        [InlineData(6, -2)]
        [InlineData(7, -3)]
        public void SetColumnHeight_ReceivesColumnAndHeight_ReturnsProperStack(int column, int height)
        {
            int[] stackDigits = new[] { 1, 1, 1, 1, 1, 1, 1, 1 };
            int compactStack = TetrisStack.CreateStack(stackDigits);
            int[] expectedDigits = (int[])stackDigits.Clone();
            expectedDigits[column] = height;
            int expectedCompactStack = TetrisStack.CreateStack(expectedDigits);

            int resultingStack = TetrisStack.SetColumnHeight(compactStack, column, height);

            resultingStack.Should().Be(expectedCompactStack);
        }

        [Theory]
        [InlineData(0, 1, 2)]
        [InlineData(1, 2, 3)]
        [InlineData(2, 3, 4)]
        [InlineData(3, 4, 4)]
        [InlineData(4, 0, 1)]
        [InlineData(5, -1, 0)]
        [InlineData(6, -2, -1)]
        [InlineData(7, -3, -2)]
        public void ModifyColumnHeight_ReceivesColumnAndHeight_ReturnsProperStack(int column, int height, int expectedHeight)
        {
            int[] stackDigits = new[] { 1, 1, 1, 1, 1, 1, 1, 1 };
            int compactStack = TetrisStack.CreateStack(stackDigits);
            int[] expectedDigits = (int[])stackDigits.Clone();
            expectedDigits[column] = expectedHeight;
            int expectedCompactStack = TetrisStack.CreateStack(expectedDigits);

            int resultingStack = TetrisStack.ModifyColumnHeight(compactStack, column, height);

            resultingStack.Should().Be(expectedCompactStack);
        }
    }
}

