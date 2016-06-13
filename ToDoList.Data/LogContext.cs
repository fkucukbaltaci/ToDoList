using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Entity;

namespace ToDoList.Data
{
    public class LogContext
    {
        public List<Log> GetAll()
        {
            using (var dbContext = new ToDoListContext())
            {
                return dbContext.Logs.ToList();
            }
        }

        public Log Get(int LogId)
        {
            using (var dbContext = new ToDoListContext())
            {
                return dbContext.Logs.Single(x => x.Id == LogId);
            }
        }

        public int Add(Log log, out string Error)
        {
            Error = string.Empty;
            if (log == null)
            {
                Error = "Invalid Log!";
                return -1;
            }

            try
            {
                using (var dbContext = new ToDoListContext())
                {
                    dbContext.Logs.Add(log);
                    dbContext.SaveChanges();
                    return log.Id;
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return -1;
            }

        }      

    }
}
