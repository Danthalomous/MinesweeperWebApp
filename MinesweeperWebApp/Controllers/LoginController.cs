using Microsoft.AspNetCore.Mvc;
using MinesweeperWebApp.Models;
using MinesweeperWebApp.Services;
using System.Security.Permissions;

namespace MinesweeperWebApp.Controllers
{
	public class LoginController : Controller
	{
		/// <summary>
		/// Main login page routing
		/// </summary>
		/// <returns></returns>
		public IActionResult Index()
		{
			return View();
		}

		/// <summary>
		/// Handling the login page and routing to the correct page
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public IActionResult ProcessLogin(LoginModel user)
		{
			SecurityService securityService = new SecurityService();

			if(securityService.IsValid(user))
				return View("LoginSuccess", user);
			else
				return View("LoginFailure", user);

		}
	}
}
