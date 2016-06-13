using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Data;
using ToDoList.Entity;

namespace ToDoList.Repository
{
    public class TodoRepository
    {
        private readonly TodoContext _todoContext;
        public TodoRepository()
        {
            _todoContext = new TodoContext();
        }

        public List<Todo> TodoList(int UserId)
        {
            return _todoContext.TodoList(UserId);
        }

        public Todo Get(int TodoId)
        {
            return _todoContext.Get(TodoId);
        }

        public bool Save(Todo todo, out string Error)
        {
            Error = string.Empty;

            return todo.Id < 1 ? _todoContext.Add(todo, out Error) : _todoContext.Edit(todo, out Error);
        }

        public List<Todo> GetAll()
        {
            return _todoContext.GetAll();
        }

        public bool Delete(int TodoId, out string Error)
        {
            Error = string.Empty;

            TaskRepository _taskRepository = new TaskRepository();
            bool status = _taskRepository.DeleteByTodo(TodoId, out Error);

            if (status)
            {
                status = _todoContext.Delete(TodoId, out Error);
            }

            return status;
        }
    }
}
