using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ToDoList.Entity;
using ToDoList.Repository;

namespace ToDoList.Notification
{
    class SMSNotifier : INotify
    {
        public void Notify(User user, Entity.Task task)
        {
            //Thread.Sleep(TimeSpan.FromSeconds(10));

            Log log = new Log();
            log.Date = DateTime.Now;
            log.Level = "Info";
            log.Logger = "SMS Notifier";
            log.Message = "UserId:" + user.Id + " TaskId:" + task.Id;
            log.Thread = string.Empty;

            LogRepository logRepo = new LogRepository();
            string Error = string.Empty;
            logRepo.Add(log, out Error);            
        }
    }
}
