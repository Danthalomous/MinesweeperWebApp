using System;

namespace Milestone_Project
{
    public class Cell
    {
        public int Row { get; private set; }
        public int Column { get; private set; }
        public bool IsMine { get; internal set; }
        public bool IsRevealed { get; private set; }
        public bool IsFlagged { get; set; }
        public int AdjacentMines { get; internal set; }

        public Cell(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public void Reveal()
        {
            IsRevealed = true;
            // You can trigger some event here if needed, e.g., revealing the number of adjacent mines
        }
    }
}