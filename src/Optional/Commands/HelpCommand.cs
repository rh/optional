using System.Collections.Generic;
using System.Linq;
using Optional.Attributes;
using Optional.Parsers;

namespace Optional.Commands
{
	[Description("Displays help")]
    [Usage("[command]")]
	public class HelpCommand : Command, ICommandsAware, IArgumentsAware
	{
		public static bool DisplayDescription = true;
		public static bool DisplayCopyRight;

		[Ignore]
		public IList<ICommand> Commands { get; set; }

		public override int Execute()
		{
			// args[0] is 'help', args[1] is the name of the command to get help on
			if (ApplicationContext.Arguments.Length == 2)
			{
				var name = ApplicationContext.Arguments[1];
				var commands = Commands.Where(c => c.Name == name);
				if (commands.Count() == 0)
				{
					return new UnknownCommand(name) {ApplicationContext = ApplicationContext}.Execute();
				}

				var command = commands.First();
				WriteLine("{0}: {1}", name, DescriptionOf(command));
				WriteLine("Usage: {0}", UsageOf(command));

				var options = Options.Create(command);
				if (options.Count > 0)
				{
					WriteLine();
					WriteLine("Options:");
					foreach (var option in options)
					{
						WriteLine("  -{0}, --{1,-10} {2}{3}", option.ShortName, option.LongName, option.Required ? "* " : "  ", option.Description);
					}
				}
				return 0;
			}

			// This state is always invalid
			if (ApplicationContext.Arguments.Length > 2)
			{
				WriteLine("Invalid options.");
				WriteLine("Usage: {0} help <command>", ApplicationContext.Name);
				return 1;
			}

			WriteLine("Usage: {0} <command>", ApplicationContext.Name);

			if (DisplayDescription)
			{
				WriteLine();
				WriteLine(ApplicationContext.Description);
			}

			if (DisplayCopyRight)
			{
				if (DisplayDescription == false)
				{
					WriteLine();
				}
				WriteLine(ApplicationContext.Copyright);
			}

			if (Commands != null)
			{
				WriteLine();
				WriteLine("Commands:");
				foreach (var command in Commands)
				{
					WriteLine("  {0,-16} {1}", command.Name, DescriptionOf(command));
				}
			}
			return 0;
		}

		private static string DescriptionOf(ICommand command)
		{
			if (command.GetType().IsDefined(typeof(DescriptionAttribute), false))
			{
				var attribute = (DescriptionAttribute) command.GetType().GetCustomAttributes(typeof(DescriptionAttribute), false)[0];
				return attribute.Value;
			}
			return string.Empty;
		}

		private string UsageOf(ICommand command)
		{
			if (command.GetType().IsDefined(typeof(UsageAttribute), false))
			{
				var attribute = (UsageAttribute) command.GetType().GetCustomAttributes(typeof(UsageAttribute), false)[0];
				return string.Format("{0} {1} {2}", ApplicationContext.Name, command.Name, attribute.Value);
			}
			return string.Format("{0} {1}", ApplicationContext.Name, command.Name);
		}
	}
}