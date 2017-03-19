using System;

class GoingToParty
{
    static void Main()
    {
        string input = Console.ReadLine();
        int pos = 0;        
        int len = input.Length;
        int step = 0;

        while (0 <= pos && pos < len)
        {
            step = input[pos];

            if (97 <= step && step <= 122)
            {                
                pos += step - 96;
            }
            if (65 <= step && step <= 90)
            {                
                pos -= step - 64;
            }
            if (step == 94)
            {
                break;
            }
        }

        if (pos >= 0 && pos < len)
        {
            Console.WriteLine("Djor and Djano are at the party at {0}!", pos);
        }
        else
        {
            Console.WriteLine("Djor and Djano are lost at {0}!", pos);
        }
    }
}