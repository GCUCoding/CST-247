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
        public IActionResult Login([Bind]User user)
        {
            if(userDAL.ValidateUser(user))
            {
                User loggedUser = userDAL.GetUserByUsername(user);
                return View(loggedUser);
            }
            return View("../Minesweeper/Login");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View("../Minesweeper/Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register([Bind] User user)
        {
            List<User> users;

            if (ModelState.IsValid)
            {
                userDAL.AddUser(user);
                user = userDAL.GetUserByUsername(user);
                return View("Login", user);
            }
            users = userDAL.GetUsers().ToList();
            return View("../Minesweeper/Register");
        }
    }
}
