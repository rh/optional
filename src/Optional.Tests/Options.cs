using Optional.Attributes;

namespace Optional.Tests
{
    public class Options
    {
        [Required, Description("Sets foo")]
        public string Foo { get; set; }

        public string Bar { get; set; }

        [ShortName("r"), LongName("foo-bar")]
        public string FooBar { get; set; }

        public bool Switch { get; set; }
    }
}