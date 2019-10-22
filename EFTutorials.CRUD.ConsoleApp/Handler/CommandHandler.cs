using EFTutorials.CRUD.ConsoleApp.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTutorials.CRUD.ConsoleApp.Handler
{
    public class CommandHandler : ICommandHandler
    {
        private readonly List<ICommand> commands;
        private ICommand command;
        public CommandHandler()
        {
            this.commands = new List<ICommand>();
        }      

        public void SetCommand(ICommand command) => this.command = command;
        public void Invoke()
        {
            commands.Add(command);
            command.ExecuteAction();
        }      
    }
}
