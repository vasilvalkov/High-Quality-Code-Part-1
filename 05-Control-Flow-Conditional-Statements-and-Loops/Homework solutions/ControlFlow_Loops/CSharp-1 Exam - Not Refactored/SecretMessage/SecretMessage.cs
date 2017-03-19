using System;

class SecretMessage
{
    static void Main()
    {
        string s;
        string e;
        string text;
        string message = "";
        int linesCount = 0;
        int indexS;
        int indexE;

        do
        {
            s = Console.ReadLine();
            if (s == "end") break;
            e = Console.ReadLine();
            if (e == "end") break;
            text = Console.ReadLine();
            if (text == "end") break;

            linesCount += 3;

            indexS = int.Parse(s);
            indexE = int.Parse(e);

            if (indexS < 0)
            {
                indexS = text.Length - Math.Abs(indexS);
            }
            if (indexE < 0)
            {
                indexE = text.Length - Math.Abs(indexE);
            }
            if (linesCount % 2 == 0)
            {
                for (int i = indexS; i <= indexE; i += 4)
                {
                    message += text[i];
                }
            }
            else
            {
                for (int i = indexS; i <= indexE; i += 3)
                {
                    message += text[i];
                }
            }
        } while (true);

        Console.WriteLine(message);
    }
}