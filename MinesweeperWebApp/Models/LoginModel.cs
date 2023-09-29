using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MinesweeperWebApp.Models
{
	public class LoginModel
	{
		// Properties with their validation rules
		[Required]
		[DisplayName("Enter Username")]
		public string Username { get; set; }

		[Required]
		[DisplayName("Enter Password")]
		public string Password { get; set; }

		/// <summary>
		/// Default Constructor
		/// </summary>
		public LoginModel()
		{

		}

		/// <summary>
		/// Parameterized Constructor
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		public LoginModel(string username, string password)
		{
			Username = username;
			Password = password;
		}
	}
}
