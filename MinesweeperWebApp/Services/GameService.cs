using Milestone_Project;

namespace MinesweeperWebApp.Services
{
    public class GameService
    {
        public Board Board { get; private set; }

        public GameService()
        {
            // Initialize the game with a difficulty level
            Board = new Board(Board.Difficulty.Easy);
        }

        public void ProcessCellClick(int row, int col)
        {
            Board.RevealCell(row, col);

        }

        public void ToggleFlag(int row, int col)
        {
            var cell = Board.GetCell(row, col);
            if (cell != null)
            {
                cell.IsFlagged = !cell.IsFlagged;
            }

        }


    }
}
