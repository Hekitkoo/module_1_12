using System;
using FluentAssertions;
using NUnit.Framework;

namespace Console.Tests
{
    public class NumberQualifierTests
    {
        private const int StartIndex = 1;
        private const int EndIndex = 100;
        private const int EvenIndex = 2;
        private const string EvenText = "Even";
        private const string OddText = "Odd";
        private const int PrimeIndex = 0;
        private NumberQualifier numberQualifier;

        [SetUp]
        public void Setup()
        {
            numberQualifier = new NumberQualifier();
        }

        [Test]
        public void Print_RangeAreSet_ResultIsNotNullOrEmpty()
        {
            var result = numberQualifier.Print(StartIndex, EndIndex);

            result.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void Print_RangeAreSet_ResultIsSameAsExpected()
        {
            var expectedResult = $"{OddText}, {EvenText}";

            var result = numberQualifier.Print(StartIndex, EvenIndex);

            result.Should().Be(expectedResult);
        }

        [Test]
        public void Print_RangeAreSetWrong_ThrowException()
        {
            Action action = () => numberQualifier.Print(EvenIndex, StartIndex);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Print_RangeAreSetWithEvenRange_EvenTextResult()
        {
            var expectedResult = EvenText;

            var result = numberQualifier.Print(EvenIndex, EvenIndex);

            result.Should().Be(expectedResult);
        }

        [Test]
        public void Print_RangeAreSetWithOddRange_OddTextResult()
        {
            var expectedResult = OddText;

            var result = numberQualifier.Print(StartIndex, StartIndex);

            result.Should().Be(expectedResult);
        }

        [Test]
        public void Print_RangeAreSetWithPrimeRange_PrimeTextResult()
        {
            var expectedResult = PrimeIndex.ToString();

            var result = numberQualifier.Print(PrimeIndex, PrimeIndex);

            result.Should().Be(expectedResult);
        }

        [Test]
        public void Print_NumberIsInput_EvenTextResult()
        {
            var expectedResult = EvenText;

            var result = numberQualifier.Print(EvenIndex);

            result.Should().Be(expectedResult);
        }

        [Test]
        public void Print_NumberIsInput_OddTextResult()
        {
            var expectedResult = OddText;

            var result = numberQualifier.Print(StartIndex);

            result.Should().Be(expectedResult);
        }

        [Test]
        public void Print_NumberIsInput_PrimeTextResult()
        {
            var expectedResult = PrimeIndex.ToString();

            var result = numberQualifier.Print(PrimeIndex);

            result.Should().Be(expectedResult);
        }
    }
}