using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Entity
{
    public class Todo
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
