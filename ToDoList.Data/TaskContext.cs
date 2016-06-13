using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDoList.Entity;


namespace ToDoList.Data
{
    public class TaskContext
    {
        public List<Task> TaskList(int TodoId,int UserId)
        {
            using (var dbContext = new ToDoListContext())
            {
                var query = (from t in dbContext.Tasks where t.Todo.Id == TodoId && t.User.Id==UserId  select t).ToList<Task>();
                return query;
            }
        }

        public Task Get(int TaskId)
        {
            using (var dbContext = new ToDoListContext())
            {
                return dbContext.Tasks.Single(x => x.Id == TaskId);
            }
        }

        public int Add(Task task,out string Error)
        {
            Error = string.Empty;
            if (task == null)
            {
                Error = "Invalid Task!";
                return -1;
            }

            try
            {
                using (var dbContext = new ToDoListContext())
                {
                    dbContext.Tasks.Add(task);
                    dbContext.SaveChanges();
                    return task.Id;
                }
            }
            catch(Exception ex)
            {
                Error = ex.Message;
                return -1;
            }
            
        }

        public int Edit(Task task, out string Error)
        {
            Error = string.Empty;
            if (task == null)
            {
                Error = "Invalid Task!";
                return -1;
            }

            try
            {
                using (var dbContext = new ToDoListContext())
                {
                    var uTask = dbContext.Tasks.SingleOrDefault(t => t.Id == task.Id);
                    if (uTask == null)
                    {
                        Error = "Invalid Task!";
                        return -1;
                    }


                    uTask.Name = task.Name;
                    uTask.IsDone = task.IsDone;
                    uTask.NotifyBeforeMin = task.NotifyBeforeMin;
                    uTask.StartTime = task.StartTime;
                    uTask.Description = task.Description;

                    dbContext.SaveChanges();
                    return task.Id;
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return -1;
            }

            
        }

        public List<Task> GetAll()
        {
            using (var dbContext = new ToDoListContext())
            {
                return dbContext.Tasks.ToList();
            }
        }

        public List<Task> GetAllActive()
        {
            using (var dbContext = new ToDoListContext())
            {
                return dbContext.Tasks.Where(t => t.IsDone == false).ToList();
            }
        }

        public bool Delete(int TaskId, out string Error)
        {
            Error = string.Empty;


            try
            {
                using (var dbContext = new ToDoListContext())
                {
                    var uTask = dbContext.Tasks.SingleOrDefault(t => t.Id == TaskId);
                    if (uTask == null)
                    {
                        Error = "Invalid Task!";
                        return false;
                    }


                    dbContext.Tasks.Remove(uTask);

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

        public bool DeleteByTodo(int TodoId , out string Error)
        {
            Error = string.Empty;


            try
            {
                using (var dbContext = new ToDoListContext())
                {
                    var dList = dbContext.Tasks.Where(t => t.Todo.Id == TodoId);

                    dbContext.Tasks.RemoveRange(dList);

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

        public List<Task> GetNotificationList(out string Error)
        {
            Error = string.Empty;

            try
            {
                using (var dbContext = new ToDoListContext())
                {
                    
                    return dbContext.Tasks.Where(t => 
                        t.StartTime.ToString("yyyy-MM-dd HH:mm") == System.Data.Entity.DbFunctions.AddMinutes(DateTime.Now,t.NotifyBeforeMin).Value.ToString("yyyy-MM-dd HH:mm")
                    ).ToList();
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return null;
            }
        }
    }
}
