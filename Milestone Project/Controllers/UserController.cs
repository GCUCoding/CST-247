using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer;
using BusinessLayer;
using Milestone_Project.Models.UserInfo;

namespace Milestone_Project.Controllers
{
    public class UserController : Controller
    {
        UserData userDAL = new UserData();

        public IActionResult Index()
        {
            List<User> users = new List<User>();
            users = userDAL.GetUsers().ToList();
            return View(users);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Register([Bind] User user)
        {
            MinesweeperController m = new MinesweeperController();
            if (ModelState.IsValid)
            {
                userDAL.AddUser(user);
                return View("../Home/Index");
            }
            return View(user);
        }
    }
}
