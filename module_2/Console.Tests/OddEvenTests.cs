using System;
using FluentAssertions;
using NUnit.Framework;

namespace Console.Tests
{
    public class OddEvenTests
    {
        private const int StartIndex = 1;
        private const int EndIndex = 100;
        private const int TestRangeEndIndex = 2;
        private OddEven oddEven;

        [SetUp]
        public void Setup()
        {
            oddEven = new OddEven();
        }

        [Test]
        public void Print_RangeAreSet_ResultIsNotNullOrEmpty()
        {
            var result = oddEven.Print(StartIndex, EndIndex);

            result.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void Print_RangeAreSet_ResultIsSameAsExpected()
        {
            var expectedResult = $"{StartIndex}, {TestRangeEndIndex}";

            var result = oddEven.Print(StartIndex, TestRangeEndIndex);

            result.Should().BeSameAs(expectedResult);
        }

        [Test]
        public void Print_RangeAreSetWrong_ThrowException()
        {
            Action action = () => oddEven.Print(StartIndex, TestRangeEndIndex);

            action.Should().Throw<ArgumentException>();
        }
    }
}