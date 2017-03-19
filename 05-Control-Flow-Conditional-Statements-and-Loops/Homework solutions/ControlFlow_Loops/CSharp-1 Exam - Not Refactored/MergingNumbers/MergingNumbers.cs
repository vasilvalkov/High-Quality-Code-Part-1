using System;

class MergingNumbers
{
    static void Main()
    {
        int elemCount = int.Parse(Console.ReadLine());
        int[] arr = new int[elemCount];
        for (int i = 0; i < elemCount; i++)
        {   // Populate array
            arr[i] = int.Parse(Console.ReadLine());
        }

        for (int i = 0; i < elemCount - 1; i++)
        {
            int firstRemainder = arr[i] % 10;
            int secondRemainder = (arr[i + 1] / 10) % 10;
            Console.Write("{0}{1} ", firstRemainder, secondRemainder);
        }

        Console.WriteLine();

        for (int i = 0; i < elemCount - 1; i++)
        {            
            Console.Write("{0} ", arr[i] + arr[i + 1]);
        }
    }
}