using Optional.Attributes;

namespace Optional.Commands
{
    [Description("Displays the version")]
    public class VersionCommand : Command
    {
        public override int Execute()
        {
            WriteLine(ApplicationContext.Version);
            return 0;
        }
    }
}