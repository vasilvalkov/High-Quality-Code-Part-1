using System;

class MagicalNumbers
{
    static void Main()
    {
        int inputNumber = int.Parse(Console.ReadLine());
        int inputNumberLength = 3;
        double firstDigit = default(double);
        double secondDigit = default(double);
        double thirdDigit = default(double);
        double result = default(double);
        int indexOfHundreds = 0;
        int indexOfTens = 1;
        int indexOfOnes = 2;

        for (int i = 0; i < inputNumberLength; i++)
        {
            if (i == indexOfHundreds)
            {
                thirdDigit = inputNumber % 10;
            }

            if (i == indexOfTens)
            {
                secondDigit = inputNumber % 10;
            }

            if (i == indexOfOnes)
            {
                firstDigit = inputNumber % 10;
            }

            inputNumber /= 10;
        }

        if (secondDigit % 2 == 0)
        {
            result = (firstDigit + secondDigit) * thirdDigit;
        }
        else
        {
            result = (firstDigit * secondDigit) / thirdDigit;
        }

        Console.WriteLine("{0:F2}", result);
    }
}