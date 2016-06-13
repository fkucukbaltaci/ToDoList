using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Data;
using ToDoList.Entity;

namespace ToDoList.Repository
{
    public class LogRepository
    {
        private readonly LogContext _logContext;
        public LogRepository()
        {
            _logContext = new LogContext();
        }

        public List<Log> GetAll()
        {
            return _logContext.GetAll();
        }

        public Log Get(int LogId)
        {
            return _logContext.Get(LogId);
        }

        public int Add(Log log, out string Error)
        {
            return _logContext.Add(log, out Error);
        }
    }
 }

