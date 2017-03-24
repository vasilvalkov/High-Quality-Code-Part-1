using System;
using System.Text;

namespace _04_PeshoCode
{
    class PeshoCode
    {
        static ulong sum = 0;

        static void Main()
        {
            string word = Console.ReadLine();
            int linesCount = int.Parse(Console.ReadLine());
            string text = ReadText(linesCount);
            int wordIndex = text.IndexOf(word);
            int indexOfDot = text.IndexOf('.', wordIndex);
            int indexOfQM = text.IndexOf('?', wordIndex);
            string substring = string.Empty;

            if (indexOfDot != -1 && (indexOfQM <= 0 || indexOfQM > indexOfDot))
            {
                substring = GetSubstring(text, word, wordIndex, indexOfDot);

                foreach (var letter in substring)
                {
                    if (letter != ' ' && letter != '.')
                    {
                        sum += letter;
                    }
                }
            }

            if (indexOfQM != -1 && (indexOfDot <= 0 || indexOfDot > indexOfQM))
            {
                substring = GetSubstring(text, word, wordIndex, indexOfQM);

                foreach (var letter in substring)
                {
                    if (letter != ' ' && letter != '.' && letter != '?')
                    {
                        sum += letter;
                    }
                }
            }

            Console.WriteLine(sum);
        }

        private static string ReadText(int lines)
        {
            StringBuilder text = new StringBuilder();

            for (int i = 0; i < lines; i++)
            {
                text.Append(Console.ReadLine());
            }

            return text.ToString();
        }
        private static string GetSubstring(string text, string word, int wordIndex, int index)
        {
            string sub = string.Empty;

            if (text[index] == '?')
            {
                int startIndex = wordIndex + word.Length;
                sub = text.Substring(startIndex, index - startIndex + 1);
            }

            if (text[index] == '.')
            {
                int indexOfSentenceEnd = default(int);
                bool endsWithDot = text.LastIndexOf('.', wordIndex) > text.LastIndexOf('?', wordIndex);

                if (endsWithDot)
                {
                    indexOfSentenceEnd = text.LastIndexOf('.', wordIndex);
                }
                else
                {
                    indexOfSentenceEnd = text.LastIndexOf('?', wordIndex);
                }

                sub = text.Substring(indexOfSentenceEnd + 1, wordIndex - (indexOfSentenceEnd + 1));
            }

            return sub.ToUpper();
        }
    }
}