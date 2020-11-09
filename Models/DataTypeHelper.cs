using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelReader.Models
{
    public class DataTypeHelper
    {
        public static string TypeChecker(object item)
        {
            string result = "Unknown";
            if (item.GetType() == typeof(int))
            {
                result = item.GetType().Name;
            }
            else if (item.GetType() == typeof(decimal))
            {
                result = "Decimal";
            }
            else if (item.GetType() == typeof(double))
            {
                var doubleType = Math.Abs((double) item % 1) < double.Epsilon;
                result = doubleType ? "Integer" : "Double";
            }
            else if (item.GetType() == typeof(string))
            {
                result = "String";
            }
            return result;
        }
    }
}