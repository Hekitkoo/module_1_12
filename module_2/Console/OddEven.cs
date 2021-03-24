using System;
using System.Text;

namespace Console
{
    public class OddEven
    {
        private const string EvenText = "Even";

        public string Print(int startRange, int endRange)
        {
            ValidateStartEndRange(startRange, endRange);

            var stringBuilder = new StringBuilder();

            for (var current = startRange; current <= endRange; current++)
            {
                if (current % 2 == 0)
                {
                    stringBuilder.Append(EvenText);
                }
                else
                {
                    stringBuilder.Append(current);
                }

                if (current != endRange)
                {
                    stringBuilder.Append(", ");
                }
            }

            return stringBuilder.ToString();
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