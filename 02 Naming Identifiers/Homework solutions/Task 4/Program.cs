using Minesweeper.Contracts;
using Minesweeper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minesweeper
{
    public class MinesGame
    {
        public static readonly int BoardRowsCount = 5;
        public static readonly int BoardColumnsCount = 10;

        private static Random random = new Random();

        static void Main(string[] аргументи)
        {
            const int MAX_POINTS = 35;
            string command = string.Empty;
            char[,] board = CreateBoard(5, 10);
            char[,] minesBoard = AllocateMines();
            int points = 0;
            bool stepOnMine = false;
            List<IPlayer> topPlayers = new List<IPlayer>(6);
            int row = 0;
            int column = 0;
            bool gameStart = true;
            bool gameEnd = false;

            do
            {
                if (gameStart)
                {
                    Console.WriteLine("Hajde da igraem na “Mini4KI”. Probvaj si kasmeta da otkriesh poleteta bez mini4ki." +
                    " Komanda 'top' pokazva klasiraneto, 'restart' po4va nova igra, 'exit' izliza i hajde 4ao!");
                    DrawBoard(board);
                    gameStart = false;
                }

                Console.Write("Daj red i kolona : ");

                command = Console.ReadLine().Trim();

                if (command.Length >= 3)
                {
                    if (int.TryParse(command[0].ToString(), out row) &&
                        int.TryParse(command[2].ToString(), out column) &&
                        row <= board.GetLength(0) &&
                        column <= board.GetLength(1))
                    {
                        command = "turn";
                    }
                }

                switch (command)
                {
                    case "top":
                        GetScores(topPlayers);
                        break;
                    case "restart":
                        board = CreateBoard(5, 10);
                        minesBoard = AllocateMines();
                        DrawBoard(board);
                        stepOnMine = false;
                        gameStart = false;
                        break;
                    case "exit":
                        Console.WriteLine("4a0, 4a0, 4a0!");
                        break;
                    case "turn":
                        if (minesBoard[row, column] != '*')
                        {
                            if (minesBoard[row, column] == '-')
                            {
                                RevealNearbyMinesCount(board, minesBoard, row, column);
                                points++;
                            }

                            if (MAX_POINTS == points)
                            {
                                gameEnd = true;
                            }
                            else
                            {
                                DrawBoard(board);
                            }
                        }
                        else
                        {
                            stepOnMine = true;
                        }
                        break;
                    default:
                        Console.WriteLine("\nGreshka! nevalidna Komanda\n");
                        break;
                }

                if (stepOnMine)
                {
                    DrawBoard(minesBoard);

                    Console.Write("\nHrrrrrr! Umria gerojski s {0} to4ki. " +
                        "Daj si niknejm: ", points);

                    string playerName = Console.ReadLine();

                    Player player = new Player(playerName, points);

                    if (topPlayers.Count < 5)
                    {
                        topPlayers.Add(player);
                    }
                    else
                    {
                        for (int i = 0; i < topPlayers.Count; i++)
                        {
                            if (topPlayers[i].Points < player.Points)
                            {
                                topPlayers.Insert(i, player);
                                topPlayers.RemoveAt(topPlayers.Count - 1);
                                break;
                            }
                        }
                    }

                    topPlayers.Sort((firstPlayer, secondPlayer) => secondPlayer.Name.CompareTo(firstPlayer.Name));

                    topPlayers.Sort((firstPlayer, secondPlayer) => secondPlayer.Points.CompareTo(firstPlayer.Points));

                    GetScores(topPlayers);

                    board = CreateBoard(5, 10);
                    minesBoard = AllocateMines();
                    points = 0;
                    stepOnMine = false;
                    gameStart = true;
                }
                if (gameEnd)
                {
                    Console.WriteLine("\nBRAVOOOS! Otvri 35 kletki bez kapka kryv.");
                    DrawBoard(minesBoard);
                    Console.WriteLine("Daj si imeto, batka: ");
                    string playerName = Console.ReadLine();
                    Player player = new Player(playerName, points);
                    topPlayers.Add(player);
                    GetScores(topPlayers);
                    board = CreateBoard(5, 10);
                    minesBoard = AllocateMines();
                    points = 0;
                    gameEnd = false;
                    gameStart = true;
                }
            } while (command != "exit");

            Console.WriteLine("Made in Bulgaria - Uauahahahahaha!");
            Console.WriteLine("AREEEEEEeeeeeee.");
            Console.Read();
        }

        private static void GetScores(IList<IPlayer> players)
        {
            Console.WriteLine("\nTo4KI:");

            if (players.Count > 0)
            {
                for (int i = 0; i < players.Count; i++)
                {
                    Console.WriteLine("{0}. {1} --> {2} kutii",
                        i + 1, players[i].Name, players[i].Points);
                }

                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("prazna klasaciq!\n");
            }
        }

        private static void RevealNearbyMinesCount(char[,] board, char[,] minesBoard, int row, int col)
        {
            char nearbyMinesCount = CountNearbyMines(minesBoard, row, col);
            minesBoard[row, col] = nearbyMinesCount;
            board[row, col] = nearbyMinesCount;
        }

        private static void DrawBoard(char[,] board)
        {
            int rowsCount = board.GetLength(0);
            int colsCount = board.GetLength(1);

            Console.WriteLine("\n    0 1 2 3 4 5 6 7 8 9");
            Console.WriteLine("   ---------------------");

            for (int row = 0; row < rowsCount; row++)
            {
                Console.Write("{0} | ", row);

                for (int col = 0; col < colsCount; col++)
                {
                    Console.Write(string.Format("{0} ", board[row, col]));
                }

                Console.Write("|");
                Console.WriteLine();
            }

            Console.WriteLine("   ---------------------\n");
        }

        private static char[,] CreateBoard(int boardRows, int boardColumns)
        {
            char[,] board = new char[boardRows, boardColumns];

            for (int row = 0; row < boardRows; row++)
            {
                for (int col = 0; col < boardColumns; col++)
                {
                    board[row, col] = '?';
                }
            }

            return board;
        }

        private static char[,] AllocateMines()
        {
            char[,] board = new char[BoardRowsCount, BoardColumnsCount];

            for (int row = 0; row < BoardRowsCount; row++)
            {
                for (int col = 0; col < BoardColumnsCount; col++)
                {
                    board[row, col] = '-';
                }
            }

            List<int> mines = new List<int>();
            

            while (mines.Count < 15)
            {
                int minePosition = random.Next(50);

                if (!mines.Contains(minePosition))
                {
                    mines.Add(minePosition);
                }
            }

            foreach (int mine in mines)
            {
                int row = (mine / BoardColumnsCount);
                int column = (mine % BoardColumnsCount);

                if (column == 0 && mine != 0)
                {
                    row--;
                    column = BoardColumnsCount;
                }
                else
                {
                    column++;
                }

                board[row, column - 1] = '*';
            }

            return board;
        }

        // Nothing calls this method?
        //private static void smetki(char[,] board)
        //{
        //    int rows = board.GetLength(0);
        //    int cols = board.GetLength(1);

        //    for (int row = 0; row < rows; row++)
        //    {
        //        for (int col = 0; col < cols; col++)
        //        {
        //            if (board[row, col] != '*')
        //            {
        //                char nearbyMinesCount = CountNearbyMines(board, row, col);
        //                board[row, col] = nearbyMinesCount;
        //            }
        //        }
        //    }
        //}

        private static char CountNearbyMines(char[,] board, int row, int col)
        {
            int minesCount = 0;
            int boardRowsCount = board.GetLength(0);
            int boardColumnsCount = board.GetLength(1);

            if (row - 1 >= 0)
            {
                if (board[row - 1, col] == '*')
                {
                    minesCount++;
                }
            }

            if (row + 1 < boardRowsCount)
            {
                if (board[row + 1, col] == '*')
                {
                    minesCount++;
                }
            }

            if (col - 1 >= 0)
            {
                if (board[row, col - 1] == '*')
                {
                    minesCount++;
                }
            }

            if (col + 1 < boardColumnsCount)
            {
                if (board[row, col + 1] == '*')
                {
                    minesCount++;
                }
            }

            if ((row - 1 >= 0) && (col - 1 >= 0))
            {
                if (board[row - 1, col - 1] == '*')
                {
                    minesCount++;
                }
            }

            if ((row - 1 >= 0) && (col + 1 < boardColumnsCount))
            {
                if (board[row - 1, col + 1] == '*')
                {
                    minesCount++;
                }
            }

            if ((row + 1 < boardRowsCount) && (col - 1 >= 0))
            {
                if (board[row + 1, col - 1] == '*')
                {
                    minesCount++;
                }
            }

            if ((row + 1 < boardRowsCount) && (col + 1 < boardColumnsCount))
            {
                if (board[row + 1, col + 1] == '*')
                {
                    minesCount++;
                }
            }

            return char.Parse(minesCount.ToString());
        }
    }
}
