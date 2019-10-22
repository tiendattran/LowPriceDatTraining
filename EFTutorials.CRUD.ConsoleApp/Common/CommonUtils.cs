using EFTutorials.CRUD.ConsoleApp.Command;
using EFTutorials.CRUD.ConsoleApp.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTutorials.CRUD.ConsoleApp.Common
{
    public class CommonUtils
    {
        public static string EXIT_STRING = "E0";
        public static void ExecuteCommand(IHandler handler, ICommand cmd)
        {
            handler.SetCommand(cmd);
            handler.Invoke();
        }
    }
}
