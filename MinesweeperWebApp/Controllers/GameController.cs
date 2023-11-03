using Microsoft.AspNetCore.Mvc;
using Milestone_Project;

public class GameController : Controller
{
    private static Board board = new Board(0.5);


    public IActionResult PlayGame()
    {
        return View(board.Grid);
    }

    public IActionResult CellClicked(int row, int col)
    {
        board.ProcessClick(row, col);
        return RedirectToAction("PlayGame");
    }

    [HttpPost]
    public PartialViewResult ShowOneCell(int row, int col)
    {
        board.ProcessClick(row, col);
        return PartialView("ShowOneCell", board.GetCell(row, col));
    }
}
