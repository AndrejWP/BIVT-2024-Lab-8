using System;

namespace Lab_8
{
    public abstract class White
    {
        private string _input;
        public string Input => _input;

        protected White(string input)
        {
            _input = input ?? throw new ArgumentNullException(nameof(input));
        }

        public abstract void Review();
    }
}
