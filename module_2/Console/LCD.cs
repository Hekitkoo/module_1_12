using System;
using System.Collections.Generic;
using System.Text;
using Console.Models;

namespace Console
{
    public class LCD
    {
        private IDictionary<char, Digit> digits = new Dictionary<char, Digit>
        {
            {'0', new Digit("._.", "|.|", "|_|")}
        };

        public string ToLCD(string number)
        {
            var chars = Pars(number);
            var stringBuilder = new StringBuilder();

            foreach (var symbol in chars)
            {
                digits.TryGetValue(symbol, out var value);
                stringBuilder.Append(value);
            }

            return stringBuilder.ToString();
        }

        private char[] Pars(string number)
        {
            return number.ToCharArray();
        }
    }
}