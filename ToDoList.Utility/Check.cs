using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Utility
{
    public class Check
    {
        public static bool IsNumeric(string value)
        {
            int n;
            return int.TryParse(value, out n);
        }
    }
}
