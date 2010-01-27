using Optional.Parsers;
using Xunit;

namespace Optional.Tests
{
    public class NameValueParserFixture
    {
        [Fact]
        public void CreateListOfOptions()
        {
            var parser = new NameValueParser();
            var options = parser.Parse(new[] {"-f", "foo", "-b", "bar", "--baz", "-x", "--last"});

            Assert.NotEmpty(options);
            Assert.Equal(5, options.Count);

            Assert.Equal("f", options[0].ShortName);
            Assert.Equal(string.Empty, options[0].LongName);
            Assert.Equal("foo", options[0].Value);

            Assert.Equal("b", options[1].ShortName);
            Assert.Equal(string.Empty, options[1].LongName);
            Assert.Equal("bar", options[1].Value);

            Assert.Equal(string.Empty, options[2].ShortName);
            Assert.Equal("baz", options[2].LongName);
            Assert.Equal(null, options[2].Value);

            Assert.Equal("x", options[3].ShortName);
            Assert.Equal(string.Empty, options[3].LongName);
            Assert.Equal(null, options[2].Value);

            Assert.Equal(string.Empty, options[4].ShortName);
            Assert.Equal("last", options[4].LongName);
            Assert.Equal(null, options[2].Value);
        }

        [Fact]
        public void CreateOneOptionWithoutAValue()
        {
            var parser = new NameValueParser();
            var options = parser.Parse(new[] {"foo"});

            Assert.NotEmpty(options);
            Assert.Equal(1, options.Count);

            Assert.True(options[0].ShortName == string.Empty);
            Assert.True(options[0].LongName == string.Empty);
            Assert.Equal("foo", options[0].Value);
        }
    }
}