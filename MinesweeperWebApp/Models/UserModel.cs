using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MinesweeperWebApp.Models
{
	public class UserModel
	{
		// Properties with their validation rules
		public int Id { get; set; }

		[Required]
		[StringLength(30, MinimumLength = 1)]
		[DisplayName("First Name")]
		public string FirstName { get; set; }

		[Required]
		[StringLength(30, MinimumLength = 1)]
		[DisplayName("Last Name")]
		public string LastName { get; set; }

		[Required]
		[StringLength(7, MinimumLength = 4)]
		[DisplayName("Sex")]
		public string Sex { get; set; }

		[Required]
		[Range(18, 100)]
		[DisplayName("Age")]
		public int Age { get; set; }

		[Required]
		[StringLength(15, MinimumLength = 5)]
		[DisplayName("State")]
		public string State { get; set; }

		[Required]
		[EmailAddress]
		[DisplayName("Email")]
		public string Email { get; set; }

		[Required]
		[StringLength(30, MinimumLength = 5)]
		[DisplayName("Username")]
		public string Username { get; set; }

		[Required]
		[StringLength(25, MinimumLength = 8)]
		[DisplayName("Password")]
		public string Password { get; set; }

		/// <summary>
		/// Default Constructor
		/// </summary>
		public UserModel()
		{

		}

		/// <summary>
		/// Parameterized Constructor
		/// </summary>
		/// <param name="id"></param>
		/// <param name="firstName"></param>
		/// <param name="lastName"></param>
		/// <param name="sex"></param>
		/// <param name="age"></param>
		/// <param name="state"></param>
		/// <param name="email"></param>
		/// <param name="username"></param>
		/// <param name="password"></param>
		public UserModel(int id, string firstName, string lastName, string sex, int age, string state, string email, string username, string password)
		{
			Id = id;
			FirstName = firstName;
			LastName = lastName;
			Sex = sex;
			Age = age;
			State = state;
			Email = email;
			Username = username;
			Password = password;
		}
	}
}
