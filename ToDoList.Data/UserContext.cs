using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Entity;

namespace ToDoList.Data
{
    public class UserContext
    {
        public List<User> GetAll()
        {
            using (var dbContext = new ToDoListContext())
            {
                return dbContext.Users.ToList();
            }
        }

        public User Get(int UserId)
        {
            using (var dbContext = new ToDoListContext())
            {
                return dbContext.Users.Single(x => x.Id == UserId);
            }
        }

        public int Add(User user, out string Error)
        {
            Error = string.Empty;
            if (user == null)
            {
                Error = "Invalid User!";
                return -1;
            }

            try
            {
                using (var dbContext = new ToDoListContext())
                {
                    user.Password = Utility.Security.CreateMD5(user.Password);
                    dbContext.Users.Add(user);
                    dbContext.SaveChanges();
                    return user.Id;
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return -1;
            }

        }

        public int Edit(User user, out string Error)
        {
            Error = string.Empty;
            if (user == null)
            {
                Error = "Invalid User!";
                return -1;
            }

            try
            {
                using (var dbContext = new ToDoListContext())
                {
                    var uUser = dbContext.Users.SingleOrDefault(t => t.Id == user.Id);
                    if (uUser == null)
                    {
                        Error = "Invalid User!";
                        return -1;
                    }
                    
                    uUser.Name = user.Name;
                    uUser.Password = Utility.Security.CreateMD5(uUser.Password);
                    uUser.Email = uUser.Email;

                    dbContext.SaveChanges();
                    return uUser.Id;
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return -1;
            }


        }

        public User Login(string Email, string Password)
        {
            try
            {
                using (var dbContext = new ToDoListContext())
                {
                    Password = Utility.Security.CreateMD5(Password);
                    var uUser = dbContext.Users.SingleOrDefault(t => t.Email == Email && t.Password == Password);
                    return uUser;
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                //Error = ex.Message;
                return null;
            }
        }

    }
}
