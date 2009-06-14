using System.Collections.Generic;
using System.Linq;
using Optional.Exceptions;
using Optional.Parsers;

namespace Optional.Commands
{
    public class CommandFactory
    {
        public IApplicationContext Context { get; set; }
        public IList<ICommand> Commands { get; set; }

        /// <summary>
        /// Creates a new <see cref="CommandFactory"/> and registers <see cref="HelpCommand"/>
        /// and <see cref="VersionCommand"/>.
        /// </summary>
        /// <remarks>
        /// If the standard <see cref="HelpCommand"/> and <see cref="VersionCommand"/> are not preferred,
        /// they can be removed like this:
        /// <code>
        /// var factory = new CommandFactory();
        /// factory.Commands.Clear();
        /// </code>
        /// </remarks>
        public CommandFactory(string[] args)
        {
            Context = new ApplicationContext(args);
            Commands = new List<ICommand>();
            Register<HelpCommand>();
            Register<VersionCommand>();
        }

        public ICommand Create(string[] args)
        {
            var command = CreateInternal(args);
            if (command is ICommandsAware)
            {
                // Certain commands, such as HelpCommand, may need access to all available commands
                (command as ICommandsAware).Commands = Commands;
            }
            command.ApplicationContext = Context;
            return command;
        }

        private ICommand CreateInternal(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                // TODO: what if a client has removed HelpCommand from Commands?
                // In that case this usage would be inconsistent
                return new HelpCommand();
            }

            var name = args[0].ToLower();
            var arguments = args.Skip(1).ToArray();

            foreach (var command in Commands)
            {
                if (command.Name != name)
                {
                    continue;
                }
                if (command is IArgumentsAware)
                {
                    // command wants to process the supplied command-line arguments on its own
                    return command;
                }
                return new Parser().Parse(arguments, command);
            }

            // No command found. Consider 'name' to be the first part of a command
            IList<ICommand> matches = new List<ICommand>();
            foreach (var command in Commands)
            {
                if (command.Name.StartsWith(name))
                {
                    matches.Add(command);
                }
            }

            if (matches.Count == 1)
            {
                var command = matches[0];
                if (command is IArgumentsAware)
                {
                    // command wants to process the supplied command-line arguments on its own
                    return command;
                }
                return new Parser().Parse(arguments, command);
            }

            if (matches.Count > 1)
            {
                return new AmbiguousMatchCommand {Prefix = name, Commands = matches};
            }

            return new UnknownCommand(name);
        }

        public void Register<T>() where T : ICommand, new()
        {
            Register(new T());
        }

        public void Register(ICommand command)
        {
            foreach (var registered in Commands)
            {
                if (registered.Name.Equals(command.Name))
                {
                    throw new DuplicateCommandException(command.Name);
                }
            }
            Commands.Add(command);
        }
    }
}