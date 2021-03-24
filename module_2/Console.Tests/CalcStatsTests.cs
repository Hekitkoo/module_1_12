﻿using System;
using FluentAssertions;
using NUnit.Framework;

namespace Console.Tests
{
    public class CalcStatsTests
    {
        private static readonly int[] TestArray = {6, 9, 15, -2, 92, 11};
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
        
        [Test]
        public void Process_IntArrayIsCorrect_MinimumValueResultTextContains()
        {
            var expectedText = "o) minimum value = -2";

            var result = calcStats.Process(TestArray);

            result.Should().Contain(expectedText);
        }
    }
}