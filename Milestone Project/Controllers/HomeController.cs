using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Milestone_Project.Models;
using Milestone_Project.Models.GameLogic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Milestone_Project.Controllers
{
    public class HomeController : Controller
    {
        static Board board;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            board = new Board(10);
            board.SetupLiveNeighbors(5);
            board.calculateLiveNeighbors();
            return View("Index", board);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
