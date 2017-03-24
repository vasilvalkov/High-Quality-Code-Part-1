using System;
using System.Linq;

namespace _03_SnackyTheSnake
{
    class SnackyTheSnake
    {
        static int length = 0;
        static int rows = 0;
        static int cols = 0;
        static char[,] field;
        static int[] pos = new int[2];

        static void Main()
        {
            int[] dim = Console.ReadLine().Split('x').Select(int.Parse).ToArray();
            rows = dim[0];
            cols = dim[1];

            field = new char[rows, cols];

            FillMatrix(rows, cols);

            pos[0] = 0;
            for (int c = 0; c < cols; c++)
            {
                if (field[0, c] == 's')
                {
                    pos[1] = c;
                    break;
                }
            }

            char[] splitChars = { ',' };
            string command = Console.ReadLine();
            string[] directions = command.Split(splitChars, StringSplitOptions.RemoveEmptyEntries);

            foreach (var direction in directions)
            {
                MoveSnake(direction);
            }

            Console.WriteLine(length);
        }

        private static void MoveSnake(string direction)
        {
            if (pos[0] >= 0 && pos[0] < rows &&
                pos[1] >= 0 && pos[1] < cols)
            {
                if (((direction == "UR" || direction == "RU") && (pos[0] == 0 || pos[1] == cols - 1)) ||
                    ((direction == "LU" || direction == "UL") && (pos[0] == 0 || pos[1] == 0)) ||
                    ((direction == "DL" || direction == "LD") && (pos[0] == rows - 1 || pos[1] == 0)) ||
                    ((direction == "DR" || direction == "RD") && (pos[0] == rows - 1 || pos[1] == cols - 1)))
                {
                    //continue;
                }
                else
                {
                    switch (direction)
                    {
                        case "d": pos[0]++; break;
                        case "u": pos[0]--; break;
                        case "l": pos[1]--; break;
                        case "r": pos[1]++; break;
                        default:
                            throw new ArgumentException("Invalid direction");
                    }
                }

                length += field[pos[0], pos[1]];
            }
        }

        private static void FillMatrix(int rows, int cols)
        {
            for (int r = 0; r < rows; r++)
            {
                char[] row = Console.ReadLine().ToCharArray();

                for (int c = 0; c < cols; c++)
                {
                    field[r, c] = row[c];
                }
            }
        }

        private static void PrintMatrix(char[,] board)
        {
            Console.Clear();

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    Console.Write("{0,2}", board[r, c]);
                }

                Console.WriteLine();
            }
        }
    }
}
