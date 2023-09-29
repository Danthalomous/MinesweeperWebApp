using MinesweeperWebApp.Models;
using System.Data.SqlClient;

namespace MinesweeperWebApp.Services
{
	public class SecurityDAO
	{
		string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Minesweeper;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

		// Public method that searches and validates if the user exists
		public bool FindUserByUserNameAndPassword(LoginModel user)
		{
			bool success = false;

			// Sql Querry
			string sqlStatement = "Select * From Users where Username = @username and Password = @password";

			// Connecting to the database and running the querry
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(sqlStatement, connection);

				command.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 40).Value = user.Username;
				command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 40).Value = user.Password;

				try
				{
					connection.Open();
					SqlDataReader reader = command.ExecuteReader();

					// Ensuring it found a result
					if (reader.HasRows)
						success = true;
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}

			return success;
		}

		// Public method that creates a new user to the database
		public bool CreateUser(UserModel user)
		{
			bool success = false;

			// First command is to see if the username is valid
			string sqlStatement = "Select * From Users where Username = @username";

			// Second command is the actual insert statement for a new user
			string sqlInsertStatement = "Insert Into Users (FirstName, LastName, Sex, Age, State, Email, Username, Password) Values (@firstname, @lastname, @sex, @age, @state, @email, @username, @password)";

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				// First command
				// ---------------------------------------------------------------------
				SqlCommand testCommand = new SqlCommand(sqlStatement, connection);

				testCommand.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 40).Value = user.Username;
				// ---------------------------------------------------------------------
				// Second Command
				// ---------------------------------------------------------------------
				SqlCommand insertCommand = new SqlCommand(sqlInsertStatement, connection);

				insertCommand.Parameters.Add("@firstname", System.Data.SqlDbType.NVarChar, 40).Value = user.FirstName;

				insertCommand.Parameters.Add("@lastname", System.Data.SqlDbType.NVarChar, 40).Value = user.LastName;

				insertCommand.Parameters.Add("@sex", System.Data.SqlDbType.NVarChar, 8).Value = user.Sex;

				insertCommand.Parameters.Add("@age", System.Data.SqlDbType.Int).Value = user.Age;

				insertCommand.Parameters.Add("@state", System.Data.SqlDbType.NVarChar, 15).Value = user.State;

				insertCommand.Parameters.Add("@email", System.Data.SqlDbType.NVarChar, 40).Value = user.Email;

				insertCommand.Parameters.Add("@username", System.Data.SqlDbType.NVarChar, 40).Value = user.Username;

				insertCommand.Parameters.Add("@password", System.Data.SqlDbType.NVarChar, 40).Value = user.Password;

				// ---------------------------------------------------------------------

				try
				{
					connection.Open();
					SqlDataReader reader = testCommand.ExecuteReader();

					// Checking if the username has already been used
					if (reader.HasRows)
					{
						success = false;
					}
					else
					{
						reader.Close();

						insertCommand.ExecuteNonQuery();

						// If no issues then this line will execute and the user was inserted into the table
						success = true;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}

			return success;
		}
	}
}
