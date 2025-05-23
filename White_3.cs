using System;
using System.Text;

namespace Lab_8
{
    public class White_3 : White
    {
        private string _output;
        private readonly string[,] _codeTable;

        public White_3(string input, string[,] codeTable) : base(input)
        {
            _codeTable = codeTable ?? throw new ArgumentNullException(nameof(codeTable));
            _output = input == null ? null : string.Empty;
        }

        public string Output => _output;

        public override void Review()
        {
            if (Input == null)
            {
                _output = null;
                return;
            }
            
            if (Input.Length == 0)
            {
                _output = string.Empty;
                return;
            }

            StringBuilder result = new StringBuilder();
            string[] tokens = ExtractTokens(Input);

            foreach (string token in tokens)
            {
                if (IsWord(token))
                {
                    string code = FindCode(token);
                    result.Append(code ?? token);
                }
                else
                {
                    result.Append(token);
                }
            }

            _output = result.ToString();
        }

        private string[] ExtractTokens(string text)
        {
            string[] tempTokens = new string[text.Length * 2];
            int tokenCount = 0;
            StringBuilder currentToken = new StringBuilder();

            foreach (char c in text)
            {
                if (IsWordChar(c))
                {
                    currentToken.Append(c);
                }
                else
                {
                    if (currentToken.Length > 0)
                    {
                        tempTokens[tokenCount++] = currentToken.ToString();
                        currentToken.Clear();
                    }
                    tempTokens[tokenCount++] = c.ToString();
                }
            }

            if (currentToken.Length > 0)
            {
                tempTokens[tokenCount++] = currentToken.ToString();
            }

            string[] tokens = new string[tokenCount];
            Array.Copy(tempTokens, tokens, tokenCount);
            return tokens;
        }

        private bool IsWord(string token)
        {
            foreach (char c in token)
            {
                if (!IsWordChar(c)) return false;
            }
            return true;
        }

        private bool IsWordChar(char c)
        {
            return char.IsLetter(c) || c == '-' || c == '\'' || c == '–';
        }

        private string FindCode(string word)
        {
            for (int i = 0; i < _codeTable.GetLength(0); i++)
            {
                if (string.Equals(_codeTable[i, 0], word, StringComparison.OrdinalIgnoreCase))
                {
                    return _codeTable[i, 1];
                }
            }
            return null;
        }

        public override string ToString()
        {
            return _output ?? string.Empty;
        }
    }
}
