using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class Cell
    {
        //Creates two ints to hold the position of the cell in a grid
        public int Row { get; set; }
        public int Column { get; set; }
        //Creates a boolean to tell whether or not the cell has been visited by the user
        public bool Visited { get; set; }
        //Creates a boolean to tell whether or not the cell is a bomb
        public bool Live { get; set; }
        //Creates an int to tell how many cells around any given cell are live
        public int LiveNeighbors { get; set; }

        public bool Checked { get; set; }

        //Creates a no-args constructor for each cell
        public Cell()
        {
            //initializes properties
            Row = -1;
            Column = -1;
            Visited = false;
            Live = false;
            LiveNeighbors = 0;
            Checked = false;
        }
    }
}
