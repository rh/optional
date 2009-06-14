using System.Collections.Generic;

namespace Optional.Commands
{
    /// <summary>
    /// When an application is called on the command-line with just the first part of the
    /// name of a command, and multiple commands match the given name, this command is executed.
    /// </summary>
    public class AmbiguousMatchCommand : Command
    {
        public string Prefix { get; set; }
        public IEnumerable<ICommand> Commands { get; set; }

        public override int Execute()
        {
            WriteLine("Multiple matches for '{0}':", Prefix);
            foreach (var command in Commands)
            {
                WriteLine("  {0,-16}", command.Name);
            }
            return 2;
        }
    }
}