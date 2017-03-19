using System;

class MagicalNumbers
{
    static void Main()
    {
        int input = int.Parse(Console.ReadLine());
        double first = -1;
        double second = -1;
        double third = -1;
        double result;

        for (int i = 0; i < 3; i++, input /= 10)
        {
            if (i == 0)
            {
                third = input % 10;
            }
            if (i == 1)
            {
                second = input % 10;
            }
            if (i == 2)
            {
                first = input % 10;
            }                
        }

        if (second % 2 == 0)
        {
            result = (first + second) * third;
        }
        else
        {
            result = (first * second) / third;
        }

        Console.WriteLine("{0:F2}", result);
    }
}