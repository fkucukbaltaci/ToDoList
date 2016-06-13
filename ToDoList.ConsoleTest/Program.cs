using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ToDoList.Repository;
using ToDoList.Entity;
using ToDoList.Notification;
namespace ToDoList.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var userRepo = new UserRepository();
            var taskRepo = new TaskRepository();
            var todoRepo = new TodoRepository();

              
            Console.ReadLine();
        }
    }
}
