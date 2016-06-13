using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Entity;

namespace ToDoList.Data
{
    public class TodoContext
    {
        public List<Todo> TodoList(int UserId)
        {
            using (var dbContext = new ToDoListContext())
            {
                var query = (from t in dbContext.Todos where t.User.Id == UserId select t).ToList<Todo>();
                return query;
            }
        }

        public Todo Get(int TodoId)
        {
            using (var dbContext = new ToDoListContext())
            {
                return dbContext.Todos.Single(x => x.Id == TodoId);
            }
        }

        public bool Add(Todo todo, out string Error)
        {
            Error = string.Empty;
            if (todo == null)
            {
                Error = "Invalid Todo!";
                return false;
            }

            try
            {
                using (var dbContext = new ToDoListContext())
                {
                    dbContext.Todos.Add(todo);
                    dbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }

        }

        public bool Edit(Todo todo, out string Error)
        {
            Error = string.Empty;
            if (todo == null)
            {
                Error = "Invalid Todo!";
                return false;
            }

            try
            {
                using (var dbContext = new ToDoListContext())
                {
                    var uTodo = dbContext.Todos.SingleOrDefault(t => t.Id == todo.Id);
                    if (uTodo == null)
                    {
                        Error = "Invalid Todo!";
                        return false;
                    }


                    uTodo.Name = todo.Name;

                    dbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }


        }

        public List<Todo> GetAll()
        {
            using (var dbContext = new ToDoListContext())
            {
                return dbContext.Todos.ToList();
            }
        }

        public bool Delete(int TodoId, out string Error)
        {
            Error = string.Empty;
        

            try
            {
                using (var dbContext = new ToDoListContext())
                {
                    var uTodo = dbContext.Todos.SingleOrDefault(t => t.Id == TodoId);
                    if (uTodo == null)
                    {
                        Error = "Invalid Todo!";
                        return false;
                    }


                    dbContext.Todos.Remove(uTodo);

                    dbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }


        }
    }
}
