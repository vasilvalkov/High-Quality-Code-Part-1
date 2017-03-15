using Minesweeper.Core.Contracts;
using System;

namespace Minesweeper.Core.Providers
{
    public class ConsoleWriter : IWriter
    {
        public void Write(string text)
        {
            Console.Write(text);
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }

        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }

        public void WriteLine(string format,  params object[] arguments)
        {
            Console.WriteLine(format, arguments);
        }
    }
}
