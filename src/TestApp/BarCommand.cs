using Optional.Attributes;
using Optional.Commands;

namespace TestApp
{
	[Description("Lorem ipsum")]
	[Usage("<argument>")]
	public class BarCommand : Command, IArgumentsAware
	{
		public override int Execute()
		{
			if (ApplicationContext.Arguments.Length < 2)
			{
				WriteLine("No argument given.");
				return 1;
			}

			if (ApplicationContext.Arguments.Length > 2)
			{
				WriteLine("Too many arguments given.");
				return 1;
			}

			WriteLine("Argument is '{0}'.", ApplicationContext.Arguments[1]);
			return 0;
		}
	}
}