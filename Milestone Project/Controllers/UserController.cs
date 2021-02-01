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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind] User user)
        {
            if (ModelState.IsValid)
            {
                userDAL.AddUser(user);
                List<User> users = new List<User>();
                users = userDAL.GetUsers().ToList();
                return View(users);                
            }
            return View(user);
        }
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register([Bind] User user)
        {
            if (ModelState.IsValid)
            {
                userDAL.AddUser(user);
                return View("../Home/Index");
            }
            return View(user);
        }
    }
}
