using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTutorials.CRUD.ConsoleApp.Helper
{
    public static class ConsoleHelper
    {
        public static void CleanTheConsole(string title)
        {
            Console.Clear();
            Console.WriteLine("------- " + title + "-------");
        }
        public static void DrawHorizontalLine(char character, int length)
        {
            for (int i = 0; i < length; i++)
            {
                Console.Write(character);
            }
            Console.WriteLine();
        }
    }
}
