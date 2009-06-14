using System.Collections.Generic;

namespace Optional.Commands
{
    public interface ICommandsAware
    {
        IList<ICommand> Commands { set; }
    }
}