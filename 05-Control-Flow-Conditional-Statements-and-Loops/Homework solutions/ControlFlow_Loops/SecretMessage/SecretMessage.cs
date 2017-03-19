using System;

class SecretMessage
{
    static void Main()
    {
        const int ODD_LINE_SYMBOL_INDEX_INCREMENT_STEP = 3;
        const int EVEN_LINE_SYMBOL_INDEX_INCREMENT_STEP = 4;
        const int INPUT_LINES_COUNT = 3;

        string secretMessage = string.Empty;
        int linesCount = 0;

        do
        {
            string startIndexOfSegmentToDecode = Console.ReadLine();

            if (startIndexOfSegmentToDecode == "end")
            {
                break;
            }

            string lastIndexOfSegmentToDecode = Console.ReadLine();

            if (lastIndexOfSegmentToDecode == "end")
            {
                break;
            }

            string text = Console.ReadLine();

            if (text == "end")
            {
                break;
            }

            linesCount += INPUT_LINES_COUNT;

            int startIndex = int.Parse(startIndexOfSegmentToDecode);

            if (startIndex < 0)
            {
                startIndex = text.Length - Math.Abs(startIndex);
            }

            int endIndex = int.Parse(lastIndexOfSegmentToDecode);

            if (endIndex < 0)
            {
                endIndex = text.Length - Math.Abs(endIndex);
            }
            
            if (linesCount % 2 == 0)
            {
                for (int i = startIndex; i <= endIndex; i += EVEN_LINE_SYMBOL_INDEX_INCREMENT_STEP)
                {
                    secretMessage += text[i];
                }
            }
            else
            {
                for (int i = startIndex; i <= endIndex; i += ODD_LINE_SYMBOL_INDEX_INCREMENT_STEP)
                {
                    secretMessage += text[i];
                }
            }
        } while (true);

        Console.WriteLine(secretMessage);
    }
}