using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoList.Repository;
using ToDoList.Entity;
using Microsoft.AspNet.Identity;

namespace ToDoList.Web.Controllers
{
    public class TodoController : Controller
    {
        // GET: Todo
        [Authorize]
        public ActionResult Index()
        {
            TodoRepository todoRepo = new TodoRepository();
            ViewBag.Todos = todoRepo.TodoList(Convert.ToInt32(User.Identity.GetUserId()));
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int TodoId)
        {
            string Error = string.Empty;
            TodoRepository todoRepo = new TodoRepository();
            todoRepo.Delete(TodoId, out Error);

            return RedirectToAction("Index", "Todo");
        }

        [Authorize]
        [HttpPost]
        public ActionResult Save(string Name)
        {
            TodoRepository todoRepo = new TodoRepository();
            Todo todo = new Todo()
            {
                Id = 0,
                Name = Name,
                UserId = Convert.ToInt32(User.Identity.GetUserId())
            };

            string Error = string.Empty;
            bool status = todoRepo.Save(todo, out Error);
            return status ? Json("ok") : Json(Error);
        }

  
    }
}