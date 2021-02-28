using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
        {
            board = new Board(10);
            firstMoveTaken = false;
            return View("Index", board);
        }
        
        public IActionResult HandleButtonClick(string coords)
        {
            string[] coordsArr = coords.Split(',');
            int i = int.Parse(coordsArr[0]);
            int j = int.Parse(coordsArr[1]);
            board.Grid[i, j].Visited = true;
            if (!firstMoveTaken)
            {
                board.SetupLiveNeighbors(5);
                board.calculateLiveNeighbors();
                firstMoveTaken = true;
            }
            board.FloodFill(i, j);
            return View("Index", board);
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
