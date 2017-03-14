namespace Minesweeper.Models.Contracts
{
    public interface IPlayer
    {
        string Name { get; set; }

        int Points { get; set; }
    }
}
