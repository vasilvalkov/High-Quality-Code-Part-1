using System;

class GoingToParty
{
    static void Main()
    {
        const int CHAR_a_ASCII = 97;
        const int CHAR_z_ASCII = 122;
        const int CHAR_A_ASCII = 65;
        const int CHAR_Z_ASCII = 90;
        const int CHAR_BACKTICK_ASCII = 96;
        const int CHAR_0_ASCII = 64;
        const int CHAR_PARTY_SYMBOL = 94;

        string directions = Console.ReadLine();
        int position = 0;        
        int len = directions.Length;
        int step = 0;
        

        while (0 <= position && position < len)
        {
            step = directions[position];

            if (CHAR_a_ASCII <= step && step <= CHAR_z_ASCII)
            {                
                position += step - CHAR_BACKTICK_ASCII;
            }

            if (CHAR_A_ASCII <= step && step <= CHAR_Z_ASCII)
            {                
                position -= step - CHAR_0_ASCII;
            }

            if (step == CHAR_PARTY_SYMBOL)
            {
                break;
            }
        }

        if (0 <= position && position < len)
        {
            Console.WriteLine("Djor and Djano are at the party at {0}!", position);
        }
        else
        {
            Console.WriteLine("Djor and Djano are lost at {0}!", position);
        }
    }
}