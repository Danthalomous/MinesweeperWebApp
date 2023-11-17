namespace MinesweeperWebApp.Models
{
    public class SaveBoardRequest
    {
        public int UserID { get; set; }
        public DateTime SavedDate { get; set; }
        public string GameBoard { get; set; }

        public SaveBoardRequest(int userID, DateTime savedDate, string gameBoard)
        {
            UserID = userID;
            SavedDate = savedDate;
            GameBoard = gameBoard;
        }
    }
}
