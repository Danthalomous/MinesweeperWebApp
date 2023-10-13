using System;
using System.Collections.Generic;

namespace Milestone_Project
{
    public class Board
    {
        private Cell[,] grid;
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        public double Difficulty { get; set; }

        public Board(double difficulty)
        {
            Difficulty = difficulty;

            if (difficulty == 0.2)
            {
                Rows = 4;
                Columns = 4;
            }
            else if (difficulty == 0.5)
            {
                Rows = 8;
                Columns = 8;
            }
            else if (difficulty == 0.8)
            {
                Rows = 12;
                Columns = 12;
            }
            else
            {
                Rows = 8;
                Columns = 8;
            }

            grid = new Cell[Rows, Columns];

            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    grid[row, col] = new Cell(row, col);
                }
            }

            SetupLiveNeighbors();
            CalculateLiveNeighbors();
        }

        public void SetupLiveNeighbors()
        {
            Random random = new Random();

            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    if (random.NextDouble() < Difficulty)
                    {
                        grid[row, col].Live = true;
                    }
                }
            }
        }

        public void CalculateLiveNeighbors()
        {
            int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    if (grid[row, col].Live)
                    {
                        grid[row, col].LiveNeighbors = 9;
                        continue;
                    }

                    int liveCount = 0;

                    for (int i = 0; i < 8; i++)
                    {
                        int newRow = row + dx[i];
                        int newCol = col + dy[i];

                        if (IsValidPosition(newRow, newCol) && grid[newRow, newCol].Live)
                        {
                            liveCount++;
                        }
                    }

                    grid[row, col].LiveNeighbors = liveCount;
                }
            }
        }

        private bool IsValidPosition(int row, int col)
        {
            return row >= 0 && row < Rows && col >= 0 && col < Columns;
        }

        public Cell GetCell(int row, int col)
        {
            return grid[row, col];
        }

        public void floodFill(int row, int col)
        {
            grid[row, col].Visited = true;

            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };

            for (int i = 0; i < 4; i++)
            {
                int newRow = row + dx[i];
                int newCol = col + dy[i];

                if (IsValidPosition(newRow, newCol) && !grid[newRow, newCol].Visited && !grid[newRow, newCol].Live)
                {
                    floodFill(newRow, newCol);
                }
            }
        }
    }
}

