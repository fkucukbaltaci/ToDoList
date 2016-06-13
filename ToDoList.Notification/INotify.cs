using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDoList.Entity;

namespace ToDoList.Notification
{
    interface INotify
    {
        void Notify(User user, Task task);
    }
}
