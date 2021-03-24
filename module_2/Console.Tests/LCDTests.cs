using System;
using FluentAssertions;
using NUnit.Framework;

namespace Console.Tests
{
    public class LCDTests
    {
        private LCD lcd;

        [SetUp]
        public void Setup()
        {
            lcd = new LCD();
        }

        [Test]
        public void ToLCD_WrongInput_StringIsEmpty()
        {
            var testInput = "a";

            var result = lcd.ToLCD(testInput);

            result.Should().BeEmpty();
        }

        [Test]
        public void ToLCD_RightZeroInput_ReturnStringAsExpected()
        {
            var testInput = 0.ToString();
            var expectedResult = $"._.{Environment.NewLine}|.|{Environment.NewLine}|_|";

            var result = lcd.ToLCD(testInput);

            result.Should().Be(expectedResult);
        }
    }
}