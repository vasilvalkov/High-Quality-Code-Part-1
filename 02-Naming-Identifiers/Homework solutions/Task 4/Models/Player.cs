using Minesweeper.Models.Contracts;

namespace Minesweeper.Models
{
    public class Player : IPlayer
    {
        string name;
        int points;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Points
        {
            get { return points; }
            set { points = value; }
        }

        public Player()
        {
        }

        public Player(string name, int points)
        {
            this.Name = name;
            this.Points = points;
        }
    }
}
