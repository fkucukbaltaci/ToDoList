using System.Collections.Generic;
using System.Linq;
using ToDoList.Data;
using ToDoList.Entity;

namespace ToDoList.Repository
{
    public class UserRepository
    {
        private readonly UserContext _userContext;
        public UserRepository()
        {
            _userContext = new UserContext();
        }

        public List<User> GetAll()
        {
            return _userContext.GetAll();
        }

        public User Get(int UserId)
        {
            return _userContext.Get(UserId);
        }

        public int Save(User user, out string Error)
        {
            Error = string.Empty;

            return user.Id < 1 ? _userContext.Add(user, out Error) : _userContext.Edit(user, out Error);
        }

        public User Login(string Email, string Password)
        {
            return _userContext.Login(Email,Password);
        }
    }
}
