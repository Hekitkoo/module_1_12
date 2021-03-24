using System;
using System.Linq;
using System.Text;

namespace Console
{
    public class CalcStats
    {
        public string Process(int[] numbers)
        {
            Validate(numbers);

            var stringBuilder = new StringBuilder();
            var minimumValue = numbers.Min();
            stringBuilder.Append($"o) minimum value = {minimumValue}");

            return stringBuilder.ToString();
        }

        private void Validate(int[] numbers)
        {
            if (numbers.Length == default)
            {
                throw new ArgumentException();
            }
        }
    }
}