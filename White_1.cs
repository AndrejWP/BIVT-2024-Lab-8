using System;
using System.Text;

namespace Lab_8
{
    public class White_1 : White
    {
        private int _output;
        private static readonly char[] Punctuation = { '.', '!', '?', ',', ';', ':', '(', ')', '–', '—', '"' };

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

            for (int i = 0; i < Input.Length; i++)
            {
                char c = Input[i];

                // Обработка кавычек внутри слов 
                if (c == '"')
                {
                    if (i > 0 && i < Input.Length - 1 &&
                        IsWordChar(Input[i - 1]) && IsWordChar(Input[i + 1]))
                    {
                        continue; // Пропустить кавычку внутри слова
                    }
                    else
                    {
                        punctuationCount++;
                    }
                    continue;
                }

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
            // Учитываем только буквы, дефисы и апострофы
            return char.IsLetter(c) || c == '-' || c == '\'';
        }

        private bool IsPunctuation(char c)
        {
            // Проверяем, является ли символ знаком препинания
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
