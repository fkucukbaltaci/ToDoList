using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoList.Repository;
using ToDoList.Entity;
using ToDoList.Utility;
using ToDoList.Web.Models;
using Microsoft.AspNet.Identity;

namespace ToDoList.Web.Controllers
{
    public class TaskController : Controller
    {
        TaskRepository taskRepo = new TaskRepository();
        TodoRepository todoRepo = new TodoRepository();

        // GET: Task
        [Authorize]
        public ActionResult Index(int TodoId)
        {
            ViewBag.TodoId = TodoId;
            List<Task> TaskList = taskRepo.TaskList(TodoId, Convert.ToInt32(User.Identity.GetUserId()));
            ViewBag.TaskList = TaskList;

            Todo todo = todoRepo.Get(TodoId);
            if (todo == null)
            {
                return RedirectToAction("Todo");
            }
            ViewBag.Header = todo.Name;

            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddEdit(int TodoId, int TaskId)
        {
            ViewBag.SuccessMessage = "";
            ViewBag.CustomValidationSummary = "";
            ViewBag.TodoId = TodoId;
            ViewBag.TaskId = TaskId;
            ViewBag.ActionMode = "Edit";

            Task t = new Task();
            if (TaskId < 1)
            {
                ViewBag.ActionMode = "Add";
                t.NotifyBeforeMin = 15;
                t.StartTime = DateTime.Now.AddMinutes(5);
                t.IsDone = false;
                t.Name = string.Empty;
                t.UserId = Convert.ToInt32(User.Identity.GetUserId());
                t.TodoId = TodoId;
                t.Description = string.Empty;
                t.Id = 0;
            }
            else
            {
                t=taskRepo.Get(TaskId);
            }

            TaskModel model = new TaskModel {
                Description = t.Description,
                Id = t.Id,
                IsDone = t.IsDone,
                Name = t.Name,
                NotifyBeforeMin = t.NotifyBeforeMin,
                StartDay = t.StartTime.Day,
                StartHourMinute = t.StartTime.Hour.ToString().MakeDoubleDigit() + ":" + t.StartTime.Minute.ToString().MakeDoubleDigit(),
                StartMonth = t.StartTime.Month,
                StartYear = t.StartTime.Year
            };

            List<DateSelectItem> dDayList = new List<DateSelectItem>();
            for (int i = 1; i <= 31; i++) { dDayList.Add(new DateSelectItem { Value=i,Text=i.ToString().MakeDoubleDigit() }); }

            List<DateSelectItem> dMonthList = new List<DateSelectItem>();
            for (int i = 1; i <= 12; i++) { dMonthList.Add(new DateSelectItem { Value = i, Text = i.ToString().MakeDoubleDigit() }); }

            ViewBag.dayList = new SelectList(dDayList,"Value","Text",1);
            ViewBag.monthList = new SelectList(dMonthList, "Value", "Text", 1);

            return View("AddEdit",model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEdit(int TodoId, int TaskId, TaskModel model)
        {
            ViewBag.SuccessMessage = "";
            ViewBag.CustomValidationSummary = "";
            ViewBag.TodoId = TodoId;
            ViewBag.TaskId = TaskId;
            ViewBag.ActionMode = "Edit";
            model.Id = TaskId;
            if (model.Id<1) ViewBag.ActionMode = "Add";

              if (ModelState.IsValid)
            {
                bool StartHourValidation = model.StartHourMinute!=null && model.StartHourMinute.Contains(":") && Check.IsNumeric(model.StartHourMinute.Replace(":", string.Empty));

                if (!StartHourValidation)
                {
                    ViewBag.CustomValidationSummary = "Start hour and minute  must be specified.";
                }
                else
                {
                    char[] seperator = new char[] { ':' };
                    int Hour = Convert.ToInt32(model.StartHourMinute.Split(seperator)[0]);
                    int Minute = Convert.ToInt32(model.StartHourMinute.Split(seperator)[1]);

                    Task t = new Task();
                    if (model.Id > 0)
                    {
                         t = taskRepo.Get(model.Id);
                    }else
                    {
                        t.UserId = Convert.ToInt32(User.Identity.GetUserId()); ;
                        t.TodoId = TodoId;
                    }
                    
                    t.Description = model.Description;
                    t.IsDone = model.IsDone;
                    t.Name = model.Name;
                    t.NotifyBeforeMin = model.NotifyBeforeMin;
                    t.StartTime = new DateTime(model.StartYear, model.StartMonth, model.StartDay, Hour, Minute, 0);

                    string Error = "";
                    int taskId = taskRepo.Save(t, out Error);

                    if (taskId < 0)
                    {
                        ViewBag.CustomValidationSummary = Error;
                    }else
                    {
                        model.Id = taskId;
                        ViewBag.SuccessMessage = ViewBag.ActionMode=="Edit" ? "Task updated" : "New Task Added";
                        ViewBag.ActionMode = "Edit";
                    }
                }
            }

          

            List<DateSelectItem> dDayList = new List<DateSelectItem>();
            for (int i = 1; i <= 31; i++) { dDayList.Add(new DateSelectItem { Value = i, Text = i.ToString().MakeDoubleDigit() }); }

            List<DateSelectItem> dMonthList = new List<DateSelectItem>();
            for (int i = 1; i <= 12; i++) { dMonthList.Add(new DateSelectItem { Value = i, Text = i.ToString().MakeDoubleDigit() }); }

            ViewBag.dayList = new SelectList(dDayList, "Value", "Text", 1);
            ViewBag.monthList = new SelectList(dMonthList, "Value", "Text", 1);

            
            return View("AddEdit", model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int TodoId, int TaskId)
        {
            string Error = string.Empty;
            taskRepo.Delete(TaskId, out Error);
            return RedirectToAction("Index", "Task", new { TodoId = TodoId });
        }

    }

    class DateSelectItem
    {
        public int Value { get; set; }
        public string Text { get; set; }
    }

    
}