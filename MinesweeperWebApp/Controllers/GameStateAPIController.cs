using Microsoft.AspNetCore.Mvc;
using Milestone_Project;
using MinesweeperWebApp.Models;
using MinesweeperWebApp.Services;
using System.Text.Json;

namespace MinesweeperWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameStateAPIController : ControllerBase
    {
        SaveBoardDAO savingService = new SaveBoardDAO();

        [HttpPut("saveBoard")]
        public IActionResult SaveBoard([FromBody] SaveBoardRequest savedBoard)
        {
            if (savingService.SaveBoard(savedBoard.UserID, savedBoard.SavedDate, savedBoard.GameBoard))
            {
                return Ok("Success");
            }
            else
            {
                return BadRequest("Failure");
            }
        }

        [HttpGet("getSaves")]
        public IActionResult GetAllSaves()
        {
            List<SaveBoardRequest> saveList = savingService.GetAllSaves();

            if(saveList.Count > 0)
            {
                return Ok(saveList);
            }
            else
            {
                return BadRequest("Failure");
            }
        }
    }
}
