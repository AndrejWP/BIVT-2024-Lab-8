
using System;
namespace Lab_8 {
    public class White_4 : White
    {
        private int _output;

        public White_4(string input) : base(input)
        {
            _output = 0;
        }

        public int Output => _output;

        public override void Review()
        {
            _output = 0;
            if (string.IsNullOrEmpty(Input))
                return;

            foreach (char c in Input)
            {
                if (c >= '0' && c <= '9')
                    _output += c - '0';
            }
        }

        public override string ToString()
        {
            return _output.ToString();
        }
    }
}