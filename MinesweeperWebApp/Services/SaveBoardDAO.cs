using Milestone_Project;
using MinesweeperWebApp.Models;
using System.Data.SqlClient;
using System.Text.Json;

namespace MinesweeperWebApp.Services
{
    public class SaveBoardDAO
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Minesweeper;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public bool SaveBoard(int userID, DateTime date, string boardState)//Board boardToSave)
        {
            bool success = false;

            //string boardState = JsonSerializer.Serialize(boardToSave);

            string sqlStatement = "INSERT INTO SavedState (user_id, save_date, game_state) VALUES (@userID, @saveData, @gameState)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@userID", System.Data.SqlDbType.Int).Value = userID;
                command.Parameters.Add("@saveData", System.Data.SqlDbType.DateTime).Value = date;
                command.Parameters.Add("@gameState", System.Data.SqlDbType.VarChar).Value = boardState;

                try
                {
                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                        success = true;
                    else
                        success = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return success;
        }

        public List<SaveBoardRequest> GetAllSaves()
        {
            List<SaveBoardRequest> returnThis = new List<SaveBoardRequest>();

            string sqlStatement = "SELECT save_date, game_state FROM dbo.SavedState";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        returnThis.Add(new SaveBoardRequest(0, (DateTime)reader[0], (string)reader[1]));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return returnThis;
        }

        public SaveBoardRequest LoadBoard(int userID)
        {
            SaveBoardRequest savedGame = null;
            string sqlStatement = "SELECT game_state FROM dbo.SaveState WHERE user_id = @userID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.Add("@userID", System.Data.SqlDbType.Int).Value = userID;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        savedGame = new SaveBoardRequest(userID, DateTime.MinValue, reader["game_state"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return savedGame;
        }

        public bool DeleteBoard(int userID)
        {
            bool success = false;
            string sqlStatement = "DELETE FROM dbo.SaveState WHERE user_id = @userID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.Add("@userID", System.Data.SqlDbType.Int).Value = userID;

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    success = rowsAffected > 0;
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
