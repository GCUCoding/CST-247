using Microsoft.AspNetCore.Mvc;
using Milestone_Project.BusinessLayer;
using Milestone_Project.Models.GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Milestone_Project.Controllers
{
    public class MinesweeperController : Controller
    {
        static Board board;
        static bool firstMoveTaken = false;
        BoardService boardService = new BoardService();
        public IActionResult Index()
        {
            board = new Board(10);
            firstMoveTaken = false;
            return View("Index", board);
        }
        
        public IActionResult HandleButtonClick(string place)
        {
            ViewBag.place = place;
            int location = Convert.ToInt32(place);
            int i = location/board.Size;
            int j = location%board.Size;
            board.Grid[i, j].Visited = true;
            board.Grid[i, j].Flagged = false;
            if (!firstMoveTaken)
            {
                board.SetupLiveNeighbors(5);
                board.calculateLiveNeighbors();
                firstMoveTaken = true;
            }
            board.FloodFill(i, j);
            if (boardService.CheckWin(board))
            {
                return View("GameWin", board);
            }
            else if (boardService.CheckLoss(board))
            {
                return View("GameLoss", board);
            }
            else
            {
                return View("Index", board);
            }
        }

        public IActionResult FlagButton(string id)
        {
            int location = Convert.ToInt32(id);
            int i = location / board.Size;
            int j = location % board.Size;
            if(board.Grid[i, j].Flagged)
            {
                board.Grid[i, j].Flagged = false;
            }
            else
            {
                board.Grid[i, j].Flagged = true;
            }
            return PartialView(board.Grid[i,j]);
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
