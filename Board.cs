using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    //Creates a new class to represent the Minesweeper Board
    public class Board
    {
        //Creates an int property called Size to contain the size of the board
        public int Size { get; set; }
        //Creates a 2D array property of type Cell in order to represent the board itself
        public Cell[,] Grid { get; set; }
        //Creates an int property to hold a difficulty value set by the user in order to make the game more difficult
        public int Difficulty { get; set; }

        //Creates a constructor for objects of the board class, getting the size parameter elsewhere
        public Board(int size)
        {
            //Sets the size of the board
            Size = size;
            //Creates a new 2D array based on the size of the board specified
            Grid = new Cell[size,size];
            //initializes the array
            for(int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    Grid[i, j] = new Cell
                    {
                        //sets the row and column properties of the new cell objects
                        Row = i,
                        Column = j
                    };
                }
            }
        }

        //defines a method to set whether or not a given cell is live(a bomb)
        public void SetupLiveNeighbors(int difficulty)
        {
            Difficulty = difficulty;
            Random r1 = new Random();
            int checkedNum;
            for(int i = 0; i < Size; i++)
            {
                for(int j = 0; j < Size; j++)
                {
                    checkedNum = r1.Next(100);
                    if(checkedNum <= Difficulty)
                    {
                        
                        Grid[i, j].Live = true;
                    }
                }
            }    
        }
        /*calculates the amount of cells that are live around a given cell, unless that cell is live, and sets each cell's LiveNeighbor
        *property accordingly
        */
        public void CalculateLiveNeighbors()
        {
            for(int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (Grid[i, j].Live)
                    {
                        Grid[i, j].LiveNeighbors = 9;
                    }
                    else
                    {
                        if (i < Size-1 && Grid[i + 1, j].Live)
                        {
                            Grid[i, j].LiveNeighbors++;
                        }
                        if (i > 0 && Grid[i - 1, j].Live)
                        {
                            Grid[i, j].LiveNeighbors++;
                        }
                        if (i < Size-1 && j > 0 && Grid[i + 1, j - 1].Live)
                        {
                            Grid[i, j].LiveNeighbors++;
                        }
                        if (i > 0 && j > 0 && Grid[i - 1, j - 1].Live)
                        {
                            Grid[i, j].LiveNeighbors++;
                        }
                        if (i < Size-1 && j < Size-1 && Grid[i + 1, j + 1].Live)
                        {
                            Grid[i, j].LiveNeighbors++;
                        }
                        if (i > 0 && j < Size-1 && Grid[i - 1, j + 1].Live)
                        {
                            Grid[i, j].LiveNeighbors++;
                        }
                        if (j < Size-1 && Grid[i, j + 1].Live)
                        {
                            Grid[i, j].LiveNeighbors++;
                        }
                        if ( j > 0 && Grid[i, j - 1].Live)
                        {
                            Grid[i, j].LiveNeighbors++;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Reveals cells that neighbor a selected cell that is not neighbored by a bomb that aren't live 
        /// </summary>
        /// <param name="row">The row of a given cell</param>
        /// <param name="col">The column of a given cell</param>
        public void RevealSafeNeighbors(int row, int col)
        {

            if(Grid[row,col].Visited && !Grid[row,col].Checked && Grid[row,col].LiveNeighbors == 0)
            {
                Grid[row, col].Checked = true;

                if(row + 1 < Size)
                {
                    Grid[row + 1, col].Visited = true;
                    RevealSafeNeighbors(row + 1, col);
                }
                if(row - 1 > -1)
                {
                    Grid[row - 1, col].Visited = true;
                    RevealSafeNeighbors(row - 1, col);
                }
                if(col + 1 < Size)
                {
                    Grid[row, col + 1].Visited = true;
                    RevealSafeNeighbors(row, col + 1);
                }
                if(col - 1 > -1)
                {
                    Grid[row, col - 1].Visited = true;
                    RevealSafeNeighbors(row, col-1);
                }
                if(row + 1 < Size && col + 1 < Size)
                {
                    Grid[row + 1, col + 1].Visited = true;
                    RevealSafeNeighbors(row + 1, col + 1);
                }
                if(row + 1 < Size && col - 1 > -1)
                {
                    Grid[row + 1, col - 1].Visited = true;
                    RevealSafeNeighbors(row + 1, col - 1);
                }
                if(row - 1 > -1 && col + 1 < Size)
                {
                    Grid[row - 1, col + 1].Visited = true;
                    RevealSafeNeighbors(row - 1, col + 1);
                }
                if(row - 1 > -1 && col - 1 > -1)
                {
                    Grid[row - 1, col - 1].Visited = true;
                    RevealSafeNeighbors(row - 1, col - 1);
                }
                
            }
        }
        /// <summary>
        /// Checks to see if the game has ended in a console version
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public bool CheckGameEnded(int row, int col)
        {
            if (Grid[row, col].Live && Grid[row, col].Visited)
            {
                Console.WriteLine("You have hit a bomb and the game is over!");
                return true;
            }
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (!Grid[i, j].Live && !Grid[i, j].Visited)
                    {
                        return false;
                    }
                }
            }
            Console.WriteLine("You have won the game! Congratulations!");
            return true;
        }

        /// <summary>
        /// Checks to see if the user has succeeded in winning the game or if the user has lost
        /// </summary>
        /// <param name="row">The row of the button that was clicked</param>
        /// <param name="col">The column of the button that was clicked</param>
        /// <returns></returns>
        public string CheckGUIGameEnded(int row, int col)
        {
            //if the button that was clicked was a bomb, result in a loss
            if (Grid[row, col].Live && Grid[row, col].Visited)
            {
                return "loss";
            }
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    //if there are still board spaces which have not been visited and are also not bombs, continue the game
                    if (!Grid[i, j].Live && !Grid[i, j].Visited)
                    {
                        return "continue";
                    }
                }
            }
            //if all board spaces have been visited that are not also bombs, result in a win
            return "win";
        }
    }
}
