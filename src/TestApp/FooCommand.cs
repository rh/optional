using Optional.Attributes;
using Optional.Commands;

namespace TestApp
{
    [Description("Sets everything to foo")]
    public class FooCommand : Command
    {
        [Required, Description("Sets bar")]
        public string Bar { get; set; }

        [Description("Sets a flag")]
        public bool Flag { get; set; }

        public override int Execute()
        {
            WriteLine("Executing foo with Bar='{0}', Flag={1}...", Bar, Flag);
            return 0;
        }
    }
}