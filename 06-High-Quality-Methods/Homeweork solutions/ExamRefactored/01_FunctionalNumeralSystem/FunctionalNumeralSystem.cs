using System;
using System.Numerics;

namespace _01_FunctionalNumeralSystem
{
    class FunctionalNumeralSystem
    {
        static string[] digits = {"standardml", "commonlisp", "mercury", "clojure", "haskell", "erlang", "scala", "ocaml", "racket", "scheme", "curry"};

        static string hexDigits = "0123456789ABCDEF";

        static ulong HexToDec(string number)
        {
            ulong decValue = default(ulong);
            ulong hexBase = 16UL;

            foreach (char digit in number)
            {
                int hexValue = hexDigits.IndexOf(digit);
                decValue = decValue * hexBase + (ulong)hexValue;
            }

            return decValue;
        }
        static BigInteger product = BigInteger.One;

        static void Main()
        {
            string input = Console.ReadLine();
            string[] numbers = input.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var number in numbers)
            {
                string hexNumber = number;

                foreach (var digit in digits)
                {
                    if (number.IndexOf(digit) >= 0)
                    {
                        string digitIn15 = string.Empty;

                        switch (digit)
                        {
                            case "standardml": digitIn15 = "9"; break;
                            case "commonlisp": digitIn15 = "D"; break;
                            case "mercury": digitIn15 = "C"; break;
                            case "clojure": digitIn15 = "7"; break;
                            case "haskell": digitIn15 = "1"; break;
                            case "erlang": digitIn15 = "8"; break;
                            case "scala": digitIn15 = "2"; break;
                            case "ocaml": digitIn15 = "0"; break;
                            case "racket": digitIn15 = "A"; break;
                            case "scheme": digitIn15 = "E"; break;
                            case "curry": digitIn15 = "F"; break;
                            case "f#": digitIn15 = "3"; break;
                            case "lisp": digitIn15 = "4"; break;
                            case "rust": digitIn15 = "5"; break;
                            case "ml": digitIn15 = "6"; break;
                            case "elm": digitIn15 = "B"; break;
                            default:
                                throw new ArgumentException("Incorrect digit!");
                        }

                        hexNumber = hexNumber.Replace(digit, digitIn15);
                    }
                }

                ulong decNumber = HexToDec(hexNumber);
                product *= decNumber;
            }

            Console.WriteLine(product);
        }
    }
}
