using System;
using System.Collections.Generic;
using System.Text;

namespace Milestone_Project.Models.GameLogic
{ 
    public class Board
    {
        public int Size { get; set; }
        public int Difficulty { get; set; }

        //Creating the array
        public Cell[,] Grid;

        /// <summary>
        /// Constructng the board
        /// </summary>
        /// <param name="size"></param>
        public Board(int size)
        {
            Size = size;

            //this makes the board a square grid based on the size chosen by the player
            Grid = new Cell[size, size];

            //this nested for loop populates the grid with cells
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Grid[i, j] = new Cell();
                }
            }
        }

        /// <summary>
        /// Sets the number of mines using a random number based on the selected difficulty
        /// </summary>
        /// <param name="difficulty"></param>
        public void SetupLiveNeighbors(int difficulty)
        {
            Difficulty = difficulty;

            //Creates a new random integer
            Random random = new Random();

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    int r = random.Next(100);
                    /*
                     * this basically states that if the random int for each cell is equal or less than
                     * the difficulty set by the player, it becomes a bomb. So if the difficulty is 20,
                     * the only cells that become bombs are cells with the random ints 0-20 and 21-100 wiil
                     * all be empty or "safe" squares
                     */
                    if (r <= difficulty)
                    {
                        Grid[i, j].Live = true;
                    }
                }
            }
        }

        /// <summary>
        /// Increments the counter of each cell for the amount of mines located adjacent to it
        /// </summary>
        public void calculateLiveNeighbors()
        {   
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    //This if statement checks if the cell currently being checked is a bomb
                    if (Grid[i,j].Live)
                    {
                        Grid[i,j].LiveNeighbor = 9;
                    }

                    /*
                     * These if statements chech each of the 8 cells surrounding the cell currently 
                     * being checked and sets it's number to the amount of bombs adjacent to it
                    */
                    else
                    {
                        if (i < Size - 1 && Grid[i + 1,j].Live)
                        {
                            Grid[i, j].LiveNeighbor++;
                        }
                        if (j < Size - 1 && Grid[i, j + 1].Live)
                        {
                            Grid[i, j].LiveNeighbor++;
                        }
                        if (i < Size - 1 && j < Size - 1 && Grid[i + 1, j + 1].Live)
                        {
                            Grid[i, j].LiveNeighbor++;
                        }
                        if (i > 0 && Grid[i - 1, j].Live)
                        {
                            Grid[i, j].LiveNeighbor++;
                        }
                        if (j > 0 && Grid[i, j - 1].Live)
                        {
                            Grid[i, j].LiveNeighbor++;
                        }
                        if (i > 0 && j > 0 && Grid[i - 1, j - 1].Live)
                        {
                            Grid[i, j].LiveNeighbor++;
                        }
                        if (i < Size - 1 && j > 0 && Grid[i + 1, j - 1].Live)
                        {
                            Grid[i, j].LiveNeighbor++;
                        }
                        if (i > 0 && j < Size - 1 && Grid[i - 1, j + 1].Live)
                        {
                            Grid[i, j].LiveNeighbor++;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Uses recursion to visit all cells around a cell with no live neighbors until a live neighbor is found
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        public void FloodFill(int row, int column)
        {
            if (row > Size && row < 0 && column > Size && column < 0)
            {
                //Out of Bounds Exit
            }
            //If the current cell is NOT a mine, IS a 0, and HAS been visited - then continue
            else if (!Grid[row, column].Live && Grid[row, column].LiveNeighbor == 0 && Grid[row, column].Visited)
            {
                //Each if checks if that cell is valid and has been visited to double check for exceptions
                if (row < Size - 1 && !Grid[row + 1, column].Visited)
                {
                    //If the conditions of the if statement has been met - it will set the cell it is currently on to visited
                    Grid[row + 1, column].Visited = true;
                    //then it will call the method again using recusion using the new cell as the current cell
                    FloodFill(row + 1, column);
                }
                if (column < Size - 1 && !Grid[row, column + 1].Visited)
                {
                    Grid[row, column + 1].Visited = true;
                    FloodFill(row, column + 1);
                }
                if (row < Size - 1 && column < Size - 1 && !Grid[row + 1, column + 1].Visited)
                {
                    Grid[row + 1, column + 1].Visited = true;
                    FloodFill(row + 1, column + 1);
                }
                if (row > 0 && !Grid[row - 1, column].Visited)
                {
                    Grid[row - 1, column].Visited = true;
                    FloodFill(row - 1, column);
                }
                if (column > 0 && !Grid[row, column - 1].Visited)
                {
                    Grid[row, column - 1].Visited = true;
                    FloodFill(row, column - 1);
                }
                if (row > 0 && column > 0 && !Grid[row - 1, column - 1].Visited)
                {
                    Grid[row - 1, column - 1].Visited = true;
                    FloodFill(row - 1, column - 1);
                }
                if (row < Size - 1 && column > 0 && !Grid[row + 1, column - 1].Visited)
                {
                    Grid[row + 1, column - 1].Visited = true;
                    FloodFill(row + 1, column - 1);
                }
                if (row > 0 && column < Size - 1 && !Grid[row - 1, column + 1].Visited)
                {
                    Grid[row - 1, column + 1].Visited = true;
                    FloodFill(row - 1, column + 1);
                }
            }
        }

        /// <summary>
        /// This method checks all conditions to determine whether the game is over or needs to be continued
        /// </summary>
        /// <param name="board"> this calls to the current game board created </param>
        /// <param name="row"> this takes the user input for row </param>
        /// <param name="column"> this takes the user input for column </param>
        /// <returns> If this method returns false the game continues, if it returns true the game ends </returns>
        public int GUIIsGameOver(int row, int column)
        {
            //Lose
            if (Grid[row, column].Visited && Grid[row, column].Live)
            {
                return 1;
            }
            //Continue
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (!Grid[i, j].Visited && !Grid[i, j].Live)
                    {
                        return 0;
                    }
                }
            }
            //Win
            return 2;
        }
    }
}
