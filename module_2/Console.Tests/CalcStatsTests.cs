using System;
using FluentAssertions;
using NUnit.Framework;

namespace Console.Tests
{
    public class CalcStatsTests
    {
        private CalcStats calcStats;

        [SetUp]
        public void Setup()
        {
            calcStats = new CalcStats();
        }

        [Test]
        public void Process_IntArrayIsEmpty_ArgumentException()
        {
            var emptyArray = Array.Empty<int>();

            Action action = () => calcStats.Process(emptyArray);

            action.Should().Throw<ArgumentException>();
        }
    }
}