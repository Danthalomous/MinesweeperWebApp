using Microsoft.AspNetCore.Mvc;
using MinesweeperWebApp.Models;
using MinesweeperWebApp.Services;

namespace MinesweeperWebApp.Controllers
{
	public class RegisterController : Controller
	{
		/// <summary>
		/// Routing for the main register page
		/// </summary>
		/// <returns></returns>
		public IActionResult Index()
		{
			return View();
		}

		/// <summary>
		/// Handling the register page and routing to the correct page
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public IActionResult ProcessRegister(UserModel user)
		{
			SecurityService securityService = new SecurityService();

			if(securityService.CanRegister(user))
				return RedirectToAction("Gameboard", "Home");
			else
				return View("RegisterFailure", user);
		}
	}
}
