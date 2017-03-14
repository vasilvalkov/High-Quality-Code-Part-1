﻿using Minesweeper.Contracts;
using Minesweeper.Models;
using System;
using System.Collections.Generic;

namespace Minesweeper.Core
{
    public class Game
    {
        public const int MAX_POINTS = 35;
        public const int BOARD_ROWS_COUNT = 5;
        public const int BOARD_COLUMNS_COUNT = 10;

        private static string command = string.Empty;
        private static int points = 0;
        private static bool stepOnMine = false;
        private static List<IPlayer> topPlayers = new List<IPlayer>(6);
        private static int inputRow = 0;
        private static int inputColumn = 0;
        private static bool gameStart = true;
        private static bool allCellsWithoutMineOpened = false;
        private static Board gameBoard = new Board(BOARD_ROWS_COUNT, BOARD_COLUMNS_COUNT);
        private static char[,] board = gameBoard.Playground;
        private static char[,] minesBoard = gameBoard.AllocateMines();

        public static void Play()
        {
            do
            {
                if (gameStart)
                {
                    Console.WriteLine("Hajde da igraem na “Mini4KI”. Probvaj si kasmeta da otkriesh poleteta bez mini4ki." +
                    " Komanda 'top' pokazva klasiraneto, 'restart' po4va nova igra, 'exit' izliza i hajde 4ao!");

                    Console.WriteLine(gameBoard.Draw());

                    gameStart = false;
                }

                Console.Write("Daj red i kolona : ");

                command = Console.ReadLine().Trim();

                HandleCommand(command);

                if (stepOnMine)
                {
                    Console.WriteLine(gameBoard.Draw());

                    Console.Write("\nHrrrrrr! Umria gerojski s {0} to4ki. " +
                        "Daj si niknejm: ", points);

                    string playerName = Console.ReadLine();
                    IPlayer player = new Player(playerName, points);
                    AddToTopPlayers(player);

                    topPlayers.Sort((firstPlayer, secondPlayer) => secondPlayer.Name.CompareTo(firstPlayer.Name));

                    topPlayers.Sort((firstPlayer, secondPlayer) => secondPlayer.Points.CompareTo(firstPlayer.Points));

                    GetScores(topPlayers);

                    InitializeGame();
                }

                if (allCellsWithoutMineOpened)
                {
                    Console.WriteLine("\nBRAVOOOS! Otvri 35 kletki bez kapka kryv.");
                    Console.WriteLine(gameBoard.Draw());
                    Console.WriteLine("Daj si imeto, batka: ");

                    string playerName = Console.ReadLine();
                    IPlayer player = new Player(playerName, points);
                    AddToTopPlayers(player);

                    GetScores(topPlayers);

                    InitializeGame();
                }
            } while (command != "exit");

            Console.WriteLine("Made in Bulgaria - Uauahahahahaha!");
            Console.WriteLine("AREEEEEEeeeeeee.");
            Console.Read();
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
                    Console.WriteLine(gameBoard.Draw());
                    stepOnMine = false;
                    gameStart = false;
                    break;
                case "exit":
                    Console.WriteLine("4a0, 4a0, 4a0!");
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
                            Console.WriteLine(gameBoard.Draw());
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
        }

        private static void InitializeGame()
        {
            board = gameBoard.CreatePlayground(BOARD_ROWS_COUNT, BOARD_COLUMNS_COUNT);
            minesBoard = gameBoard.AllocateMines();
            points = 0;
            allCellsWithoutMineOpened = false;
            gameStart = true;
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
    }
}