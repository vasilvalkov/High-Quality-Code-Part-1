using System;

namespace Methods
{
    class Methods
    {
        static double CalcTriangleArea(double a, double b, double c)
        {
            if (a <= 0 || b <= 0 || c <= 0)
            {
                throw new ArgumentException("Sides should be positive.");
            }

            double semiperimeter = (a + b + c) / 2;
            double area = Math.Sqrt(semiperimeter * (semiperimeter - a) * (semiperimeter - b) * (semiperimeter - c));

            return area;
        }

        static string DigitToWord(int digit)
        {
            switch (digit)
            {
                case 0: return "zero";
                case 1: return "one";
                case 2: return "two";
                case 3: return "three";
                case 4: return "four";
                case 5: return "five";
                case 6: return "six";
                case 7: return "seven";
                case 8: return "eight";
                case 9: return "nine";
                default:
                    throw new ArgumentException("Parameter must be a single positive digit.");
            }
        }

        static int FindMax(params int[] elements)
        {
            if (elements == null || elements.Length == 0)
            {
                throw new ArgumentException("No numbers provided as parameters.");
            }

            int max = int.MinValue;
            for (int i = 1; i < elements.Length; i++)
            {
                if (elements[i] > max)
                {
                    max = elements[i];
                }
            }

            return max;
        }

        static string FormatNumber(double number, string format)
        {
            switch (format)
            {
                case "f": return string.Format("{0:f2}", number);
                case "%": return string.Format("{0:p0}", number);
                case "r": return string.Format("{0,8}", number);
                default:
                    throw new ArgumentException("Format is not supported.");
            }
        }

        static double CalcDistanceBetweenTwoPoints(double x1, double y1, double x2, double y2)
        {
            double distance = Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));

            return distance;
        }

        static bool IsHorizontal(double x1, double y1, double x2, double y2)
        {
            return y1 == y2;          
        }

        static bool IsVertical(double x1, double y1, double x2, double y2)
        {
            return x1 == x2;
        }

        static void Main()
        {
            Console.WriteLine(CalcTriangleArea(3, 4, 5));

            Console.WriteLine(DigitToWord(5));

            Console.WriteLine(FindMax(5, -1, 3, 2, 14, 2, 3));

            Console.WriteLine(FormatNumber(1.3, "f"));
            Console.WriteLine(FormatNumber(0.75, "%"));
            Console.WriteLine(FormatNumber(2.30, "r"));

            double distance = CalcDistanceBetweenTwoPoints(3, -1, 3, 2.5);
            bool horizontal = IsHorizontal(3, -1, 3, 2.5);
            bool vertical = IsVertical(3, -1, 3, 2.5);

            Console.WriteLine(distance);
            Console.WriteLine("Horizontal? " + horizontal);
            Console.WriteLine("Vertical? " + vertical);

            DateTime peterBirthDate = DateTime.Parse("17.03.1992");
            Student peter = new Student(peterBirthDate) { FirstName = "Peter", LastName = "Ivanov" };
            peter.OtherInfo = "From Sofia";

            DateTime stellaBirthDate = DateTime.Parse("03.11.1993");
            Student stella = new Student(stellaBirthDate) { FirstName = "Stella", LastName = "Markova" };
            stella.OtherInfo = "From Vidin, gamer, high results";
            
            Console.WriteLine("{0} older than {1} -> {2}",
                peter.FirstName, stella.FirstName, peter.BirthDate > stella.BirthDate);
        }
    }
}
