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
        public void ToLCD_Lowercase_StringIsEmpty()
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

        [Test]
        public void ToLCD_RightOneInput_ReturnStringAsExpected()
        {
            var testInput = 1.ToString();
            var expectedResult = $"...{Environment.NewLine}..|{Environment.NewLine}..|";

            var result = lcd.ToLCD(testInput);

            result.Should().Be(expectedResult);
        }

        [Test]
        public void ToLCD_RightTwoInput_ReturnStringAsExpected()
        {
            var testInput = 2.ToString();
            var expectedResult = $"._.{Environment.NewLine}._|{Environment.NewLine}|_.";

            var result = lcd.ToLCD(testInput);

            result.Should().Be(expectedResult);
        }

        [Test]
        public void ToLCD_RightThreeInput_ReturnStringAsExpected()
        {
            var testInput = 3.ToString();
            var expectedResult = $"._.{Environment.NewLine}._|{Environment.NewLine}._|";

            var result = lcd.ToLCD(testInput);

            result.Should().Be(expectedResult);
        }

        [Test]
        public void ToLCD_RightFourInput_ReturnStringAsExpected()
        {
            var testInput = 4.ToString();
            var expectedResult = $"...{Environment.NewLine}|_|{Environment.NewLine}..|";

            var result = lcd.ToLCD(testInput);

            result.Should().Be(expectedResult);
        }

        [Test]
        public void ToLCD_RightFiveInput_ReturnStringAsExpected()
        {
            var testInput = 5.ToString();
            var expectedResult = $"._.{Environment.NewLine}|_.{Environment.NewLine}._|";

            var result = lcd.ToLCD(testInput);

            result.Should().Be(expectedResult);
        }

        [Test]
        public void ToLCD_RightSixInput_ReturnStringAsExpected()
        {
            var testInput = 6.ToString();
            var expectedResult = $"._.{Environment.NewLine}|_.{Environment.NewLine}|_|";

            var result = lcd.ToLCD(testInput);

            result.Should().Be(expectedResult);
        }

        [Test]
        public void ToLCD_RightSevenInput_ReturnStringAsExpected()
        {
            var testInput = 7.ToString();
            var expectedResult = $"._.{Environment.NewLine}..|{Environment.NewLine}..|";

            var result = lcd.ToLCD(testInput);

            result.Should().Be(expectedResult);
        }

        [Test]
        public void ToLCD_RightEightInput_ReturnStringAsExpected()
        {
            var testInput = 8.ToString();
            var expectedResult = $"._.{Environment.NewLine}|_|{Environment.NewLine}|_|";

            var result = lcd.ToLCD(testInput);

            result.Should().Be(expectedResult);
        }

        [Test]
        public void ToLCD_RightNineInput_ReturnStringAsExpected()
        {
            var testInput = 9.ToString();
            var expectedResult = $"._.{Environment.NewLine}|_|{Environment.NewLine}..|";

            var result = lcd.ToLCD(testInput);

            result.Should().Be(expectedResult);
        }

        [Test]
        public void ToLCD_RightNineOneZeroInput_ReturnStringAsExpected()
        {
            var testInput = 910.ToString();
            var expectedResult =
                $"._.{Environment.NewLine}|_|{Environment.NewLine}..|{Environment.NewLine}...{Environment.NewLine}..|{Environment.NewLine}..|{Environment.NewLine}._.{Environment.NewLine}|.|{Environment.NewLine}|_|";

            var result = lcd.ToLCD(testInput);

            result.Should().Be(expectedResult);
        }
    }
}