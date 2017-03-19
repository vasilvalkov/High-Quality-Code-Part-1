using System;

class MergingNumbers
{
    static void Main()
    {
        int elemCount = int.Parse(Console.ReadLine());
        int[] numbers = new int[elemCount];

        for (int i = 0; i < elemCount; i++)
        {
            numbers[i] = int.Parse(Console.ReadLine());
        }

        for (int i = 0; i < elemCount - 1; i++)
        {
            int secondDigitOfFisrtNumber = numbers[i] % 10;
            int firstDigitOfSecondNumber = (numbers[i + 1] / 10) % 10;

            string mergedNumber = string.Format("{0}{1} ", secondDigitOfFisrtNumber, firstDigitOfSecondNumber);

            Console.Write(mergedNumber);
        }

        Console.WriteLine();

        for (int i = 0; i < elemCount - 1; i++)
        {
            int summedNumber = numbers[i] + numbers[i + 1];

            Console.Write("{0} ", summedNumber);
        }
    }
}