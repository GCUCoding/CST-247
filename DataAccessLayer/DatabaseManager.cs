using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    public class DatabaseManager
    {
        public string DBUsersConn { get; set; }
        public DatabaseManager()
        {
            this.DBUsersConn = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Users;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        public IEnumerable<User> GetUsers()
        {
            List<User> users = new List<User>();

            using (SqlConnection conn = new SqlConnection(DBUsersConn))
            {
                //Set the SQL command to the stored procedures
                using (SqlCommand cmd = new SqlCommand("usp_GetAllUsers", conn))
                {
                    //By setting the command type to StoredProcedure, the first parameter 
                    //to the SQL command will be interpreted as the name of a stored procedure
                    //(instead of interpreting it as a command string) which would typically be the sql query

                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    //Provides a way or reading a forward-only stream of rows from a SQL server
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        User user = new User();
                        try
                        {
                            user.ID = Convert.ToInt32(dataReader["ID"].ToString());
                        }
                        catch
                        {
                            user.ID = 0;
                        }
                        user.Name = dataReader["Name"].ToString();
                        user.Username = dataReader["Username"].ToString();
                        user.Password = dataReader["Password"].ToString();
                        users.Add(user);
                    }
                    //end of the while loop

                }
                //good practice to close connnection 
                conn.Close();
            }
            return users;
        }

        public bool AddUser(User user)
        {
            using (SqlConnection conn = new SqlConnection(DBUsersConn))
            {
                //Set the SQL command to the stored 
                //if you don't want to use stored procedures, this is a second example
                // string insertCmd = @"INSERT INTO Employee (Name, Gender, Company, Department)
                //                      VALUES(@Name, @Gender, @Company, @Department";
                //SqlCommand cmd = new SqlCommand(insertCmd, conn)
                using (SqlCommand cmd = new SqlCommand("usp_AddUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Name", user.Name);
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Password", user.Password);

                    conn.Open();
                    //ExecuteNonQuery used for executing queries that do not return any data
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            return true;
        }

        public bool ValidateUser(User user)
        {
            User userCheck = new User();
            using (SqlConnection conn = new SqlConnection(DBUsersConn))
            {
                //Set the SQL command to the stored procedures
                using (SqlCommand cmd = new SqlCommand("usp_Login", conn))
                {
                    //By setting the command type to StoredProcedure, the first parameter 
                    //to the SQL command will be interpreted as the name of a stored procedure
                    //(instead of interpreting it as a command string) which would typically be the sql query

                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    //Provides a way or reading a forward-only stream of rows from a SQL server
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        try
                        {
                            userCheck.ID = Convert.ToInt32(dataReader["ID"].ToString());
                        }
                        catch
                        {
                            user.ID = 0;
                        }
                        userCheck.Name = dataReader["Name"].ToString();
                        userCheck.Username = dataReader["Username"].ToString();
                        userCheck.Password = dataReader["Password"].ToString();
                    }
                    //end of the while loop

                }
                //good practice to close connnection 
                conn.Close();
            }
            if(userCheck.ID == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public User GetUserByUsername(User user)
        {
            User userCheck = new User();
            using (SqlConnection conn = new SqlConnection(DBUsersConn))
            {
                //Set the SQL command to the stored procedures
                using (SqlCommand cmd = new SqlCommand("usp_GetUserByUsername", conn))
                {
                    //By setting the command type to StoredProcedure, the first parameter 
                    //to the SQL command will be interpreted as the name of a stored procedure
                    //(instead of interpreting it as a command string) which would typically be the sql query

                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    cmd.Parameters.AddWithValue("@Username", user.Username);

                    //Provides a way or reading a forward-only stream of rows from a SQL server
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        try
                        {
                            userCheck.ID = Convert.ToInt32(dataReader["ID"].ToString());
                        }
                        catch
                        {
                            user.ID = 0;
                        }
                        userCheck.Name = dataReader["Name"].ToString();
                        userCheck.Username = dataReader["Username"].ToString();
                        userCheck.Password = dataReader["Password"].ToString();
                    }
                    //end of the while loop

                }
                //good practice to close connnection 
                conn.Close();
            }
            return userCheck;
        }
    }
}
