using System.Collections.Generic;
using System.Text;
using Console.Models;

namespace Console
{
    public class LCD
    {
        private static readonly IDictionary<int, string> Bits = new Dictionary<int, string>
        {
            {0, "._."},
            {1, "..."},
            {2, "|.|"},
            {3, "..|"},
            {4, "._|"},
            {5, "|_|"},
            {6, "|_."},
            {7, "..|"}
        };
        
        private readonly IDictionary<char, Digit> digits = new Dictionary<char, Digit>
        {
            {'0', new Digit(Bits[0], Bits[2], Bits[5])},
            {'1', new Digit(Bits[1], Bits[3], Bits[3])},
            {'2', new Digit(Bits[0], Bits[4], Bits[6])},
            {'3', new Digit(Bits[0], Bits[4], Bits[4])},
            {'4', new Digit(Bits[1], Bits[5], Bits[3])},
            {'5', new Digit(Bits[0], Bits[6], Bits[4])},
            {'6', new Digit(Bits[0], Bits[6], Bits[5])},
            {'7', new Digit(Bits[0], Bits[7], Bits[7])},
            {'8', new Digit(Bits[0], Bits[5], Bits[5])},
            {'9', new Digit(Bits[0], Bits[5], Bits[7])}
        };

        public string ToLCD(string number)
        {
            var chars = Parse(number);
            var stringBuilder = new StringBuilder();

            for (var index = 0; index <= chars.Length - 1; index++)
            {
                digits.TryGetValue(chars[index], out var value);
                stringBuilder.Append(value);

                if (chars.Length > 1 && index != chars.Length - 1)
                {
                    stringBuilder.AppendLine();
                }
            }

            return stringBuilder.ToString();
        }

        private char[] Parse(string number)
        {
            return number.ToCharArray();
        }
    }
}