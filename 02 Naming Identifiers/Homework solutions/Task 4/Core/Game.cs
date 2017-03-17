using Minesweeper.Core.Contracts;
using Minesweeper.Globals;
using Minesweeper.Models;
using Minesweeper.Models.Contracts;
using System.Collections.Generic;

namespace Minesweeper.Core
{
    public class Game
    {
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
            this.topPlayers = new List<IPlayer>();

            this.InitializeGame();
        }

        public void Play()
        {
            do
            {
                if (this.gameStarts)
                {
                    this.writer.WriteLine("Hajde da igraem na “Mini4KI”. Probvaj si kasmeta da otkriesh poleteta bez mini4ki." +
                    " Komanda 'top' pokazva klasiraneto, 'restart' po4va nova igra, 'exit' izliza i hajde 4ao!");

                    this.writer.WriteLine(this.gameBoard.Draw());

                    this.gameStarts = false;
                }

                this.writer.Write("Daj red i kolona : ");

                this.command = reader.ReadLine().Trim();

                this.HandleCommand(this.command);

                if (this.stepOnMine)
                {
                    this.writer.WriteLine(this.gameBoard.Draw());

                    this.writer.Write("\nHrrrrrr! Umria gerojski s {0} to4ki. " +
                        "Daj si niknejm: ", this.points);

                    string playerName = this.reader.ReadLine();
                    IPlayer player = new Player(playerName, this.points);
                    this.AddToTopPlayers(player);

                    this.topPlayers.Sort((firstPlayer, secondPlayer) => secondPlayer.Name.CompareTo(firstPlayer.Name));

                    this.topPlayers.Sort((firstPlayer, secondPlayer) => secondPlayer.Points.CompareTo(firstPlayer.Points));

                    this.GetScores(topPlayers);

                    this.InitializeGame(false);
                }

                if (this.allCellsWithoutMineOpened)
                {
                    this.writer.WriteLine("\nBRAVOOOS! Otvri 35 kletki bez kapka kryv.");
                    this.writer.WriteLine(this.gameBoard.Draw());
                    this.writer.WriteLine("Daj si imeto, batka: ");

                    string playerName = this.reader.ReadLine();
                    IPlayer player = new Player(playerName, points);
                    this.AddToTopPlayers(player);

                    this.GetScores(this.topPlayers);

                    this.InitializeGame(false);
                }
            } while (this.command != "exit");

            this.writer.WriteLine("Made in Bulgaria - Uauahahahahaha!");
            this.writer.WriteLine("AREEEEEEeeeeeee.");
            this.reader.Read();
        }

        private void AddToTopPlayers(IPlayer player)
        {
            if (this.topPlayers.Count < 5)
            {
                this.topPlayers.Add(player);
            }
            else
            {
                for (int i = 0; i < this.topPlayers.Count; i++)
                {
                    if (this.topPlayers[i].Points < player.Points)
                    {
                        this.topPlayers.Insert(i, player);
                        this.topPlayers.RemoveAt(this.topPlayers.Count - 1);
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
                    this.GetScores(this.topPlayers);
                    break;
                case "restart":
                    this.InitializeGame(false);
                    break;
                case "exit":
                    this.writer.WriteLine("4a0, 4a0, 4a0!");
                    break;
                case "turn":
                    this.CommitTurn();
                    break;
                default:
                    this.writer.WriteLine("\nGreshka! nevalidna Komanda\n");
                    break;
            }
        }

        private void CommitTurn()
        {
            if (this.minesPlayground[this.inputRow, this.inputColumn] != '*')
            {
                if (this.minesPlayground[this.inputRow, this.inputColumn] == '-')
                {
                    this.gameBoard.RevealNearbyMinesCount(this.inputRow, this.inputColumn);
                    this.points++;
                }

                if (Constants.MAX_POINTS == this.points)
                {
                    this.allCellsWithoutMineOpened = true;
                }
                else
                {
                    this.writer.WriteLine(this.gameBoard.Draw());
                }
            }
            else
            {
                this.stepOnMine = true;
            }
        }

        private void InitializeGame(bool gameStarts = true)
        {
            this.playground = this.gameBoard.CreatePlayground(Constants.BOARD_ROWS_COUNT, Constants.BOARD_COLUMNS_COUNT);
            this.minesPlayground = this.gameBoard.AllocateMines();
            this.points = 0;
            this.stepOnMine = false;
            this.allCellsWithoutMineOpened = false;
            this.gameStarts = gameStarts;
            this.inputRow = 0;
            this.inputColumn = 0;
        }

        private void GetScores(IList<IPlayer> players)
        {
            this.writer.WriteLine("\nTo4KI:");

            if (players.Count > 0)
            {
                for (int i = 0; i < players.Count; i++)
                {
                    this.writer.WriteLine("{0}. {1} --> {2} kutii",
                        i + 1, players[i].Name, players[i].Points);
                }

                this.writer.WriteLine();
            }
            else
            {
                this.writer.WriteLine("prazna klasaciq!\n");
            }
        }
    }
}