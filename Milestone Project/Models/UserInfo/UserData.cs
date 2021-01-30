using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;
using DataAccessLayer;

namespace Milestone_Project.Models.UserInfo
{
    public class UserData
    {
        UserDataAccess dataAccess = new UserDataAccess();

        public IEnumerable<User> GetUsers()
        {
            return dataAccess.GetUsers();
        }

        public bool AddUser(User user)
        {
            return dataAccess.AddUser(user);
        }
    }
}
