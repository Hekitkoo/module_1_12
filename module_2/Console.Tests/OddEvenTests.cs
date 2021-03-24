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
        private const string EvenText = "Even";
        private const string OddText = "Odd";
        private const int PrimeIndex = 0;
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
            var expectedResult = $"{OddText}, {EvenText}";

            var result = oddEven.Print(StartIndex, TestRangeEndIndex);

            result.Should().Be(expectedResult);
        }

        [Test]
        public void Print_RangeAreSetWrong_ThrowException()
        {
            Action action = () => oddEven.Print(TestRangeEndIndex, StartIndex);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Print_RangeAreSetWithEvenRange_EvenTextResult()
        {
            var expectedResult = EvenText;

            var result = oddEven.Print(TestRangeEndIndex, TestRangeEndIndex);

            result.Should().Be(expectedResult);
        }

        [Test]
        public void Print_RangeAreSetWithOddRange_OddTextResult()
        {
            var expectedResult = OddText;

            var result = oddEven.Print(StartIndex, StartIndex);

            result.Should().Be(expectedResult);
        }

        [Test]
        public void Print_RangeAreSetWithPrimeRange_PrimeTextResult()
        {
            var expectedResult = PrimeIndex.ToString();

            var result = oddEven.Print(PrimeIndex, PrimeIndex);

            result.Should().Be(expectedResult);
        }
    }
}