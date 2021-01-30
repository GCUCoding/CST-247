using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer;

namespace BusinessLayer
{
    public class UserDataAccess
    {
        DatabaseManager dbAccess = new DatabaseManager();

        public IEnumerable<User> GetUsers()
        {
            return dbAccess.GetUsers();
        }

        public bool AddUser(User user)
        {
            return dbAccess.AddUser(user);
        }
    }
}
