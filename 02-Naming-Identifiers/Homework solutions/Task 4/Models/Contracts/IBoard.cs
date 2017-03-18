namespace Minesweeper.Models.Contracts
{
    public interface IBoard
    {
        char[,] Playground { get; }

        char[,] CreatePlayground(int boardRows, int boardColumns);

        char[,] AllocateMines();

        void RevealNearbyMinesCount(int row, int col);

        string Draw();
    }
}
