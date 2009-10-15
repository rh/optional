using System.Threading;
using Optional.Attributes;
using Optional.Commands;

namespace TestApp
{
	[Description("Does something fast")]
	public class FastCommand : Command
	{
		public override int Execute()
		{
			Write("Processing ");
			for (var i = 0; i < 10; i++)
			{
				Write(".");
				Thread.Sleep(300);
			}
			WriteLine();
			return 0;
		}
	}
}