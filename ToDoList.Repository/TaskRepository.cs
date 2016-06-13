using System.Collections.Generic;
using ToDoList.Data;
using ToDoList.Entity;

namespace ToDoList.Repository
{
    public class TaskRepository
    {
        private readonly TaskContext _taskContext;
        public TaskRepository()
        {
            _taskContext = new TaskContext();
        }

        public List<Task> TaskList(int TodoId, int UserId)
        {
            return _taskContext.TaskList(TodoId, UserId);
        }

        public Task Get(int TaskId)
        {
            return _taskContext.Get(TaskId);
        }

        public int Save(Task task, out string Error)
        {
            Error = string.Empty;
            
            return task.Id < 1 ? _taskContext.Add(task, out Error) : _taskContext.Edit(task, out Error);
        }

        public List<Task> GetAll()
        {
            return _taskContext.GetAll();
        }

        public bool Delete(int TaskId, out string Error)
        {
            Error = string.Empty;
            return _taskContext.Delete(TaskId, out Error);
        }

        public bool DeleteByTodo(int TodoId, out string Error)
        {
            Error = string.Empty;
            return _taskContext.DeleteByTodo(TodoId, out Error);
        }

        public List<Task> GetNotificationList(out string Error)
        {
            Error = string.Empty;
            return _taskContext.GetNotificationList(out Error);
        }

        public List<Task> GetAllActive()
        {
            return _taskContext.GetAllActive();
        }

    }
}
