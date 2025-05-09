using System;
using System.Text;

namespace Lab_8
{
    public class White_2 : White
    {
        private int[,] _outputMatrix;
        private static readonly char[] Vowels = {
            'a', 'e', 'i', 'o', 'u', 'y',
            'а', 'у', 'о', 'ы', 'и', 'э', 'я', 'ю', 'ё', 'е',
            'A', 'E', 'I', 'O', 'U', 'Y',
            'А', 'У', 'О', 'Ы', 'И', 'Э', 'Я', 'Ю', 'Ё', 'Е'
        };

        public White_2(string input) : base(input)
        {
            _outputMatrix = new int[0, 2];
        }

        public int[,] Output => _outputMatrix;

        public override void Review()
        {
            if (string.IsNullOrEmpty(Input))
            {
                _outputMatrix = new int[0, 2];
                return;
            }

            string[] words = ExtractWords(Input);
            int[] syllables = new int[words.Length];

            for (int i = 0; i < words.Length; i++)
            {
                syllables[i] = CountSyllables(words[i]);
            }

            int[,] stats = new int[0, 2];
            foreach (int s in syllables)
            {
                bool found = false;
                for (int i = 0; i < stats.GetLength(0); i++)
                {
                    if (stats[i, 0] == s)
                    {
                        stats[i, 1]++;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    int[,] newStats = new int[stats.GetLength(0) + 1, 2];
                    Array.Copy(stats, newStats, stats.Length);
                    newStats[newStats.GetLength(0) - 1, 0] = s;
                    newStats[newStats.GetLength(0) - 1, 1] = 1;
                    stats = newStats;
                }
            }

            for (int i = 0; i < stats.GetLength(0); i++)
            {
                for (int j = i + 1; j < stats.GetLength(0); j++)
                {
                    if (stats[i, 0] > stats[j, 0])
                    {
                        (stats[i, 0], stats[j, 0]) = (stats[j, 0], stats[i, 0]);
                        (stats[i, 1], stats[j, 1]) = (stats[j, 1], stats[i, 1]);
                    }
                }
            }

            _outputMatrix = stats;
        }

        private string[] ExtractWords(string text)
        {
            string[] temp = new string[text.Length];
            int wordCount = 0;
            StringBuilder currentWord = new StringBuilder();
            bool inWord = false;

            foreach (char c in text)
            {
                if (IsWordChar(c))
                {
                    currentWord.Append(c);
                    inWord = true;
                }
                else
                {
                    if (inWord)
                    {
                        temp[wordCount++] = currentWord.ToString();
                        currentWord.Clear();
                        inWord = false;
                    }
                }
            }

            if (inWord)
            {
                temp[wordCount++] = currentWord.ToString();
            }

            string[] words = new string[wordCount];
            Array.Copy(temp, words, wordCount);
            return words;
        }

        private bool IsWordChar(char c)
        {
            return char.IsLetter(c) || c == '-' || c == '\'';
        }

        private int CountSyllables(string word)
        {
            int total = 0;
            string[] parts = SplitWord(word);

            foreach (string part in parts)
            {
                int count = 0;
                foreach (char c in part)
                {
                    if (Array.IndexOf(Vowels, c) >= 0) count++;
                }
                total += count > 0 ? count : 1;
            }

            return total == 0 ? 1 : total;
        }

        private string[] SplitWord(string word)
        {
            string[] temp = new string[word.Length];
            int partCount = 0;
            StringBuilder currentPart = new StringBuilder();

            foreach (char c in word)
            {
                if (c == '-' || c == '\'')
                {
                    if (currentPart.Length > 0)
                    {
                        temp[partCount++] = currentPart.ToString();
                        currentPart.Clear();
                    }
                }
                else
                {
                    currentPart.Append(c);
                }
            }

            if (currentPart.Length > 0)
            {
                temp[partCount++] = currentPart.ToString();
            }

            string[] parts = new string[partCount];
            Array.Copy(temp, parts, partCount);
            return parts;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < _outputMatrix.GetLength(0); i++)
            {
                sb.Append($"{_outputMatrix[i, 0]}–{_outputMatrix[i, 1]}");
                if (i < _outputMatrix.GetLength(0) - 1) sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
