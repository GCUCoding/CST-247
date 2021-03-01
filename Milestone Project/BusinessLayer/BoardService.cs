using Milestone_Project.Models.GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Milestone_Project.BusinessLayer
{
    public class BoardService
    {
        public bool CheckWin(Board board)
        {
            foreach(Cell cell in board.Grid)
            {
                if(!cell.Live && !cell.Visited)
                {
                    return false;
                }
            }
            return true;
        }
        public bool CheckLoss(Board board)
        {
            foreach (Cell cell in board.Grid)
            {
                if (cell.Live && cell.Visited)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
