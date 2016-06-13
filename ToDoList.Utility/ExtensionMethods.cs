using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Utility
{
    public static class ExtensionMethods
    {
        public static string MakeDoubleDigit(this string value)
        {
            if (value.Length < 2)
            {
                value = ("0" + value);
            }
            return value;
        }
    }
}
