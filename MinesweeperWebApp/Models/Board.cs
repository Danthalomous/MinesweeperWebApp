using System;

namespace Milestone_Project
{
    public class Board
    {
        public Cell[,] Grid { get; private set; }  // Make grid publicly accessible as Grid
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

            Grid = new Cell[Rows, Columns];

            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    Grid[row, col] = new Cell(row, col);
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
                        Grid[row, col].Live = true;
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
                    if (Grid[row, col].Live)
                    {
                        Grid[row, col].LiveNeighbors = 9;
                        continue;
                    }

                    int liveCount = 0;

                    for (int i = 0; i < 8; i++)
                    {
                        int newRow = row + dx[i];
                        int newCol = col + dy[i];

                        if (IsValidPosition(newRow, newCol) && Grid[newRow, newCol].Live)
                        {
                            liveCount++;
                        }
                    }

                    Grid[row, col].LiveNeighbors = liveCount;
                }
            }
        }

        private bool IsValidPosition(int row, int col)
        {
            return row >= 0 && row < Rows && col >= 0 && col < Columns;
        }

        public Cell GetCell(int row, int col)
        {
            return Grid[row, col];
        }

        public void floodFill(int row, int col)
        {
            Grid[row, col].Visited = true;

            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };

            for (int i = 0; i < 4; i++)
            {
                int newRow = row + dx[i];
                int newCol = col + dy[i];

                if (IsValidPosition(newRow, newCol) && !Grid[newRow, newCol].Visited && !Grid[newRow, newCol].Live)
                {
                    floodFill(newRow, newCol);
                }
            }
        }

        public void ProcessClick(int row, int col)
        {
            Cell cell = Grid[row, col];

            // If it's a mine
            if (cell.Live)
            {
                // TODO: Handle game over or other logic
            }
            else if (cell.LiveNeighbors == 0)
            {
                floodFill(row, col);
            }
            else
            {
                cell.Visited = true;  // Mark the cell as visited
            }

            // TODO: Add more logic as needed
        }
    }
}


