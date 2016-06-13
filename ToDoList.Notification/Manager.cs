using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Repository;
using ToDoList.Entity;

namespace ToDoList.Notification
{
    public class Manager
    {
        private List<INotify> _observers = new List<INotify>();

        public Manager()
        {
            _observers.Add(new EmailNotifier());
            _observers.Add(new SMSNotifier());
        }

        public void MakeNotification()
        {
            DateTime _now = DateTime.Now;

            string Error = string.Empty;
            TaskRepository taskRepo = new TaskRepository();
            List<Entity.Task> taskList = taskRepo.GetAllActive();

            if (taskList != null)
            {
                Dictionary<int, User> userDict = new Dictionary<int, User>();

                UserRepository userRepo = new UserRepository();
                foreach(Entity.Task t in taskList)
                {                    
                    if (t.StartTime.ToString("yyyyMMddhhmm") != _now.AddMinutes(t.NotifyBeforeMin).ToString("yyyyMMddhhmm")) continue;
                    
                    if (!userDict.ContainsKey(t.UserId))
                    {
                        userDict.Add(t.UserId, userRepo.Get(t.UserId));
                    }

                    foreach(INotify observer in _observers)
                    {
                        observer.Notify(userDict[t.UserId], t);
                    }
                }
            }
        }
    }
}
