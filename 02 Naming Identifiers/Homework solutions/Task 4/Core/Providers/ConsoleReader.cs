using Minesweeper.Core.Contracts;
using System;

namespace Minesweeper.Core.Providers
{
    public class ConsoleReader : IReader
    {
        public int Read()
        {
            return Console.Read();
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}