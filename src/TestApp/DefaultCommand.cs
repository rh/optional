using Optional.Commands;

namespace TestApp
{
	public class DefaultCommand : Command
	{
		public override int Execute()
		{
			WriteLine("Type '{0} help' for usage.", ApplicationContext.Name);
			return -1;
		}
	}
}