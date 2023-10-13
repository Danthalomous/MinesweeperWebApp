using System;

namespace Milestone_Project
{
    public class Cell
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public bool Visited { get; set; }
        public bool Live { get; set; }
        public int LiveNeighbors { get; set; }
        public bool Flagged { get; set; }

        public Cell(int row, int column)
        {
            Row = row;
            Column = column;
            Visited = false;
            LiveNeighbors = 0;
            Live = false;
            Flagged = false;
        }
    }
}
