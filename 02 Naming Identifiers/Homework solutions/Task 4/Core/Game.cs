using Minesweeper.Core.Contracts;
using Minesweeper.Models;
using Minesweeper.Models.Contracts;
using System.Collections.Generic;

namespace Minesweeper.Core
{
    public class Game
    {
        public const int MAX_POINTS = 35;
        public const int BOARD_ROWS_COUNT = 5;
        public const int BOARD_COLUMNS_COUNT = 10;

        private IBoard gameBoard;
        private IReader reader;
        private IWriter writer;
        private string command;
        private int points;
        private bool stepOnMine;
        private List<IPlayer> topPlayers;
        private int inputRow;
        private int inputColumn;
        private bool gameStarts;
        private bool allCellsWithoutMineOpened;
        private char[,] playground;
        private char[,] minesPlayground;

        public Game(IBoard board, IReader inputReader, IWriter writer)
        {
            this.gameBoard = board;
            this.reader = inputReader;
            this.writer = writer;
            this.command = string.Empty;
            this.points = 0;
            this.stepOnMine = false;
            this.topPlayers = new List<IPlayer>();
            this.inputRow = 0;
            this.inputColumn = 0;
            this.gameStarts = true;
            this.allCellsWithoutMineOpened = false;
            this.playground = gameBoard.Playground;
            this.minesPlayground = gameBoard.AllocateMines();
        }

        public void Play()
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

        private void AddToTopPlayers(IPlayer player)
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

        private void HandleCommand(string command)
        {
            if (command.Length >= 3)
            {
                if (int.TryParse(command[0].ToString(), out inputRow) &&
                    int.TryParse(command[2].ToString(), out inputColumn) &&
                    inputRow <= playground.GetLength(0) &&
                    inputColumn <= playground.GetLength(1))
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
                    playground = gameBoard.CreatePlayground(BOARD_ROWS_COUNT, BOARD_COLUMNS_COUNT);
                    minesPlayground = gameBoard.AllocateMines();
                    writer.WriteLine(gameBoard.Draw());
                    stepOnMine = false;
                    gameStarts = false;
                    break;
                case "exit":
                    writer.WriteLine("4a0, 4a0, 4a0!");
                    break;
                case "turn":
                    if (minesPlayground[inputRow, inputColumn] != '*')
                    {
                        if (minesPlayground[inputRow, inputColumn] == '-')
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

        private void InitializeGame()
        {
            playground = gameBoard.CreatePlayground(BOARD_ROWS_COUNT, BOARD_COLUMNS_COUNT);
            minesPlayground = gameBoard.AllocateMines();
            points = 0;
            allCellsWithoutMineOpened = false;
            gameStarts = true;
        }

        private void GetScores(IList<IPlayer> players)
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