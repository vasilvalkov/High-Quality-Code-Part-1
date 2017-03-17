using Minesweeper.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minesweeper.Models
{
    public class Board : IBoard
    {
        public readonly int BoardRowsCount;
        public readonly int BoardColumnsCount;
        private char[,] playground;
        private char[,] minesPlayground;
        private Random random = new Random();

        public Board(int rowsCount = 5, int columnsCount = 10)
        {
            this.BoardRowsCount = rowsCount;
            this.BoardColumnsCount = columnsCount;

            this.playground = this.CreatePlayground(this.BoardRowsCount, this.BoardColumnsCount);
            this.minesPlayground = this.AllocateMines();
        }

        public char[,] Playground
        {
            get
            {
                return this.playground;
            }
        }
        
        public char[,] CreatePlayground(int boardRows, int boardColumns)
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

        public char[,] AllocateMines()
        {
            char[,] board = new char[this.BoardRowsCount, this.BoardColumnsCount];

            for (int row = 0; row < this.BoardRowsCount; row++)
            {
                for (int col = 0; col < this.BoardColumnsCount; col++)
                {
                    board[row, col] = '-';
                }
            }

            List<int> mines = new List<int>();

            while (mines.Count < 15)
            {
                int minePosition = this.random.Next(50);

                if (!mines.Contains(minePosition))
                {
                    mines.Add(minePosition);
                }
            }

            foreach (int mine in mines)
            {
                int row = (mine / this.BoardColumnsCount);
                int column = (mine % this.BoardColumnsCount);

                if (column == 0 && mine != 0)
                {
                    row--;
                    column = this.BoardColumnsCount;
                }
                else
                {
                    column++;
                }

                board[row, column - 1] = '*';
            }

            return board;
        }

        public void RevealNearbyMinesCount(int row, int col)
        {
            char nearbyMinesCount = this.CountNearbyMines(row, col);
            this.minesPlayground[row, col] = nearbyMinesCount;
            this.playground[row, col] = nearbyMinesCount;
        }

        public string Draw()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("\n    0 1 2 3 4 5 6 7 8 9");
            builder.AppendLine("   ---------------------");

            for (int row = 0; row < this.BoardRowsCount; row++)
            {
                builder.AppendFormat("{0} | ", row);

                for (int col = 0; col < this.BoardColumnsCount; col++)
                {
                    builder.Append(string.Format("{0} ", this.Playground[row, col]));
                }

                builder.Append("|");
                builder.AppendLine();
            }

            builder.AppendLine("   ---------------------\n");

            return builder.ToString();
        }

        private char CountNearbyMines(int row, int col)
        {
            int minesCount = 0;

            if (row - 1 >= 0)
            {
                if (this.minesPlayground[row - 1, col] == '*')
                {
                    minesCount++;
                }
            }

            if (row + 1 < this.BoardRowsCount)
            {
                if (this.minesPlayground[row + 1, col] == '*')
                {
                    minesCount++;
                }
            }

            if (col - 1 >= 0)
            {
                if (this.minesPlayground[row, col - 1] == '*')
                {
                    minesCount++;
                }
            }

            if (col + 1 < this.BoardColumnsCount)
            {
                if (this.minesPlayground[row, col + 1] == '*')
                {
                    minesCount++;
                }
            }

            if ((row - 1 >= 0) && (col - 1 >= 0))
            {
                if (this.minesPlayground[row - 1, col - 1] == '*')
                {
                    minesCount++;
                }
            }

            if ((row - 1 >= 0) && (col + 1 < this.BoardColumnsCount))
            {
                if (this.minesPlayground[row - 1, col + 1] == '*')
                {
                    minesCount++;
                }
            }

            if ((row + 1 < this.BoardRowsCount) && (col - 1 >= 0))
            {
                if (this.minesPlayground[row + 1, col - 1] == '*')
                {
                    minesCount++;
                }
            }

            if ((row + 1 < this.BoardRowsCount) && (col + 1 < this.BoardColumnsCount))
            {
                if (this.minesPlayground[row + 1, col + 1] == '*')
                {
                    minesCount++;
                }
            }

            return char.Parse(minesCount.ToString());
        }
    }
}
