using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToDoList.Web.Models
{
    public class TaskModel
    {
        public int Id { get; set; }

        [StringLength(255, ErrorMessage="Name cannot be longer than 255 characters.")]
        [Required(ErrorMessage = "Name must be specified.")]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "Start day must be specified.")]
        [Range(1,31,ErrorMessage = "Start day must be specified.")]
        public int StartDay { get; set; }

        [Required(ErrorMessage = "Start month must be specified.")]
        [Range(1, 12, ErrorMessage = "Start month must be specified.")]
        public int StartMonth { get; set; }

        [Required(ErrorMessage = "Start year must be specified.")]
        public int StartYear { get; set; }

        [Required(ErrorMessage = "Start hour and minute  must be specified.")]
        [StringLength(5, ErrorMessage = "Start hour and minute  must be specified.")]
        public string StartHourMinute { get; set; }

        public int NotifyBeforeMin { get; set; }
        public bool IsDone { get; set; }
    }
}