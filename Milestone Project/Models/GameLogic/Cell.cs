using System;
using System.Collections.Generic;
using System.Text;

namespace Milestone_Project.Models.GameLogic
{
    public class Cell
    {
        //Create all properties needed for a cell with getters and setters
        public int Row { get; set; }
        public int Column { get; set; }
        public bool Visited { get; set; }
        public bool Live { get; set; }
        public int LiveNeighbor { get; set; }

        public int BoardSize { get; set; }

        public bool Flagged { get; set; }
        /// <summary>
        /// Cell Constructor
        /// </summary>
        public Cell()
        {
            Row = -1;
            Column = -1;
            Visited = false;
            Live = false;
            LiveNeighbor = 0; /*This is going to be the counter that makes the numbers
                                *in the array match the number of adjacent bomb cells */
        }
    }
}
