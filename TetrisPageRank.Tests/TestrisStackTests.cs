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
            int[] stack = new[] { 0, 1, -2, 0, 0, -1, 0, 1 };
            int expectedStack = 1160004421;

            int compactStack = TetrisStackHelper.CreateStack(stack[0], stack[1], stack[2], stack[3], stack[4], stack[5], stack[6], stack[7]);

            compactStack.Should().Be(expectedStack);
        }

        [Fact]
        public void GetReadableDigits_ReceivesBase9Stack_ReturnsReadableDigits()
        {
            int compactStack = 1160004421;
            int[] expectedDigits = new[] { 0, 1, -2, 0, 0, -1, 0, 1 };

            ReadOnlySpan<int> stackDigits = stackalloc[]
            {
                TetrisStackHelper.GetReadableDigit(compactStack, 0),
                TetrisStackHelper.GetReadableDigit(compactStack, 1),
                TetrisStackHelper.GetReadableDigit(compactStack, 2),
                TetrisStackHelper.GetReadableDigit(compactStack, 3),
                TetrisStackHelper.GetReadableDigit(compactStack, 4),
                TetrisStackHelper.GetReadableDigit(compactStack, 5),
                TetrisStackHelper.GetReadableDigit(compactStack, 6),
                TetrisStackHelper.GetReadableDigit(compactStack, 7)
            };

            stackDigits.ToArray().Should().BeEquivalentTo(expectedDigits);
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
            int compactStack = TetrisStackHelper.CreateStack(stackDigits[0], stackDigits[1], stackDigits[2], stackDigits[3], stackDigits[4], stackDigits[5], stackDigits[6], stackDigits[7]);
            int[] expectedDigits = (int[])stackDigits.Clone();
            expectedDigits[column] = height;
            int expectedCompactStack = TetrisStackHelper.CreateStack(expectedDigits[0], expectedDigits[1], expectedDigits[2], expectedDigits[3], expectedDigits[4], expectedDigits[5], expectedDigits[6], expectedDigits[7]);

            int resultingStack = TetrisStackHelper.SetColumnHeight(compactStack, column, height);

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
            int compactStack = TetrisStackHelper.CreateStack(stackDigits[0], stackDigits[1], stackDigits[2], stackDigits[3], stackDigits[4], stackDigits[5], stackDigits[6], stackDigits[7]);
            int[] expectedDigits = (int[])stackDigits.Clone();
            expectedDigits[column] = expectedHeight;
            int expectedCompactStack = TetrisStackHelper.CreateStack(expectedDigits[0], expectedDigits[1], expectedDigits[2], expectedDigits[3], expectedDigits[4], expectedDigits[5], expectedDigits[6], expectedDigits[7]);

            int resultingStack = TetrisStackHelper.AlterColumnHeight(compactStack, column, height);

            resultingStack.Should().Be(expectedCompactStack);
        }
    }
}

