namespace Optional.Commands
{
	public class UnknownCommand : Command
	{
		private readonly string command;

		public UnknownCommand(string command)
		{
			this.command = command;
		}

		public override int Execute()
		{
			WriteLine("Unknown command: '{0}'.", command);
			return 1;
		}
	}
}