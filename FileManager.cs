using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    class FileManager
    {
        public static void WriteToFile(string fileName, string text)
        {
            System.IO.File.WriteAllText(fileName, text);
        }

        public static string ReadFromFile(string fileName)
        {
            return System.IO.File.ReadAllText(fileName);
        }
    }
}
