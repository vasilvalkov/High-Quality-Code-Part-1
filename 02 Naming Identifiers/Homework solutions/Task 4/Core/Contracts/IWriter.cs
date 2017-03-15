namespace Minesweeper.Core.Contracts
{
    public interface IWriter
    {
        void Write(string text);

        void WriteLine();

        void WriteLine(string text);

        void WriteLine(string format, params object[] arguments);
    }
}
