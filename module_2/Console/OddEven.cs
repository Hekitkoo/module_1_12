using System;
using System.Text;

namespace Console
{
    public class OddEven
    {
        private const string EvenText = "Even";
        private const string OddText = "Odd";

        public string Print(int startRange, int endRange)
        {
            ValidateStartEndRange(startRange, endRange);

            var stringBuilder = new StringBuilder();

            for (var current = startRange; current <= endRange; current++)
            {
                if (current == 0)
                {
                    stringBuilder.Append(current);
                }
                else
                {
                    stringBuilder.Append(current % 2 == 0 ? EvenText : OddText);
                }

                if (current != endRange)
                {
                    stringBuilder.Append(", ");
                }
            }

            return stringBuilder.ToString();
        }

        public string Print(int number)
        {
            return string.Empty;
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