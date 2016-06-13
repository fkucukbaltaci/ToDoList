using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Utility
{
    public class Url
    {
        public static string TaskEditUrl(int TodoId, int TaskId)
        {
            return string.Format("/Task/AddEdit/{0}-{1}", TodoId.ToString(), TaskId.ToString());
        }
    }
}
