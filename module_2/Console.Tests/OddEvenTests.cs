using FluentAssertions;
using NUnit.Framework;

namespace Console.Tests
{
    public class OddEvenTests
    {
        private const int StartIndex = 1;
        private const int EndIndex = 100;
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
    }
}