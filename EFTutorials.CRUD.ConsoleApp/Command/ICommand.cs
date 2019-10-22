using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTutorials.CRUD.ConsoleApp.Command
{
    public interface ICommand
    {
        string Description { get; }
        void ExecuteAction();
    }
}
