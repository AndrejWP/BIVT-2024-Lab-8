using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Lab_8
{
    public class White_1 : White
    {
        private int _output;
        private static readonly char[] Punctuation = { '.', '!', '?', ',', ';', ':', '(', ')', '–', '—' };

        public White_1(string input) : base(input)
        {
            _output = 0;
        }

        public int Output => _output;
        
        public override void Review()
        {
            if (string.IsNullOrEmpty(Input))
            {
                _output = 0;
                return;
            }

            int wordCount = 0;
            int punctuationCount = 0;
            bool inWord = false;

            foreach (char c in Input)
            {
                if (IsWordChar(c))
                {
                    if (!inWord)
                    {
                        wordCount++;
                        inWord = true;
                    }
                }
                else
                {
                    inWord = false;
                    if (IsPunctuation(c))
                    {
                        punctuationCount++;
                    }
                }
            }

            _output = wordCount + punctuationCount;
        }

        private bool IsWordChar(char c)
        {
            return char.IsLetter(c) || c == '-' || c == '\'';
        }

        private bool IsPunctuation(char c)
        {
            foreach (char p in Punctuation)
            {
                if (p == c) return true;
            }
            return false;
        }

        public override string ToString()
        {
            return Output.ToString();
        }
    }
}