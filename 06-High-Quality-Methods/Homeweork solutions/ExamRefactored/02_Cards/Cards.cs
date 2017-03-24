using System;
using System.Text;

namespace _02_Cards
{
    class Cards
    {
        static string[] cards = {
            "2c","3c","4c","5c","6c","7c","8c","9c","Tc","Jc","Qc","Kc","Ac",
            "2d","3d","4d","5d","6d","7d","8d","9d","Td","Jd","Qd","Kd","Ad",
            "2h","3h","4h","5h","6h","7h","8h","9h","Th","Jh","Qh","Kh","Ah",
            "2s","3s","4s","5s","6s","7s","8s","9s","Ts","Js","Qs","Ks","As"
        };

        static void Main()
        {
            int handsCount = int.Parse(Console.ReadLine());
            ulong[] hands = GetHands(handsCount);
            int[] cardOccurrences = new int[cards.Length];

            foreach (var hand in hands)
            {
                for (int i = 0; i <= 51; i++)
                {
                    ulong mask = 1UL << i;

                    if ((hand & mask) > 0)
                    {
                        cardOccurrences[i] += 1;
                    }
                }                
            }

            StringBuilder evenNumberCard = new StringBuilder();
            int count = 0;

            for (int i = 0; i < cardOccurrences.Length; i++)
            {
                if (cardOccurrences[i] > 0)
                {
                    count++;
                }

                if (cardOccurrences[i] % 2 == 0)
                {
                    evenNumberCard.Append(cards[i]);
                    evenNumberCard.Append(" ");
                }
            }

            string result = count >= 52 ? "Full deck" : "Wa wa!";

            Console.WriteLine(result);
            Console.WriteLine(evenNumberCard.ToString());
        }

        private static ulong[] GetHands(int handsCount)
        {
            ulong[] hands = new ulong[handsCount];

            for (int i = 0; i < handsCount; i++)
            {
                hands[i] = ulong.Parse(Console.ReadLine());
            }

            return hands;
        }
    }
}
