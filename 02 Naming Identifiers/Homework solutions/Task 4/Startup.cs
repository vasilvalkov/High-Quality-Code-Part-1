using Minesweeper.Core;
using Minesweeper.Core.Contracts;
using Minesweeper.Core.Providers;
using Minesweeper.Globals;
using Minesweeper.Models;
using Minesweeper.Models.Contracts;

namespace Minesweeper
{
    public class Startup
    {
        public static void Main()
        {
            IBoard gameBoard = new Board(Constants.BOARD_ROWS_COUNT, Constants.BOARD_COLUMNS_COUNT);
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();

            Game game = new Game(gameBoard, reader, writer);

            game.Play();
        }
    }
}