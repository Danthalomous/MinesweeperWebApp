using MinesweeperWebApp.Models;

namespace MinesweeperWebApp.Services
{
	public class SecurityService
	{
		// Instance of the security service
		SecurityDAO securityDAO = new SecurityDAO();

		/// <summary>
		/// Checking to see if the entered data is valid
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public bool IsValid(LoginModel user)
		{
			return securityDAO.FindUserByUserNameAndPassword(user);
		}

		/// <summary>
		/// Checking to see if the entered data is valid for registration
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public bool CanRegister(UserModel user)
		{
			return securityDAO.CreateUser(user);
		}
	}
}
