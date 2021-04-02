using System;

namespace Console.Models
{
    public class Digit
    {
        private readonly string top;
        private readonly string middle;
        private readonly string bottom;

        public Digit(string top, string middle, string bottom)
        {
            this.top = top;
            this.middle = middle;
            this.bottom = bottom;
        }

        public override string ToString()
        {
            return top + Environment.NewLine + middle + Environment.NewLine + bottom;
        }
    }
}