using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Test.Converter
{
    public class ListConverter
    {
        public static string ListToString(List<int?> input)
        {
            return string.Join(',', input);
        }

        public static List<int> StringToList(string input)
        {
            return input.Split(',').ToList().Select(int.Parse).ToList();
        }
    }
}
