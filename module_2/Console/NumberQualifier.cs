using System;
using System.Text;

namespace Console
{
    public class NumberQualifier
    {
        private const string EvenText = "Even";
        private const string OddText = "Odd";

        public string Print(int startRange, int endRange)
        {
            ValidateStartEndRange(startRange, endRange);

            var stringBuilder = new StringBuilder();

            for (var current = startRange; current <= endRange; current++)
            {
                var currentText = Print(current);

                stringBuilder.Append(currentText);
                
                if (current != endRange)
                {
                    stringBuilder.Append(", ");
                }
            }

            return stringBuilder.ToString();
        }

        public string Print(int number)
        {
            if (number == 0)
            {
                return number.ToString();
            }

            return number % 2 == 0 ? EvenText : OddText;
        }
        
        private void ValidateStartEndRange(int startRange, int endRange)
        {
            if (startRange > endRange)
            {
                throw new ArgumentException();
            }
        }
    }
}