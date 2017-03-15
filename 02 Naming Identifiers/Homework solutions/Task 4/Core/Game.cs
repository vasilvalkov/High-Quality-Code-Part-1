using Minesweeper.Core.Contracts;
using Minesweeper.Core.Providers;
using Minesweeper.Models;
using Minesweeper.Models.Contracts;
using System;
using System.Collections.Generic;

namespace Minesweeper.Core
{
    public class Game
    {
        public const int MAX_POINTS = 35;
        public const int BOARD_ROWS_COUNT = 5;
        public const int BOARD_COLUMNS_COUNT = 10;

        private static IReader reader = new ConsoleReader();
        private static IWriter writer = new ConsoleWriter();
        private static string command = string.Empty;
        private static int points = 0;
        private static bool stepOnMine = false;
        private static List<IPlayer> topPlayers = new List<IPlayer>(6);
        private static int inputRow = 0;
        private static int inputColumn = 0;
        private static bool gameStarts = true;
        private static bool allCellsWithoutMineOpened = false;
        private static IBoard gameBoard = new Board(BOARD_ROWS_COUNT, BOARD_COLUMNS_COUNT);
        private static char[,] board = gameBoard.Playground;
        private static char[,] minesBoard = gameBoard.AllocateMines();

        public Game(IBoard board, IReader inputReader, IWriter writer)
        {
            // this.gameBoard = board;
            // this.reader = inputReader;
            // this.writer = writer;
        }

        public static void Play()
        {
            do
            {
                if (gameStarts)
                {
                    writer.WriteLine("Hajde da igraem na “Mini4KI”. Probvaj si kasmeta da otkriesh poleteta bez mini4ki." +
                    " Komanda 'top' pokazva klasiraneto, 'restart' po4va nova igra, 'exit' izliza i hajde 4ao!");

                    writer.WriteLine(gameBoard.Draw());

                    gameStarts = false;
                }

                writer.Write("Daj red i kolona : ");

                command = reader.ReadLine().Trim();

                HandleCommand(command);

                if (stepOnMine)
                {
                    writer.WriteLine(gameBoard.Draw());

                    writer.Write("\nHrrrrrr! Umria gerojski s {0} to4ki. " +
                        "Daj si niknejm: ", points);

                    string playerName = reader.ReadLine();
                    IPlayer player = new Player(playerName, points);
                    AddToTopPlayers(player);

                    topPlayers.Sort((firstPlayer, secondPlayer) => secondPlayer.Name.CompareTo(firstPlayer.Name));

                    topPlayers.Sort((firstPlayer, secondPlayer) => secondPlayer.Points.CompareTo(firstPlayer.Points));

                    GetScores(topPlayers);

                    InitializeGame();
                }

                if (allCellsWithoutMineOpened)
                {
                    writer.WriteLine("\nBRAVOOOS! Otvri 35 kletki bez kapka kryv.");
                    writer.WriteLine(gameBoard.Draw());
                    writer.WriteLine("Daj si imeto, batka: ");

                    string playerName = reader.ReadLine();
                    IPlayer player = new Player(playerName, points);
                    AddToTopPlayers(player);

                    GetScores(topPlayers);

                    InitializeGame();
                }
            } while (command != "exit");

            writer.WriteLine("Made in Bulgaria - Uauahahahahaha!");
            writer.WriteLine("AREEEEEEeeeeeee.");
            reader.Read();
        }

        private static void AddToTopPlayers(IPlayer player)
        {
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
        }

        private static void HandleCommand(string command)
        {
            if (command.Length >= 3)
            {
                if (int.TryParse(command[0].ToString(), out inputRow) &&
                    int.TryParse(command[2].ToString(), out inputColumn) &&
                    inputRow <= board.GetLength(0) &&
                    inputColumn <= board.GetLength(1))
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
                    board = gameBoard.CreatePlayground(BOARD_ROWS_COUNT, BOARD_COLUMNS_COUNT);
                    minesBoard = gameBoard.AllocateMines();
                    writer.WriteLine(gameBoard.Draw());
                    stepOnMine = false;
                    gameStarts = false;
                    break;
                case "exit":
                    writer.WriteLine("4a0, 4a0, 4a0!");
                    break;
                case "turn":
                    if (minesBoard[inputRow, inputColumn] != '*')
                    {
                        if (minesBoard[inputRow, inputColumn] == '-')
                        {
                            gameBoard.RevealNearbyMinesCount(inputRow, inputColumn);
                            points++;
                        }

                        if (MAX_POINTS == points)
                        {
                            allCellsWithoutMineOpened = true;
                        }
                        else
                        {
                            writer.WriteLine(gameBoard.Draw());
                        }
                    }
                    else
                    {
                        stepOnMine = true;
                    }
                    break;
                default:
                    writer.WriteLine("\nGreshka! nevalidna Komanda\n");
                    break;
            }
        }

        private static void InitializeGame()
        {
            board = gameBoard.CreatePlayground(BOARD_ROWS_COUNT, BOARD_COLUMNS_COUNT);
            minesBoard = gameBoard.AllocateMines();
            points = 0;
            allCellsWithoutMineOpened = false;
            gameStarts = true;
        }

        private static void GetScores(IList<IPlayer> players)
        {
            writer.WriteLine("\nTo4KI:");

            if (players.Count > 0)
            {
                for (int i = 0; i < players.Count; i++)
                {
                    writer.WriteLine("{0}. {1} --> {2} kutii",
                        i + 1, players[i].Name, players[i].Points);
                }

                writer.WriteLine();
            }
            else
            {
                writer.WriteLine("prazna klasaciq!\n");
            }
        }
    }
}