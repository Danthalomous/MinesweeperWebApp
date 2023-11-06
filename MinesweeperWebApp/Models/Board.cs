using System;

namespace Milestone_Project
{
    public class Board
    {
        public enum Difficulty { Easy, Medium, Hard }

        public int Rows { get; private set; }
        public int Columns { get; private set; }
        public Cell[,] Cells { get; private set; }
        public int MineCount { get; private set; }
        public int Lives { get; private set; }

        private static readonly Dictionary<Difficulty, (int rows, int cols, int mines)> DifficultySettings =
            new Dictionary<Difficulty, (int, int, int)>
            {
            { Difficulty.Easy, (8, 8, 10) },
            { Difficulty.Medium, (16, 16, 40) },
            { Difficulty.Hard, (24, 24, 99) }
            };

        public Board(Difficulty difficulty)
        {
            Lives = 3; // Initialize lives
            (Rows, Columns, MineCount) = DifficultySettings[difficulty];
            Cells = new Cell[Rows, Columns];
            InitializeCells();
            PlantMines();
            CalculateAdjacentMines();
        }

        private void InitializeCells()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Cells[i, j] = new Cell(i, j);
                }
            }
        }

        private void PlantMines()
        {
            Random random = new Random();
            int minesPlanted = 0;
            while (minesPlanted < MineCount)
            {
                int row = random.Next(Rows);
                int col = random.Next(Columns);
                if (!Cells[row, col].IsMine)
                {
                    Cells[row, col].IsMine = true;
                    minesPlanted++;
                }
            }
        }

        private void CalculateAdjacentMines()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (!Cells[i, j].IsMine)
                    {
                        int mineCount = 0;
                        foreach (var neighbor in GetNeighbors(i, j))
                        {
                            if (neighbor.IsMine)
                            {
                                mineCount++;
                            }
                        }
                        Cells[i, j].AdjacentMines = mineCount;
                    }
                }
            }
        }

        public Cell GetCell(int row, int col)
        {
            if (row >= 0 && row < Rows && col >= 0 && col < Columns)
            {
                return Cells[row, col];
            }
            return null;
        }

        public Cell[] GetNeighbors(int row, int col)
        {
            var neighbors = new List<Cell>();
            int[] dRow = { -1, -1, -1, 0, 1, 1, 1, 0 };
            int[] dCol = { -1, 0, 1, 1, 1, 0, -1, -1 };

            for (int i = 0; i < 8; i++)
            {
                int newRow = row + dRow[i];
                int newCol = col + dCol[i];
                if (newRow >= 0 && newRow < Rows && newCol >= 0 && newCol < Columns)
                {
                    neighbors.Add(Cells[newRow, newCol]);
                }
            }

            return neighbors.ToArray();
        }

        public void RevealCell(int row, int col)
        {
            var cell = GetCell(row, col);
            if (cell != null && !cell.IsRevealed)
            {
                cell.Reveal();
                if (cell.IsMine)
                {
                    Lives--;
                    // Additional logic for when a mine is revealed can be added here
                }
            }
        }
    }
}