using Microsoft.AspNetCore.Mvc;
using Milestone_Project;
using MinesweeperWebApp.Models; // Make sure this using statement points to where your Board and Cell models are located
using MinesweeperWebApp.Services;

namespace MinesweeperWebApp.Controllers
{
    public class GameController : Controller
    {
        private GameService gameService;

        public GameController()
        {
            // In a real application, you'd want to inject this dependency.
            gameService = new GameService();
        }

        // This action returns the main view with the game board.
        public IActionResult Index()
        {
            return View(gameService.Board);
        }

        // Inside your GameController
        public IActionResult PlayGame()
        {
            // Assuming Board has a property that is a 2D array of Cell objects
            Cell[,] grid = gameService.Board.Cells;
            return View(grid); // Pass the grid to the view
        }

        // This action handles cell clicks.
        [HttpPost]
        public IActionResult CellClicked(int row, int col)
        {
            gameService.ProcessCellClick(row, col);
            var cell = gameService.Board.GetCell(row, col); // Define 'cell' here

            return Json(new
            {
                row = cell.Row,
                col = cell.Column,
                isRevealed = cell.IsRevealed,
                isMine = cell.IsMine,
                adjacentMines = cell.AdjacentMines,
                isFlagged = cell.IsFlagged,
                lives = gameService.Board.Lives // Assuming you have a 'Lives' property on your Board
            });
        }

        [HttpPost]
        public IActionResult ToggleFlag(int row, int col)
        {
            gameService.ToggleFlag(row, col);
            var cell = gameService.Board.GetCell(row, col); // Define 'cell' here

            return Json(new
            {
                row = cell.Row,
                col = cell.Column,
                isFlagged = cell.IsFlagged
            });
        }

        // This action handles the AJAX call to update one cell on the board.
        [HttpPost]
        public PartialViewResult ShowOneCell(int row, int col)
        {
            gameService.ProcessCellClick(row, col);
            var cell = gameService.Board.GetCell(row, col);

            // Here you could add additional checks for game state (win/lose)

            return PartialView("ShowOneCell", cell);
        }

        public IActionResult LoadSavedGame()
        {
            return View();
        }


    }
}
