using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Console
{
    public class CalcStats
    {
        private const string MinValueText = "minimum value";
        private const string MaxValueText = "maximum value";
        private const string NumbersLengthText = "number of elements in the sequence";
        private const string AverageValueText = "average value";

        public string Process(int[] numbers)
        {
            if (numbers.Length == default)
            {
                throw new ArgumentException();
            }

            var stringBuilder = new StringBuilder();

            var minimumValue = GetMin(numbers);
            var maxValue = GetMax(numbers);
            var numbersLengthValue = GetLength(numbers);
            var averageValue = GetAverage(numbers);

            stringBuilder.AppendLine(GetMessage(minimumValue.ToString(), MinValueText));
            stringBuilder.AppendLine(GetMessage(maxValue.ToString(), MaxValueText));
            stringBuilder.AppendLine(GetMessage(numbersLengthValue.ToString(), NumbersLengthText));
            stringBuilder.AppendLine(GetMessage(averageValue.ToString(), AverageValueText));

            return stringBuilder.ToString();
        }

        private string GetMessage(string value, string minValueText)
        {
            return $"o) {minValueText} = {value}";
        }

        private double GetAverage(int[] numbers)
        {
            return numbers.Average();
        }

        private int GetLength(IReadOnlyCollection<int> numbers)
        {
            return numbers.Count;
        }

        private int GetMax(IEnumerable<int> numbers)
        {
            return numbers.Max();
        }

        private int GetMin(IEnumerable<int> numbers)
        {
            return numbers.Min();
        }
    }
}