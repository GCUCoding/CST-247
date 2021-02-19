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

        public bool ValidateUser(User user)
        {
            return dbAccess.ValidateUser(user);
        }

        public User GetUserByUsername(User user)
        {
            return dbAccess.GetUserByUsername(user);
        }
    }
}
