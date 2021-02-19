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
        public IActionResult Index()
        {
            board = new Board(10);
            board.SetupLiveNeighbors(5);
            board.calculateLiveNeighbors();
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
